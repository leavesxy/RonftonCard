using Bluemoon;
using RonftonCard.Core.Dongle;
using RonftonCard.Core.Entity;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace RonftonCard.Dongle.RockeyArm
{
	public partial class RockeyArmDongle
	{
		protected bool SetUserID(Int64 hDongle, uint uid)
		{
			this.LastErrorCode = Dongle_SetUserID(hDongle, uid);
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

		private bool Initialize(Int64 hDongle, String userId, out byte[] newAdminPin, out byte[] appId)
		{
			// unique key
			if (!unique(hDongle, out newAdminPin, out appId))
				return false;

			// re-authen
			if (!Authen(hDongle, AuthenMode.ADMIN, newAdminPin))
				return false;

			SetUserID(hDongle, ToUint32(userId));
			return true;
		}

		#region "--- User root Key Process ---"
		/// <summary>
		/// Create user root key
		/// userRootKey can't less 16 bytes
		/// </summary>
		public override ResultArgs CreateUserRootKey(String userId, byte[] userRootKey, int seq=0)
		{
			ResultArgs ret = new ResultArgs(false);

			if ( !IsValidSeq(seq) || !Open(seq) )
				return ret;

			byte[] newAdminPin;
			byte[] appId;

			Int64 hDongle = this.dongleInfo[seq].hDongle;

			if (!Initialize(hDongle, userId, out newAdminPin, out appId))
				return ret;

			if (!CreateKeyFile(hDongle, DongleConst.USER_ROOT_KEY_DESCRIPTOR, userRootKey))
				return ret;

			// renew appid
			this.dongleInfo[seq].AppId = this.Encoder.GetString(appId);

			ret.Succ = true;
			ret.Result = new UserRootKeyResponse
			{
				KeyPwd = this.Encoder.GetString(newAdminPin),
				AppId = this.dongleInfo[seq].AppId,
				KeyId = this.dongleInfo[seq].KeyId,
				Version = this.dongleInfo[seq].Version,
				UserId = userId
			};
			return ret;
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
		/// encrypt by user root key
		/// </summary>
		public override bool Encrypt(byte[] plain, out byte[] cipher, int seq = 0)
		{
			cipher = null;

			if (!IsValidSeq(seq) || !Open(seq) )
				return false;

			int len = (plain.Length % 16 == 0) ? plain.Length : (plain.Length / 16 + 1) * 16;

			// fill zero
			cipher = ArrayUtil.CopyFrom<byte>(plain, len);
			this.LastErrorCode = Dongle_TDES(this.dongleInfo[seq].hDongle, DongleConst.USER_ROOT_KEY_DESCRIPTOR, 0, cipher, cipher, (uint)len);
			logger.Debug(String.Format("plain = [ {0} ], cipher = [ {1} ]", BitConverter.ToString(plain), BitConverter.ToString(cipher)));

			// donot close dongle
			return Succ();
		}

		#endregion

		#region "--- Authen Key Process ---"
		public override ResultArgs CreateAuthenKey(String userId, int seq = 0)
		{
			ResultArgs ret = new ResultArgs(false);

			if (!IsValidSeq(seq) || !Open(seq))
				return ret;

			Int64 hDongle = this.dongleInfo[seq].hDongle;

			byte[] newAdminPin;
			byte[] appId;

			if (!Initialize(hDongle, userId, out newAdminPin, out appId))
				return ret;

			if (!CreatePrikeyFileAttr(hDongle))
				return ret;

			IntPtr pPubKey = IntPtrUtil.Create(IntPtrUtil.SizeOf(typeof(RSA_PUBLIC_KEY)));
			IntPtr pPriKey = IntPtrUtil.Create(IntPtrUtil.SizeOf(typeof(RSA_PRIVATE_KEY)));

			try
			{
				this.LastErrorCode = Dongle_RsaGenPubPriKey(hDongle, DongleConst.AUTHEN_KEY_DESCRIPTOR, pPubKey, pPriKey);

				RSA_PUBLIC_KEY pub = IntPtrUtil.ToStructure<RSA_PUBLIC_KEY>(pPubKey);
				RSA_PRIVATE_KEY pri = IntPtrUtil.ToStructure<RSA_PRIVATE_KEY>(pPriKey);

				LogKey(pub, pri);

				// renew appid
				this.dongleInfo[seq].AppId = this.Encoder.GetString(appId);

				ret.Succ = true;
				ret.Result = new AuthenKeyResponse()
				{
					PubKey = Convert.ToBase64String(pub.exponent, 0, DongleConst.RSA_KEY_LEN),
					Version = this.dongleInfo[seq].Version,
					AppId = this.dongleInfo[seq].AppId,
					UserId = userId,
					KeyPwd = this.Encoder.GetString(newAdminPin),
					KeyId = this.dongleInfo[seq].KeyId
				};
			}
			finally
			{
				IntPtrUtil.Free(ref pPubKey);
				IntPtrUtil.Free(ref pPriKey);
			}

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
		private bool CreatePrikeyFileAttr(Int64 hDongle)
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
				this.LastErrorCode = Dongle_CreateFile(hDongle, RockeyArmFileType.FILE_PRIKEY_RSA, DongleConst.AUTHEN_KEY_DESCRIPTOR, ptr);
			}
			finally
			{
				IntPtrUtil.Free(ref ptr);
			}

			return Succ();
		}

		public override bool PriEncrypt(byte[] plain, out byte[] cipher, int seq = 0)
		{
			uint nOutDataLen = DongleConst.RSA_KEY_LEN;
			cipher = new byte[nOutDataLen];

			if (!IsValidSeq(seq) || !Open(seq))
				return false;

			this.LastErrorCode = Dongle_RsaPri(this.dongleInfo[seq].hDongle, DongleConst.AUTHEN_KEY_DESCRIPTOR, 0, plain, (uint)plain.Length, cipher, ref nOutDataLen);

			// don't close dongle
			return Succ();
		}

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
		#endregion
	}
}