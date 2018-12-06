using System;
using System.Runtime.InteropServices;

namespace RonftonCard.Dongle.RockeyArm
{
	using Bluemoon;
	using Core.Dongle;
	using Core.DTO;
	using DONGLE_HANDLER = Int64;

	public partial class RockeyArmDongle
	{
		#region "--- initialize dongle ---"

		/// <summary>
		/// initialize a empty dongle
		/// after invoke SUCC,status of key is anonymous
		/// </summary>
		private bool unique(DONGLE_HANDLER hDongle, out byte[] newAdminPwd, out byte[] appId)
		{
			appId = new byte[8];
			newAdminPwd = new byte[16];

			logger.Debug("Initialize Key , seed = " + BitConverter.ToString(this.seed));

			//unique key, requst admin privilege
			if (!Authen(hDongle, DongleAuthenMode.ADMIN, this.encoder.GetBytes(this.defaultAdminPin)))
			{
				logger.Debug("Authen failed use default admin pin!");
				return false;
			}

			this.lastErrorCode = Dongle_GenUniqueKey(hDongle, seed.Length, seed, appId, newAdminPwd);

			logger.Debug("new pid = " + this.Encoder.GetString(appId));
			logger.Debug("new admin pwd = " + this.Encoder.GetString(newAdminPwd));

			return IsSucc;
		}

		/// <summary>
		/// unique Key, and login with new admin pin
		/// </summary>
		private bool Initialize(DONGLE_HANDLER hDongle, String userId, out byte[] newAdminPin, out byte[] appId)
		{
			// unique key
			if (!unique(hDongle, out newAdminPin, out appId))
				return false;

			// re-authen
			if (!Authen(hDongle, DongleAuthenMode.ADMIN, newAdminPin))
				return false;

			this.lastErrorCode = Dongle_SetUserID(hDongle, ToUint32(userId));
			return true;
		}

		#endregion

		#region "--- Create KEY information ---"
		public bool CreateKeyInfo(DONGLE_HANDLER hDongle, DongleKeyInfo keyInfo)
		{
			if (!CreateKeyInfoFile(hDongle))
				return false;

			DongleKeyInfoStru stru = CreateDongleKeyInfoStru(keyInfo);

			IntPtr ptr = IntPtrUtil.CreateByStru(stru);
			byte[] dest = new byte[IntPtrUtil.SizeOf(stru.GetType())];

			Marshal.Copy(ptr, dest, 0, dest.Length);
			this.lastErrorCode = Dongle_WriteFile(
						hDongle, 
						RockeyArmFileType.FILE_DATA, 
						DongleConst.KEY_INFO_DESCRIPTOR,
						0,
						dest,
						dest.Length);
			IntPtrUtil.Free(ref ptr);
			return IsSucc;
		}

		private DongleKeyInfoStru CreateDongleKeyInfoStru(DongleKeyInfo keyInfo)
		{
			DongleKeyInfoStru stru = new DongleKeyInfoStru();
			stru.DongleType = (byte)keyInfo.DongleType;
			stru.UserId = ArrayUtil.CopyFrom(this.encoder.GetBytes(keyInfo.UserId), 6);
			stru.UserName = ArrayUtil.CopyFrom(this.encoder.GetBytes(keyInfo.UserName), 64);
			stru.CreateDate = ArrayUtil.CopyFrom(this.encoder.GetBytes(keyInfo.CreateDate), 14);
			stru.Operator = ArrayUtil.CopyFrom(this.encoder.GetBytes(keyInfo.Operator), 3);
			return stru;
		}

		private bool CreateKeyInfoFile(DONGLE_HANDLER hDongle)
		{
			DATA_FILE_ATTR dataAttr;
			dataAttr.m_Size = DongleConst.KEY_INFO_FILE_LEN;
			//allow anonymous read
			dataAttr.m_Lic.m_Read_Priv = 0;
			//only admin could write
			dataAttr.m_Lic.m_Write_Priv = 2;

			IntPtr ptr = IntPtrUtil.CreateByStru(dataAttr);
			this.lastErrorCode = Dongle_CreateFile(hDongle, RockeyArmFileType.FILE_DATA, DongleConst.KEY_INFO_DESCRIPTOR, ptr);
			IntPtrUtil.Free(ref ptr);

			return IsSucc;
		}

		#endregion

