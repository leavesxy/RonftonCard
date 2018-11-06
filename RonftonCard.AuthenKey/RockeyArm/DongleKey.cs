﻿using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using BlueMoon;
using System.Runtime.InteropServices;
using RonftonCard.Common.AuthenKey;

namespace RonftonCard.AuthenKey.RockeyArm
{
	public partial class DongleKey  : AbstractAuthenKey
	{
		private const int RSA_KEY_LEN = 128;

		// handle for dog
		private Int64 hDongle;
		
		public DongleKey()
			: base("ErrorMessage.properties")
		{
			this.hDongle = -1;
			
			// set base.succ, default succ = 0x00;
		}

		#region "--- implement abstract ---"

		/// <summary>
		/// Close device
		/// </summary>
		public override void Close()
		{
			if( hDongle > 0 )
			{
				Dongle_Close(hDongle);
				this.hDongle = -1;
			}
		}

		/// <summary>
		/// open special sequence key, and first is default
		/// </summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override bool Open(int sequence = 0)
		{
			//avoid to re-open again
			if (this.hDongle > 0)
			{
				Close();
				this.hDongle = -1;
			}

			this.LastErrorCode = Dongle_Open(ref this.hDongle, sequence);
			return IsSucc();
		}

		/// <summary>
		/// enumerate all keys
		/// </summary>
		public override AuthenKeyInfo[] Enumerate()
		{
			long count = 0;
			this.LastErrorCode = Dongle_Enum(IntPtr.Zero, out count);

			//no key found
			if (!IsSucc() || count <= 0)
				return null;

			logger.Debug(String.Format("found {0} Dogs !", count));

			List<AuthenKeyInfo> keyInfo = new List<AuthenKeyInfo>();
			IntPtr pDongleInfo = IntPtr.Zero;

			try
			{
				int size = Marshal.SizeOf(typeof(DONGLE_INFO));
				pDongleInfo = CreateIntPtr(size * (int)count);
				this.LastErrorCode = Dongle_Enum(pDongleInfo, out count);

				for (int i = 0; i < count; i++)
				{
					IntPtr ptr = new IntPtr(pDongleInfo.ToInt64() + i * size);
					DONGLE_INFO dongleInfo = (DONGLE_INFO)Marshal.PtrToStructure(ptr, typeof(DONGLE_INFO));

					AuthenKeyInfo key = new AuthenKeyInfo()
					{
						Seq = (short)i,
						Version = String.Format("v{0}.{1:d2}-({2:x2},{3})",
								dongleInfo.m_Ver >> 8 & 0xff,
								dongleInfo.m_Ver & 0xff,
								dongleInfo.m_Type,
								BitConverter.ToString(dongleInfo.m_BirthDay)),
						UserInfo = dongleInfo.m_UserID.ToString("X08"),
						ProductId = dongleInfo.m_PID.ToString("X08"),
						KeyId = BitConverter.ToString(dongleInfo.m_HID)
					};
					keyInfo.Add(key);
					logger.Debug(key.ToString());
				}
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
			}
			finally
			{
				FreeIntPtr(pDongleInfo);
			}
			return keyInfo.ToArray();
		}

		#endregion

		#region "--- util ---"
		
		/// <summary>
		/// convert lastErrorCode to ErrorMsg key
		/// </summary>
		protected override String GetErrorMsgKey()
		{
			return String.Format("0x{0:X8}", this.LastErrorCode);
		}

		/// <summary>
		/// Create IntPtr by structure
		/// </summary>
		private IntPtr CreateIntPtr(Object stru)
		{
			try
			{
				int size = Marshal.SizeOf(stru.GetType());
				IntPtr ptr = Marshal.AllocHGlobal(size);

				//should set true, if set false, maybe cause memory leaks
				Marshal.StructureToPtr(stru, ptr, true);
				return ptr;
			}
			catch (Exception ex)
			{ }
			return IntPtr.Zero;
		}

		/// <summary>
		/// Create IntPtr with special size
		/// </summary>
		private IntPtr CreateIntPtr(int count)
		{
			return Marshal.AllocHGlobal(count);
		}

		/// <summary>
		/// free IntPtr
		/// </summary>
		private void FreeIntPtr(IntPtr ptr)
		{
			if (ptr != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(ptr);
				ptr = IntPtr.Zero;
			}
		}

		#endregion

		#region "--- extension function ---"
		/// <summary>
		/// restore to origin
		/// </summary>
		public bool Restore()
		{
			//if (!Authen(AuthenMode.ADMIN, pin))
			//	return false;
			this.LastErrorCode = Dongle_RFS(this.hDongle);
			return IsSucc();
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
		public bool Initialize(byte[] seed, out byte[] newAdminPwd, out byte[] pid)
		{
			pid = new byte[8];
			newAdminPwd = new byte[16];

			logger.Debug("Initialize Key , seed = " + BitConverter.ToString(seed));

			//unique key, requst admin privilege
			if (!Authen(AuthenMode.ADMIN, Encoding.Default.GetBytes(this.defaultAdminPin)))
				return false;

			this.LastErrorCode = Dongle_GenUniqueKey(hDongle, seed.Length, seed, pid, newAdminPwd);

			logger.Debug("new pid = " + Encoding.ASCII.GetString(pid));
			logger.Debug("new admin pwd = " + Encoding.ASCII.GetString(newAdminPwd) );

			return IsSucc();
		}

		#endregion

		public override bool Create(AuthenKeyType keyType, byte[] inData, out byte[] outData)
		{
			outData = null;

			switch(keyType)
			{
				case AuthenKeyType.COMPANY_SEED:
					CreateKeyFile(DongleKey.COMPANY_SEED_KEY_DESCRIPTOR, inData);
					break;

				case AuthenKeyType.USER_ROOT:
					CreateKeyFile(DongleKey.USER_ROOT_KEY_DESCRIPTOR, inData);
					break;

				case AuthenKeyType.AUTHEN:
					CreateAuthenKeyFile(DongleKey.AUTHEN_KEY_DESCRIPTOR, inData, out outData);
					break;

				default:
					break;
			}

			return IsSucc();
		}

		/// <summary>
		/// create company seed key file,and seedKey is request 16 bytes at least
		/// </summary>
		private bool CreateKeyFile(ushort descriptor, byte[] seedKey)
		{
			KEY_FILE_ATTR keyAttr = new KEY_FILE_ATTR();
			keyAttr.m_Size = 16;
			keyAttr.m_Lic.m_Priv_Enc = 0;

			IntPtr ptr = CreateIntPtr(keyAttr);
			this.LastErrorCode = Dongle_CreateFile(hDongle, DongleFileType.FILE_KEY, descriptor, ptr);
			FreeIntPtr(ptr);

			if (!IsSucc())
				return false;

			// dongle key require 16 bytes key
			// fill zero if seedKey is less than 16 bytes
			byte[] key = ArrayHelper.CopyFrom<byte>(seedKey, 16);
			logger.Debug(String.Format("Create Key file, descriptor={0}, key={1}", descriptor, BitConverter.ToString(key)));

			this.LastErrorCode = Dongle_WriteFile(hDongle, DongleFileType.FILE_KEY, descriptor, 0, key, 16);

			return IsSucc();
		}

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
			cipher = ArrayHelper.CopyFrom<byte>(plain, len);

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

		//public bool SetUserID(uint uid)
		//{
		//	this.LastErrCode = Dongle_SetUserID(this.hDongle, uid);
		//	return this.LastErrCode == DongleKey.SUCC;
		//}

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