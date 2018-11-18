using System;
using System.Collections.Generic;
using System.Text;
using Bluemoon;
using RonftonCard.Core.Dongle;

namespace RonftonCard.Dongle.RockeyArm
{
	public partial class RockeyArmDongle  : AbstractDongle
	{
		private const String defaultErrMsgFileName = "RockeyArmErrorMessage.properties";

		#region "--- Constructor ---"

		public RockeyArmDongle()
			: this( Charset.UTF8.GetAliasName(),DongleConst.DEFAULT_SEED_KEY,defaultErrMsgFileName)
		{
		}

		public RockeyArmDongle(String encoding, String seed, String errMsgFileName )
			: this(encoding, seed, errMsgFileName,DongleConst.DEFAULT_ADMIN_PIN_DONGLE,DongleConst.DEFAULT_USER_PIN_DONGLE)
		{
		}

		public RockeyArmDongle(String encoding, String seed, String errMsgFileName, String adminPin, String userPin)
			: base(encoding, seed, errMsgFileName, adminPin, userPin)
		{
			//Enumerate();
		}

		#endregion

		/// <summary>
		/// enumerate all dongle keys
		/// </summary>
		private DongleInfo[] EnumerateDongle()
		{
			long count = 0;
			this.LastErrorCode = Dongle_Enum(IntPtr.Zero, out count);

			//no key found
			if (!Succ() || count <= 0)
				return null;

			logger.Debug(String.Format("found {0} Dogs !", count));

			List<DongleInfo> keyInfo = new List<DongleInfo>();
			IntPtr pDongleInfo = IntPtr.Zero;

			try
			{
				int size = IntPtrUtil.SizeOf(typeof(DONGLE_INFO));
				pDongleInfo = IntPtrUtil.Create(size * (int)count);
				this.LastErrorCode = Dongle_Enum(pDongleInfo, out count);

				for (int i = 0; i < count; i++)
				{
					IntPtr ptr = IntPtrUtil.Create(pDongleInfo, i * size);
					DONGLE_INFO devInfo = IntPtrUtil.ToStructure<DONGLE_INFO>(ptr);
					keyInfo.Add(ParseDongleInfo((short)i, devInfo));
				}
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
			}
			finally
			{
				IntPtrUtil.Free(ref pDongleInfo);
			}
			return keyInfo.ToArray();
		}

		private DongleInfo ParseDongleInfo(short seq, DONGLE_INFO devInfo)
		{
			DongleInfo dongleInfo = new DongleInfo()
			{
				Seq = seq,
				Version = String.Format("v{0}.{1:d2}-({2:x2},{3})",
								devInfo.m_Ver >> 8 & 0xff,
								devInfo.m_Ver & 0xff,
								devInfo.m_Type,
								BitConverter.ToString(devInfo.m_BirthDay)),
				UserId = devInfo.m_UserID.ToString("X08"),
				AppId = devInfo.m_PID.ToString("X08"),
				KeyId = BitConverter.ToString(devInfo.m_HID)
			};
			AbstractDongle.logger.Debug(dongleInfo.ToString());
			return dongleInfo;
		}
		
		public bool Authen(Int64 hDongle, AuthenMode authenMode, byte[] pin)
		{
			uint flag = (authenMode == AuthenMode.ADMIN) ? (uint)1 : (uint)0;
			int pRemainCount;
			this.LastErrorCode = Dongle_VerifyPIN(hDongle, flag, pin, out pRemainCount);

			return Succ();
		}

		/// <summary>
		/// after invoke SUCC,status of key is anonymous
		/// </summary>
		public bool Initialize(Int64 hDongle, out byte[] newAdminPwd, out byte[] appId)
		{
			appId = new byte[8];
			newAdminPwd = new byte[16];

			logger.Debug("Initialize Key , seed = " + BitConverter.ToString( this.seed) );

			//unique key, requst admin privilege
			if (!Authen(hDongle, AuthenMode.ADMIN, Encoding.UTF8.GetBytes(this.adminPin)))
				return false;

			this.LastErrorCode = Dongle_GenUniqueKey(hDongle, seed.Length, seed, appId, newAdminPwd);

			logger.Debug("new pid = " + this.Encoder.GetString(appId));
			logger.Debug("new admin pwd = " + this.Encoder.GetString(newAdminPwd) );

			return Succ();
		}

