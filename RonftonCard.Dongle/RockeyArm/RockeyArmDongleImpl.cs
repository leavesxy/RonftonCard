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

		#region "--- Create User information ---"
		public bool CreateUserInfo(DongleUserInfo userInfo)
		{
			return true;
		}
		#endregion

		#region "--- User root Key ---"
		/// <summary>
		/// Create user root key
		/// userRootKey can't less 16 bytes
		/// </summary>
		public ResultArgs CreateUserRootKey(String userId, byte[] userRootKey)
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

			if (!CreateKeyFile(this.hDongle, DongleConst.USER_ROOT_KEY_DESCRIPTOR, userRootKey))
			{
				ret.Msg = "Create Key file failed ";
				return ret;
			}

			// renew appid
			this.dongleInfo[this.selectedIndex].AppId = this.Encoder.GetString(appId);

			// reset authen status as anonymous
			Dongle_ResetState(this.hDongle);

			// compute test cipher ( encrypt userId with root_key )
			// test cipher is same for same user id
			byte[] uid = this.encoder.GetBytes(userId);
			byte[] testCipher;
			String cipherString=null;
			if (Encrypt(uid, out testCipher))
			{
				cipherString = HexString.ToHexString(testCipher);
			}

			ret.Succ = true;
			ret.Result = new UserRootKeyResponse
			{
				KeyPwd = this.Encoder.GetString(newAdminPin),
				AppId = this.dongleInfo[this.selectedIndex].AppId,
				KeyId = this.dongleInfo[this.selectedIndex].KeyId,
				Version = this.dongleInfo[this.selectedIndex].Version,
				UserId = userId,
				TestCipher = cipherString
			};
			return ret;
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
		public ResultArgs CreateAuthenKey(String userId)
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

			if (!CreatePrikeyFile(this.hDongle))
			{
				ret.Msg = "Create private key file failed !";
				return ret;
			}

			IntPtr pPubKey = IntPtrUtil.Create(IntPtrUtil.SizeOf(typeof(RSA_PUBLIC_KEY)));
			IntPtr pPriKey = IntPtrUtil.Create(IntPtrUtil.SizeOf(typeof(RSA_PRIVATE_KEY)));

			try
			{
				this.lastErrorCode = Dongle_RsaGenPubPriKey(hDongle, DongleConst.AUTHEN_KEY_DESCRIPTOR, pPubKey, pPriKey);

				RSA_PUBLIC_KEY pub = IntPtrUtil.ToStructure<RSA_PUBLIC_KEY>(pPubKey);
				RSA_PRIVATE_KEY pri = IntPtrUtil.ToStructure<RSA_PRIVATE_KEY>(pPriKey);

				LogKey(pub, pri);

				// renew appid
				this.dongleInfo[this.selectedIndex].AppId = this.Encoder.GetString(appId);

				ret.Succ = true;
				ret.Result = new AuthenKeyResponse()
				{
					PubKey = Convert.ToBase64String(pub.exponent, 0, DongleConst.RSA_KEY_LEN),
					Version = this.dongleInfo[this.selectedIndex].Version,
					AppId = this.dongleInfo[this.selectedIndex].AppId,
					UserId = userId,
					KeyPwd = this.Encoder.GetString(newAdminPin),
					KeyId = this.dongleInfo[this.selectedIndex].KeyId
				};
			}
			finally
			{
				IntPtrUtil.Free(ref pPubKey);
				IntPtrUtil.Free(ref pPriKey);
			}

			// reset authen status as anonymous
			Dongle_ResetState(this.hDongle);
			return ret;
		}

		private void LogKey(RSA_PUBLIC_KEY pubKey, RSA_PRIVATE_KEY priKey)
		{
			logger.Debug(String.Format("RSA-PRI : bit={0},modulus={1}", priKey.bits, priKey.modulus));
			logger.Debug("RSA-PRI : " + Convert.ToBase64String(priKey.exponent, 0, DongleConst.RSA_KEY_LEN));
			logger.Debug(String.Format("RSA-PUB : bit={0},modulus={1}", pubKey.bits, pubKey.modulus));
			logger.Debug("RSA-PUB : " + Convert.ToBase64String(pubKey.exponent, 0, DongleConst.RSA_KEY_LEN));
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