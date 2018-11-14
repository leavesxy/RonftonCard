using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Bluemoon;
using System.Runtime.InteropServices;
using RonftonCard.Common.AuthenKey;

namespace RonftonCard.AuthenKey.RockeyArm
{
	public partial class DongleKey  : AbstractAuthenKey
	{
		private const String defaultErrMsgFileName = "ErrorMessage.properties";
		private List<AuthenKeyInfo> keyInfo;

		// handle for dog
		private Int64 hDongle;

		#region "--- Constructor ---"

		public DongleKey( byte[] seed )
			: this( seed, defaultErrMsgFileName )
		{
		}

		public DongleKey( byte[] seed, String errMsgFileName )
			: this(seed, errMsgFileName, AuthenKeyConst.DEFAULT_ADMIN_PIN_DONGLE, AuthenKeyConst.DEFAULT_USER_PIN_DONGLE, Encoding.UTF8)
		{

		}

		public DongleKey(byte[] seed, String errMsgFileName, String adminPin, String userPin, Encoding encoding)
			: base(seed, errMsgFileName, adminPin, userPin, encoding)
		{
			this.hDongle = -1;
			this.keyInfo = Enumerate();
		}

		#endregion

		#region "--- util ---"
		/// <summary>
		/// enumerate all keys
		/// </summary>
		private List<AuthenKeyInfo> Enumerate()
		{
			long count = 0;
			this.LastErrorCode = Dongle_Enum(IntPtr.Zero, out count);

			//no key found
			if (!IsSucc() || count <= 0)
				return null;

			AbstractAuthenKey.logger.Debug(String.Format("found {0} Dogs !", count));

			List<AuthenKeyInfo> keyInfo = new List<AuthenKeyInfo>();
			IntPtr pDongleInfo = IntPtr.Zero;

			try
			{
				int size = IntPtrUtil.SizeOf(typeof(DONGLE_INFO));
				pDongleInfo = IntPtrUtil.Create(size * (int)count);
				this.LastErrorCode = Dongle_Enum(pDongleInfo, out count);

				for (int i = 0; i < count; i++)
				{
					IntPtr ptr = IntPtrUtil.Create(pDongleInfo, i * size);
					//(DONGLE_INFO)Marshal.PtrToStructure(ptr, typeof(DONGLE_INFO));
					DONGLE_INFO dongleInfo = IntPtrUtil.CreateObject<DONGLE_INFO>(ptr);
					keyInfo.Add(CreateAuthenKeyInfo((short)i, dongleInfo));
				}
			}
			catch (Exception ex)
			{
				AbstractAuthenKey.logger.Error(ex.Message);
			}
			finally
			{
				IntPtrUtil.Free(ref pDongleInfo);
			}
			return keyInfo;
		}

		private AuthenKeyInfo CreateAuthenKeyInfo(short seq, DONGLE_INFO dongleInfo)
		{
			AuthenKeyInfo keyInfo = new AuthenKeyInfo()
			{
				Seq = seq,
				Version = String.Format("v{0}.{1:d2}-({2:x2},{3})",
								dongleInfo.m_Ver >> 8 & 0xff,
								dongleInfo.m_Ver & 0xff,
								dongleInfo.m_Type,
								BitConverter.ToString(dongleInfo.m_BirthDay)),
				UserId = dongleInfo.m_UserID.ToString("X08"),
				AppId = dongleInfo.m_PID.ToString("X08"),
				KeyId = BitConverter.ToString(dongleInfo.m_HID)
			};

			AbstractAuthenKey.logger.Debug(keyInfo.ToString());

			return keyInfo;
		}
		
		public bool Authen(AuthenMode authenMode, byte[] pin)
		{
			uint flag = (authenMode == AuthenMode.ADMIN) ? (uint)1 : (uint)0;
			int pRemainCount;
			this.LastErrorCode = Dongle_VerifyPIN(this.hDongle, flag, pin, out pRemainCount);

			return IsSucc();
		}