		#region "--- User root Key ---"
		/// <summary>
		/// Create user root key
		/// userRootKey can't less 16 bytes
		/// </summary>
		public ResultArgs CreateUserRootKey(String userId, byte[] userRootKey, DongleKeyInfo keyInfo)
		{
			ResultArgs ret = new ResultArgs(false);

			if (!IsActive())
			{
				ret.Msg = "Create user root key failed -- no dongle opened!";
				logger.Debug(ret.Msg);
				return ret;
			}

			byte[] newAdminPin;
			byte[] appId;

			if (!Initialize(this.hDongle, userId, out newAdminPin, out appId))
			{
				ret.Msg = "initialize dongle failed ";
				return ret;
			}

			if (!CreateKeyInfo(this.hDongle, keyInfo) ||
				!CreateKeyFile(this.hDongle, DongleConst.USER_ROOT_KEY_DESCRIPTOR, userRootKey))
			{
				ret.Msg = "Create User root Key failed !";
				return ret;
			}

			// renew appid
			this.dongleInfo[this.selectedIndex].AppId = this.Encoder.GetString(appId);

			// reset authen status as anonymous
			Reset();
			
			ret.Succ = true;
			ret.Result = new UserRootKeyResponse
			{
				KeyPwd = this.Encoder.GetString(newAdminPin),
				AppId = this.dongleInfo[this.selectedIndex].AppId,
				KeyId = this.dongleInfo[this.selectedIndex].KeyId,
				Version = this.dongleInfo[this.selectedIndex].Version,
				UserId = userId,
				TestCipher = CreateTestCipher(userId)
			};
			return ret;
		}

		private String CreateTestCipher(String uid)
		{
			// compute test cipher ( encrypt userId with root_key )
			// test cipher is same for same user id
			byte[] uidBytes = this.encoder.GetBytes(uid);
			byte[] testCipher;
			
			if (Encrypt(uidBytes, out testCipher))
			{
				return HexString.ToHexString(testCipher);
			}

			return "Error";
		}


		/// <summary>
		/// create User root key file,and Key is request 16 bytes at least
		/// </summary>
		private bool CreateKeyFile(DONGLE_HANDLER hDongle, ushort fileDescriptor, byte[] userRootkey)
		{
			KEY_FILE_ATTR keyAttr = new KEY_FILE_ATTR();
			keyAttr.m_Size = 16;
			keyAttr.m_Lic.m_Priv_Enc = 0;

			IntPtr ptr = IntPtrUtil.CreateByStru(keyAttr);
			this.lastErrorCode = Dongle_CreateFile(hDongle, RockeyArmFileType.FILE_KEY, fileDescriptor, ptr);
			IntPtrUtil.Free(ref ptr);

			if (!IsSucc)
				return false;

			// dongle key require 16 bytes key
			logger.Debug(String.Format("Create Key file, descriptor={0}, key={1}", fileDescriptor, BitConverter.ToString(userRootkey)));

			this.lastErrorCode = Dongle_WriteFile(hDongle, RockeyArmFileType.FILE_KEY, fileDescriptor, 0, userRootkey, 16);

			return IsSucc;
		}
		#endregion

		#region "--- Authen Key ---"
		public ResultArgs CreateAuthenKey(String userId, DongleKeyInfo keyInfo)
		{
			ResultArgs ret = new ResultArgs(false);

			if (!IsActive())
			{
				ret.Msg = "Create authen key failed -- no dongle opened!";
				logger.Debug(ret.Msg);
				return ret;
			}

			byte[] newAdminPin;
			byte[] appId;

			if (!Initialize(this.hDongle, userId, out newAdminPin, out appId))
			{
				ret.Msg = "initialize dongle failed ";
				return ret;
			}

			if (!CreateKeyInfo(this.hDongle, keyInfo) ||
				!CreatePrikeyFile(this.hDongle) )
			{
				ret.Msg = "Create Key info file failed !";
				return ret;
			}

			return CreatePriKey(userId,appId, newAdminPin);
		}

		private ResultArgs CreatePriKey(String userId, byte[] appId, byte[] adminPin)
		{
			IntPtr pubKey = IntPtrUtil.Create(IntPtrUtil.SizeOf(typeof(RSA_PUBLIC_KEY)));
			IntPtr priKey = IntPtrUtil.Create(IntPtrUtil.SizeOf(typeof(RSA_PRIVATE_KEY)));
			ResultArgs ret = new ResultArgs(true);

			try
			{
				this.lastErrorCode = Dongle_RsaGenPubPriKey(this.hDongle, DongleConst.AUTHEN_KEY_DESCRIPTOR, pubKey, priKey);
				LogKey(pubKey, priKey);

				// renew appid
				this.dongleInfo[this.selectedIndex].AppId = this.Encoder.GetString(appId);
				
				RSA_PUBLIC_KEY pubStru = IntPtrUtil.ToStru<RSA_PUBLIC_KEY>(pubKey);
				ret.Result = new AuthenKeyResponse()
				{
					PubKey = Convert.ToBase64String(pubStru.exponent, 0, DongleConst.RSA_KEY_LEN),
					Version = this.dongleInfo[this.selectedIndex].Version,
					AppId = this.dongleInfo[this.selectedIndex].AppId,
					UserId = userId,
					KeyPwd = this.Encoder.GetString(adminPin),
					KeyId = this.dongleInfo[this.selectedIndex].KeyId
				};
			}
			finally
			{
				IntPtrUtil.Free(ref pubKey);
				IntPtrUtil.Free(ref priKey);

				// reset authen status as anonymous
				Reset();
			}
			return ret;
			 
		}