		protected bool SetUserID(Int64 hDongle, uint uid)
		{
			this.LastErrorCode = Dongle_SetUserID(hDongle, uid);
			return Succ();
		}


		/// <summary>
		/// create User root key file,and Key is request 16 bytes at least
		/// </summary>
		private bool CreateKeyFile(Int64 hDongle, ushort descriptor, byte[] userRootkey)
		{
			KEY_FILE_ATTR keyAttr = new KEY_FILE_ATTR();
			keyAttr.m_Size = 16;
			keyAttr.m_Lic.m_Priv_Enc = 0;

			IntPtr ptr = IntPtrUtil.CreateByStru(keyAttr);
			this.LastErrorCode = Dongle_CreateFile(hDongle, RockeyArmFileType.FILE_KEY, descriptor, ptr);
			IntPtrUtil.Free(ref ptr);

			if (!Succ())
				return false;

			// dongle key require 16 bytes key
			logger.Debug(String.Format("Create Key file, descriptor={0}, key={1}", descriptor, BitConverter.ToString(userRootkey)));

			this.LastErrorCode = Dongle_WriteFile(hDongle, RockeyArmFileType.FILE_KEY, descriptor, 0, userRootkey, 16);

			return Succ();
		}

		/// <summary>
		/// convert String to int,default base 16
		/// </summary>
		private uint ToUint32(String str, int fromBase = 16)
		{
			uint v = 0;

			try
			{
				v = Convert.ToUInt32(str, fromBase);
			}
			catch (Exception)
			{
			}
			return v;
		}

		protected bool IsValidSeq(int seq)
		{
			return !(this.dongles.IsNullOrEmpty() || seq > this.dongles.Length - 1);
		}

		//protected Int64 GetDongleHandler(int seq)
		//{
		//	if (IsValidSeq(seq))
		//		return this.dongles[seq].hDongle;

		//	return -1;
		//}



		//public bool CreateAuthenKeyFile(ushort descriptor, byte[] inData, out byte[] outData)
		//{
		//	PRIKEY_FILE_ATTR priAttr = CreatePrikeyFileAttr();
		//	IntPtr ptr = CreateIntPtr(priAttr);

		//	try
		//	{
		//		this.LastErrorCode = Dongle_CreateFile(hDongle, DongleFileType.FILE_PRIKEY_RSA, descriptor,  ptr);
		//	}
		//	finally
		//	{
		//		FreeIntPtr(ptr);
		//	}

		//	IntPtr pRsaPub = CreateIntPtr(Marshal.SizeOf(typeof(RSA_PUBLIC_KEY)) );
		//	IntPtr pRsaPri = CreateIntPtr(Marshal.SizeOf(typeof(RSA_PRIVATE_KEY)));

		//	//this.LastErrorCode = Dongle_RsaGenPubPriKey(hDongle, DongleKey.AUTHEN_KEY_DESCRIPTOR, ref rsaPub, ref rsaPri);
		//	this.LastErrorCode = Dongle_RsaGenPubPriKey(hDongle, DongleKey.AUTHEN_KEY_DESCRIPTOR, pRsaPub, pRsaPri);

		//	RSA_PUBLIC_KEY rsaPub = (RSA_PUBLIC_KEY)Marshal.PtrToStructure(pRsaPub, typeof(RSA_PUBLIC_KEY));
		//	RSA_PRIVATE_KEY rsaPri = (RSA_PRIVATE_KEY)Marshal.PtrToStructure(pRsaPri, typeof(RSA_PRIVATE_KEY));