		/// <summary>
		/// after invoke SUCC,status of key is anonymous
		/// </summary>
		public bool Initialize(out byte[] newAdminPwd, out byte[] pid)
		{
			pid = new byte[8];
			newAdminPwd = new byte[16];

			logger.Debug("Initialize Key , seed = " + BitConverter.ToString( this.seed) );

			//unique key, requst admin privilege
			if (!Authen(AuthenMode.ADMIN, Encoding.UTF8.GetBytes(this.adminPin)))
				return false;

			this.LastErrorCode = Dongle_GenUniqueKey(hDongle, seed.Length, seed, pid, newAdminPwd);

			logger.Debug("new pid = " + this.encoding.GetString(pid));
			logger.Debug("new admin pwd = " + this.encoding.GetString(newAdminPwd) );

			return IsSucc();
		}

		protected bool SetUserID(uint uid)
		{
			this.LastErrorCode = Dongle_SetUserID(this.hDongle, uid);
			return IsSucc();
		}


		/// <summary>
		/// create User root key file,and Key is request 16 bytes at least
		/// </summary>
		private bool CreateKeyFile(ushort descriptor, byte[] userRootkey)
		{
			KEY_FILE_ATTR keyAttr = new KEY_FILE_ATTR();
			keyAttr.m_Size = 16;
			keyAttr.m_Lic.m_Priv_Enc = 0;

			IntPtr ptr = IntPtrUtil.CreateByStru(keyAttr);
			this.LastErrorCode = Dongle_CreateFile(hDongle, DongleFileType.FILE_KEY, descriptor, ptr);
			IntPtrUtil.Free(ref ptr);

			if (!IsSucc())
				return false;

			// dongle key require 16 bytes key
			logger.Debug(String.Format("Create Key file, descriptor={0}, key={1}", descriptor, BitConverter.ToString(userRootkey)));

			this.LastErrorCode = Dongle_WriteFile(hDongle, DongleFileType.FILE_KEY, descriptor, 0, userRootkey, 16);

			return IsSucc();
		}

		#endregion

		/// <summary>
		/// create RSA private key
		/// </summary>
		private PRIKEY_FILE_ATTR CreatePrikeyFileAttr()
		{
			PRIKEY_FILE_ATTR priAttr = new PRIKEY_FILE_ATTR();

			priAttr.m_Size = RSA_KEY_LEN * 8;
			priAttr.m_Type = DongleFileType.FILE_PRIKEY_RSA;
			priAttr.m_Lic.m_Count = 0xFFFFFFFF;
			priAttr.m_Lic.m_IsDecOnRAM = 0;
			priAttr.m_Lic.m_IsReset = 0;
			priAttr.m_Lic.m_Priv = 0;
			return priAttr;
		}

		public bool CreateAuthenKeyFile(ushort descriptor, byte[] inData, out byte[] outData)
		{
			PRIKEY_FILE_ATTR priAttr = CreatePrikeyFileAttr();
			IntPtr ptr = CreateIntPtr(priAttr);

			try
			{
				this.LastErrorCode = Dongle_CreateFile(hDongle, DongleFileType.FILE_PRIKEY_RSA, descriptor,  ptr);
			}
			finally
			{
				FreeIntPtr(ptr);
			}

			IntPtr pRsaPub = CreateIntPtr(Marshal.SizeOf(typeof(RSA_PUBLIC_KEY)) );
			IntPtr pRsaPri = CreateIntPtr(Marshal.SizeOf(typeof(RSA_PRIVATE_KEY)));

			//this.LastErrorCode = Dongle_RsaGenPubPriKey(hDongle, DongleKey.AUTHEN_KEY_DESCRIPTOR, ref rsaPub, ref rsaPri);
			this.LastErrorCode = Dongle_RsaGenPubPriKey(hDongle, DongleKey.AUTHEN_KEY_DESCRIPTOR, pRsaPub, pRsaPri);

			RSA_PUBLIC_KEY rsaPub = (RSA_PUBLIC_KEY)Marshal.PtrToStructure(pRsaPub, typeof(RSA_PUBLIC_KEY));
			RSA_PRIVATE_KEY rsaPri = (RSA_PRIVATE_KEY)Marshal.PtrToStructure(pRsaPri, typeof(RSA_PRIVATE_KEY));

			FreeIntPtr(pRsaPub);
			FreeIntPtr(pRsaPri);

			LogRsaKey(rsaPub, rsaPri);
			outData = rsaPub.exponent;

			return IsSucc();
		}

