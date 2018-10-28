using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using BlueMoon;
using System.Runtime.InteropServices;
using log4net;
using BlueMoon.Config;
using RonftonCard.Common.AuthenKey;

namespace RonftonCard.AuthenKey.RockeyArm
{
	public partial class DongleKey  : AbstractAuthenKey
	{
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
		/// open first device as default
		/// </summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override bool Open(int sequence = 0)
		{
			//avoid to open again
			if (this.hDongle > 0)
				return true;

			//DONGLE_INFO pDongleInfo = new DONGLE_INFO();
			long count = 0;
			IntPtr pDongleInfo = IntPtr.Zero;
			return Dongle_Enum(pDongleInfo, out count) == 0 && Dongle_Open(ref this.hDongle, sequence) == 0;
		}

		public override AuthenKeyInfo[] Enumerate()
		{
			long count = 0;
			IntPtr pDongleInfo = IntPtr.Zero;
			this.LastErrorCode = Dongle_Enum(pDongleInfo, out count);

			//no key found
			if (!IsSucc() || count <= 0)
				return null;

			logger.Debug(String.Format("found {0} Dogs !", count));

			List<AuthenKeyInfo> keyInfo = new List<AuthenKeyInfo>();
			try
			{
				int size = Marshal.SizeOf(typeof(DONGLE_INFO));
				//pDongleInfo = Marshal.AllocHGlobal(size * (int)count);
				pDongleInfo = CreateIntPtr(size * (int)count);
				this.LastErrorCode = Dongle_Enum(pDongleInfo, out count);

				for (int i = 0; i < count; i++)
				{

					//DONGLE_INFO dongleInfo = (DONGLE_INFO)Marshal.PtrToStructure(new IntPtr(pDongleInfo.ToInt32() + i * size), typeof(DONGLE_INFO));
					IntPtr ptr = new IntPtr(pDongleInfo.ToInt64() + i * size);
					DONGLE_INFO dongleInfo = (DONGLE_INFO)Marshal.PtrToStructure(ptr, typeof(DONGLE_INFO));

					keyInfo.Add(new AuthenKeyInfo()
					{
						Version = String.Format("v{0}.{1:d2}-({2:x2},{3}",
								dongleInfo.m_Ver >> 8 & 0xff,
								dongleInfo.m_Ver & 0xff,
								dongleInfo.m_Type,
								BitConverter.ToString(dongleInfo.m_BirthDay)),
						UserInfo = dongleInfo.m_UserID.ToString("X08"),
						ProductId = dongleInfo.m_PID.ToString("X08"),
						KeyId = BitConverter.ToString(dongleInfo.m_HID)
					});
				}
			}
			catch (Exception ex)
			{ }
			finally
			{
				//if (pDongleInfo != IntPtr.Zero)
				//{
				//	Marshal.FreeHGlobal(pDongleInfo);
				//	pDongleInfo = IntPtr.Zero;
				//}
				FreeIntPtr(pDongleInfo);
			}
			return keyInfo.ToArray();
		}

		#endregion


		protected override String GetErrorMsgKey()
		{
			return String.Format("0x{0:X8}", this.LastErrorCode);
		}

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

			logger.Debug("new pid = " + BitConverter.ToString(pid));
			logger.Debug("new admin pwd = " + BitConverter.ToString(newAdminPwd));

			return IsSucc();
		}

		#endregion

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
			catch(Exception ex)
			{ }
			return IntPtr.Zero;
		}

		private IntPtr CreateIntPtr( int count )
		{
			return Marshal.AllocHGlobal(count);
		}

		private void FreeIntPtr(IntPtr ptr)
		{
			if (ptr != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(ptr);
				ptr = IntPtr.Zero;
			}
		}


		public override bool Create(AuthenKeyType keyType, byte[] inData)
		{
			switch(keyType)
			{
				case AuthenKeyType.COMPANY_SEED:
					CreateCompanySeedKeyFile(inData);
					break;

				case AuthenKeyType.USER_ROOT:
					
					break;
				case AuthenKeyType.AUTHEN:
					CreateAuthenKeyFile(inData);
					break;
				default:
					break;
			}

			return IsSucc();
		}

		/// <summary>
		/// create company seed key file,and seedKey is request 16 bytes at least
		/// </summary>
		private bool CreateCompanySeedKeyFile(byte[] seedKey)
		{
			KEY_FILE_ATTR keyAttr = new KEY_FILE_ATTR();
			keyAttr.m_Size = 16;
			keyAttr.m_Lic.m_Priv_Enc = 0;

			IntPtr ptr = CreateIntPtr(keyAttr);
			this.LastErrorCode = Dongle_CreateFile(hDongle, DongleFileType.FILE_KEY, DongleKey.COMPANY_SEED_KEY_DESCRIPTOR, ptr);
			FreeIntPtr(ptr);

			if (!IsSucc())
				return false;

			// dongle key require 16 bytes key
			// fill zero if seedKey is less than 16 bytes
			byte[] key = ArrayHelper.CopyFrom<byte>(seedKey, 16);
			this.LastErrorCode = Dongle_WriteFile(hDongle, DongleFileType.FILE_KEY, DongleKey.COMPANY_SEED_KEY_DESCRIPTOR, 0, key, 16);

			return IsSucc();
		}

		//public bool TDesEncrypt(String plain, out byte[] cipher)
		//{
		//	byte[] temp = Encoding.Default.GetBytes(plain);
		//	int len = (temp.Length % 16 == 0) ? temp.Length : (temp.Length / 16 + 1 ) * 16 ;

		//	// fill zero
		//	cipher = ArrayHelper.CopyFrom<byte>(temp, len);
		//	this.LastErrCode = Dongle_TDES(this.hDongle, DongleKey.TDES_FILE_DESCRIPTOR, 0, cipher, cipher, (uint)len);

		//	return this.LastErrCode == DongleKey.SUCC;
		//}

		//public bool SetUserID(uint uid)
		//{
		//	this.LastErrCode = Dongle_SetUserID(this.hDongle, uid);
		//	return this.LastErrCode == DongleKey.SUCC;
		//}

		public override bool Encrypt(AuthenKeyType keyType, byte[] inData, out byte[] outData)
		{
			outData = null;
			return IsSucc();
		}

		public bool CreateAuthenKeyFile(byte[] inData)
		{
			PRIKEY_FILE_ATTR priAttr = new PRIKEY_FILE_ATTR();

			//创建RSA私钥
			priAttr.m_Size = 1024;
			priAttr.m_Type = DongleFileType.FILE_PRIKEY_RSA;
			priAttr.m_Lic.m_Count = 0xFFFFFFFF;
			priAttr.m_Lic.m_IsDecOnRAM = 0;
			priAttr.m_Lic.m_IsReset = 0;
			priAttr.m_Lic.m_Priv = 0;

			IntPtr ptr = CreateIntPtr(priAttr);

			try
			{
				//int size = Marshal.SizeOf(typeof(PRIKEY_FILE_ATTR));
				//ptr = Marshal.AllocHGlobal(size);
				//Marshal.StructureToPtr(priAttr, ptr, true);

				this.LastErrorCode = Dongle_CreateFile(hDongle, DongleFileType.FILE_PRIKEY_RSA, DongleKey.AUTHEN_KEY_DESCRIPTOR, ptr);
			}
			finally
			{
				FreeIntPtr(ptr);
			}

			return IsSucc();
		}
	}
}