		//	FreeIntPtr(pRsaPub);
		//	FreeIntPtr(pRsaPri);

		//	LogRsaKey(rsaPub, rsaPri);
		//	outData = rsaPub.exponent;

		//	return IsSucc();
		//}

		//public override bool Encrypt(AuthenKeyType keyType, byte[]plain, out byte[] cipher)
		//{
		//	cipher = null;

		//	switch (keyType)
		//	{
		//		case AuthenKeyType.COMPANY_SEED:
		//			TDesEncrypt(DongleKey.COMPANY_SEED_KEY_DESCRIPTOR, plain, out cipher);
		//			break;

		//		case AuthenKeyType.USER_ROOT:
		//			TDesEncrypt(DongleKey.USER_ROOT_KEY_DESCRIPTOR, plain, out cipher);
		//			break;

		//		case AuthenKeyType.AUTHEN:
		//			RsaPriEncrypt(DongleKey.AUTHEN_KEY_DESCRIPTOR, plain, out cipher);
		//			break;

		//		default:
		//			break;
		//	}

		//	return IsSucc();
		//}

		//private void RsaPriEncrypt(ushort descriptor, byte[] plain, out byte[] cipher)
		//{
		//	uint nOutDataLen = RSA_KEY_LEN;
		//	cipher = new byte[nOutDataLen];
		//	this.LastErrorCode = Dongle_RsaPri(this.hDongle, descriptor, 0, plain, (uint)plain.Length, cipher, ref nOutDataLen);
		//}

		//public void RsaPubDecrypt(byte[] pubKey, byte[] plain, out byte[] cipher)
		//{
		//	uint nOutDataLen = RSA_KEY_LEN;
		//	cipher = new byte[nOutDataLen];

		//	RSA_PUBLIC_KEY rsaPub = new RockeyArm.DongleKey.RSA_PUBLIC_KEY();

		//	rsaPub.bits = RSA_KEY_LEN * 8 ;
		//	rsaPub.modulus = 65537;
		//	rsaPub.exponent = new byte[256];
		//	Array.Copy(pubKey, rsaPub.exponent, RSA_KEY_LEN);

		//	this.LastErrorCode = Dongle_RsaPub(this.hDongle, 1, ref rsaPub, plain, (uint)plain.Length, cipher, ref nOutDataLen);
		//}


		//private void LogRsaKey(RSA_PUBLIC_KEY rsaPub, RSA_PRIVATE_KEY rsaPri)
		//{
		//	logger.Debug(String.Format("RSA-PRI : bit={0},modulus={1}", rsaPri.bits, rsaPri.modulus));
		//	logger.Debug("RSA-PRI : " + Convert.ToBase64String(rsaPri.exponent));
		//	logger.Debug("    128 : " + Convert.ToBase64String(rsaPri.exponent, 0, RSA_KEY_LEN));
		//	logger.Debug("   -PUB : " + Convert.ToBase64String(rsaPri.publicExponent));
		//	logger.Debug("   -128 : " + Convert.ToBase64String(rsaPri.publicExponent, 0, RSA_KEY_LEN));


		//	logger.Debug(String.Format("RSA-PUB : bit={0},modulus={1}", rsaPub.bits, rsaPub.modulus));
		//	logger.Debug("RSA-PUB : " + Convert.ToBase64String(rsaPub.exponent, 0, RSA_KEY_LEN));
		//	logger.Debug("     (*): " + HexString.ToHexString(rsaPub.exponent));
		//}

		//private byte[] StructToBytes(object structObj)
		//{
		//	int size = Marshal.SizeOf(structObj);
		//	byte[] _bytes = new byte[size];
		//	IntPtr structPtr = Marshal.AllocHGlobal(size);
		//	Marshal.StructureToPtr(structObj, structPtr, false);
		//	Marshal.Copy(structPtr, _bytes, 0, size);
		//	Marshal.FreeHGlobal(structPtr);
		//	return _bytes;
		//}
	}
}