		private void LogKey(IntPtr pubKey, IntPtr priKey)
		{
			RSA_PUBLIC_KEY pubStru = IntPtrUtil.ToStru<RSA_PUBLIC_KEY>(pubKey);
			RSA_PRIVATE_KEY priStru = IntPtrUtil.ToStru<RSA_PRIVATE_KEY>(priKey);

			logger.Debug(String.Format("RSA-PRI : bit={0},modulus={1}", priStru.bits, priStru.modulus));
			logger.Debug("RSA-PRI : " + Convert.ToBase64String(priStru.exponent, 0, DongleConst.RSA_KEY_LEN));
			logger.Debug(String.Format("RSA-PUB : bit={0},modulus={1}", priStru.bits, priStru.modulus));
			logger.Debug("RSA-PUB : " + Convert.ToBase64String(priStru.exponent, 0, DongleConst.RSA_KEY_LEN));
		}

		/// <summary>
		/// create RSA private key file
		/// </summary>
		private bool CreatePrikeyFile(DONGLE_HANDLER hDongle)
		{
			PRIKEY_FILE_ATTR priAttr = new PRIKEY_FILE_ATTR();

			priAttr.m_Size = DongleConst.RSA_KEY_LEN * 8;
			priAttr.m_Type = RockeyArmFileType.FILE_PRIKEY_RSA;
			priAttr.m_Lic.m_Count = 0xFFFFFFFF;
			priAttr.m_Lic.m_IsDecOnRAM = 0;
			priAttr.m_Lic.m_IsReset = 0;
			priAttr.m_Lic.m_Priv = 0;

			IntPtr ptr = IntPtrUtil.CreateByStru(priAttr);

			try
			{
				this.lastErrorCode = Dongle_CreateFile(hDongle, RockeyArmFileType.FILE_PRIKEY_RSA, DongleConst.AUTHEN_KEY_DESCRIPTOR, ptr);
			}
			finally
			{
				IntPtrUtil.Free(ref ptr);
			}
			return IsSucc;
		}
		#endregion

		#region "--- encrypt ---"
		public bool Encrypt(DongleType dongleType, byte[] plain, out byte[] cipher)
		{
			cipher = null;

			switch(dongleType)
			{
				case DongleType.USER_ROOT:
					return Encrypt(plain, out cipher);

				case DongleType.AUTHEN:
					return PriEncrypt(plain, out cipher);

				default:
					break;
			}
			return false;
		}

		private bool Encrypt(byte[] plain, out byte[] cipher)
		{
			cipher = null;

			if (!IsActive())
			{
				logger.Debug("Create authen key failed -- no dongle opened!");
				return false;
			}

			int len = (plain.Length % 16 == 0) ? plain.Length : (plain.Length / 16 + 1) * 16;

			// fill zero
			cipher = ArrayUtil.CopyFrom<byte>(plain, len);
			this.lastErrorCode = Dongle_TDES(this.hDongle, DongleConst.USER_ROOT_KEY_DESCRIPTOR, 0, cipher, cipher, (uint)len);
			logger.Debug(String.Format("plain = [ {0} ], cipher = [ {1} ]", BitConverter.ToString(plain), BitConverter.ToString(cipher)));

			return IsSucc;
		}

		public bool PriEncrypt(byte[] plain, out byte[] cipher)
		{
			uint nOutDataLen = DongleConst.RSA_KEY_LEN;
			cipher = new byte[nOutDataLen];

			if (!IsActive())
			{
				logger.Debug("Create authen key failed -- no dongle opened!");
				return false;
			}

			this.lastErrorCode = Dongle_RsaPri(this.hDongle, DongleConst.AUTHEN_KEY_DESCRIPTOR, 0, plain, (uint)plain.Length, cipher, ref nOutDataLen);

			return IsSucc;
		}
		#endregion
	}
}