		public override bool Encrypt(AuthenKeyType keyType, byte[]plain, out byte[] cipher)
		{
			cipher = null;

			switch (keyType)
			{
				case AuthenKeyType.COMPANY_SEED:
					TDesEncrypt(DongleKey.COMPANY_SEED_KEY_DESCRIPTOR, plain, out cipher);
					break;

				case AuthenKeyType.USER_ROOT:
					TDesEncrypt(DongleKey.USER_ROOT_KEY_DESCRIPTOR, plain, out cipher);
					break;

				case AuthenKeyType.AUTHEN:
					RsaPriEncrypt(DongleKey.AUTHEN_KEY_DESCRIPTOR, plain, out cipher);
					break;

				default:
					break;
			}

			return IsSucc();
		}

		private void TDesEncrypt(ushort descriptor, byte[] plain, out byte[] cipher)
		{
			int len = (plain.Length % 16 == 0) ? plain.Length : (plain.Length / 16 + 1) * 16;

			// fill zero
			cipher = ArrayUtil.CopyFrom<byte>(plain, len);

			this.LastErrorCode = Dongle_TDES(this.hDongle, descriptor, 0, cipher, cipher, (uint)len);
		}

		private void RsaPriEncrypt(ushort descriptor, byte[] plain, out byte[] cipher)
		{
			uint nOutDataLen = RSA_KEY_LEN;
			cipher = new byte[nOutDataLen];
			this.LastErrorCode = Dongle_RsaPri(this.hDongle, descriptor, 0, plain, (uint)plain.Length, cipher, ref nOutDataLen);
		}

		public void RsaPubDecrypt(byte[] pubKey, byte[] plain, out byte[] cipher)
		{
			uint nOutDataLen = RSA_KEY_LEN;
			cipher = new byte[nOutDataLen];

			RSA_PUBLIC_KEY rsaPub = new RockeyArm.DongleKey.RSA_PUBLIC_KEY();

			rsaPub.bits = RSA_KEY_LEN * 8 ;
			rsaPub.modulus = 65537;
			rsaPub.exponent = new byte[256];
			Array.Copy(pubKey, rsaPub.exponent, RSA_KEY_LEN);

			this.LastErrorCode = Dongle_RsaPub(this.hDongle, 1, ref rsaPub, plain, (uint)plain.Length, cipher, ref nOutDataLen);
		}



		private void LogRsaKey(RSA_PUBLIC_KEY rsaPub, RSA_PRIVATE_KEY rsaPri)
		{
			logger.Debug(String.Format("RSA-PRI : bit={0},modulus={1}", rsaPri.bits, rsaPri.modulus));
			logger.Debug("RSA-PRI : " + Convert.ToBase64String(rsaPri.exponent));
			logger.Debug("    128 : " + Convert.ToBase64String(rsaPri.exponent, 0, RSA_KEY_LEN));
			logger.Debug("   -PUB : " + Convert.ToBase64String(rsaPri.publicExponent));
			logger.Debug("   -128 : " + Convert.ToBase64String(rsaPri.publicExponent, 0, RSA_KEY_LEN));


			logger.Debug(String.Format("RSA-PUB : bit={0},modulus={1}", rsaPub.bits, rsaPub.modulus));
			logger.Debug("RSA-PUB : " + Convert.ToBase64String(rsaPub.exponent, 0, RSA_KEY_LEN));
			logger.Debug("     (*): " + HexString.ToHexString(rsaPub.exponent));
		}

		private byte[] StructToBytes(object structObj)
		{
			int size = Marshal.SizeOf(structObj);
			byte[] _bytes = new byte[size];
			IntPtr structPtr = Marshal.AllocHGlobal(size);
			Marshal.StructureToPtr(structObj, structPtr, false);
			Marshal.Copy(structPtr, _bytes, 0, size);
			Marshal.FreeHGlobal(structPtr);
			return _bytes;
		}
	}
}