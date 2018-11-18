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

		#region "--- basic device operation ---"

		/// <summary>
		/// Close all dongle
		/// </summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override void Close()
		{
			if (this.Dongles.IsNullOrEmpty())
				return;

			for(int i=0;i<this.dongles.Length;i++)
			{
				if(this.dongles[i].hDongle != -1 )
				{ 
				   Dongle_Close(this.dongles[i].hDongle);
					this.dongles[i].hDongle = -1;
				}
			}
		}

		/// <summary>
		/// close specified dongle
		/// </summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override void Close(int seq)
		{
			if( !IsValidSeq(seq))
				return;

			if (this.dongles[seq].hDongle != -1)
			{
				Dongle_Close(this.dongles[seq].hDongle);
				this.dongles[seq].hDongle = -1;
			}
		}

		/// <summary>
		/// open specified key by seq , and first is default
		/// </summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override bool Open(int seq = 0)
		{
			if (!IsValidSeq(seq))
				return false;

			//avoid to re-open again
			if ( this.dongles[seq].hDongle > 0 )
				return true;

			this.LastErrorCode = Dongle_Open(ref this.dongles[seq].hDongle, seq);
			return Succ();
		}

		public override bool Enumerate()
		{
			if (this.dongles != null)
				Close();

			this.dongles = EnumerateDongle();
			return Succ();
		}

		/// <summary>
		/// convert lastErrorCode to ErrorMsg key
		/// </summary>
		protected override String GetErrorMsgKey()
		{
			return String.Format("0x{0:X8}", this.LastErrorCode);
		}

		/// <summary>
		/// restore current key, should use admin pin
		/// </summary>
		public override bool Restore(byte[] adminPin, int seq = 0)
		{
			if (!Open(seq))
				return false;

			if (!Authen(this.dongles[seq].hDongle, AuthenMode.ADMIN, adminPin))
				return false;

			this.LastErrorCode = Dongle_RFS(this.dongles[seq].hDongle);

			Close(seq);
			return Succ();
		}

		public override bool Reset(int seq=0)
		{
			if (!IsValidSeq(seq))
				return false;

			this.LastErrorCode = Dongle_ResetState(this.dongles[seq].hDongle);
			return Succ();
		}

		#endregion

		#region "--- User root Key Process ---"
		/// <summary>
		/// Create user root key
		/// </summary>
		public override ResultArgs CreateUserRootKey(String userId, String appId, byte[] userRootKey, int seq=0)
		{
			if (!Open(seq))
				return new ResultArgs(false, null, "Open Dongle error!");

			byte[] newAdminPin, newAppId;
			Int64 hDongle = this.dongles[seq].hDongle;

			try
			{
				// unique key
				if (!Initialize(hDongle, out newAdminPin, out newAppId))
					return new ResultArgs(false, null, "Initlialize Dongle error!");

				// re-authen
				if (!Authen(hDongle, AuthenMode.ADMIN, newAdminPin))
					return new ResultArgs(false, null, "Re-auth Dongle error!");

				// set user_id
				// 10009 => 0x 00 01 00 09
				if (!SetUserID(hDongle, ToUint32(appId)))
					return new ResultArgs(false, null, "Set Dongle User_ID error!");

				// update admin pin??

				if (!CreateKeyFile(hDongle, DongleConst.USER_ROOT_KEY_DESCRIPTOR, userRootKey))
					return new ResultArgs(false, null, "Create Dongle Key file error!");


			}
			finally
			{
				//Close(seq);
				Reset(seq);
			}

			return new ResultArgs(true)
			{
				Result = new UserRootKeyResponse
				{
					NewAdminPin = this.Encoder.GetString(newAdminPin),
					AppId = this.Encoder.GetString(newAppId)
				}
			};
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

			this.LastErrorCode = Dongle_TDES(this.dongles[seq].hDongle, DongleConst.USER_ROOT_KEY_DESCRIPTOR, 0, cipher, cipher, (uint)len);

			logger.Debug(String.Format("plain = [ {0} ], cipher = [ {1} ]", BitConverter.ToString(plain), BitConverter.ToString(cipher)));

			// donot close dongle

			return Succ();
		}

		#endregion

		#region "--- Authen Key Process ---"
		public override ResultArgs CreateAuthenKey(int seq = 0)
		{
			if (!IsValidSeq(seq))
				return new ResultArgs(false, null, "Invalid sequence !");

			if (!Open(seq))
				return new ResultArgs(false, null, "Can't open sequence !");

			PRIKEY_FILE_ATTR priAttr = CreatePrikeyFileAttr();
			IntPtr ptr = IntPtrUtil.CreateByStru(priAttr);

			try
			{
				this.LastErrorCode = Dongle_CreateFile(this.dongles[seq].hDongle, RockeyArmFileType.FILE_PRIKEY_RSA, DongleConst.AUTHEN_KEY_DESCRIPTOR, ptr);
			}
			finally
			{
				IntPtrUtil.Free(ref ptr);
			}

			IntPtr pPubKey = IntPtrUtil.Create(IntPtrUtil.SizeOf(typeof(RSA_PUBLIC_KEY)));
			IntPtr pPriKey = IntPtrUtil.Create(IntPtrUtil.SizeOf(typeof(RSA_PRIVATE_KEY)));

			this.LastErrorCode = Dongle_RsaGenPubPriKey(this.dongles[seq].hDongle,DongleConst.AUTHEN_KEY_DESCRIPTOR,pPubKey, pPriKey);

			RSA_PUBLIC_KEY pub = (RSA_PUBLIC_KEY)IntPtrUtil.ToStructure<RSA_PUBLIC_KEY>(pPubKey);
			RSA_PRIVATE_KEY pri = (RSA_PRIVATE_KEY)IntPtrUtil.ToStructure<RSA_PRIVATE_KEY>(pPriKey);

			IntPtrUtil.Free( ref pPubKey );
			IntPtrUtil.Free( ref pPriKey );

			LogKey(pub, pri);

			return new ResultArgs(true)
			{
				Result = new AuthenKeyResponse()
				{
					PubKey = Convert.ToBase64String(pub.exponent, 0, DongleConst.RSA_KEY_LEN)
				}
			};
		}

		private void LogKey(RSA_PUBLIC_KEY pubKey, RSA_PRIVATE_KEY priKey)
		{
			logger.Debug(String.Format("RSA-PRI : bit={0},modulus={1}", priKey.bits, priKey.modulus));
			logger.Debug("RSA-PRI : " + Convert.ToBase64String(priKey.exponent, 0, DongleConst.RSA_KEY_LEN));
			//logger.Debug("    128 : " + Convert.ToBase64String(priKey.exponent, 0, DongleConst.RSA_KEY_LEN));
			//logger.Debug("   -PUB : " + Convert.ToBase64String(priKey.publicExponent));
			//logger.Debug("   -128 : " + Convert.ToBase64String(priKey.publicExponent, 0, DongleConst.RSA_KEY_LEN));
			logger.Debug(String.Format("RSA-PUB : bit={0},modulus={1}", pubKey.bits, pubKey.modulus));
			logger.Debug("RSA-PUB : " + Convert.ToBase64String(pubKey.exponent, 0, DongleConst.RSA_KEY_LEN));
			//logger.Debug("     (*): " + HexString.ToHexString(pubKey.exponent));
		}

		/// <summary>
		/// create RSA private key file
		/// </summary>
		private PRIKEY_FILE_ATTR CreatePrikeyFileAttr()
		{
			PRIKEY_FILE_ATTR priAttr = new PRIKEY_FILE_ATTR();

			priAttr.m_Size = DongleConst.RSA_KEY_LEN * 8;
			priAttr.m_Type = RockeyArmFileType.FILE_PRIKEY_RSA;
			priAttr.m_Lic.m_Count = 0xFFFFFFFF;
			priAttr.m_Lic.m_IsDecOnRAM = 0;
			priAttr.m_Lic.m_IsReset = 0;
			priAttr.m_Lic.m_Priv = 0;
			return priAttr;
		}

		public override bool PriEncrypt(byte[] plain, out byte[] cipher, int seq = 0)
		{
			uint nOutDataLen = DongleConst.RSA_KEY_LEN;
			cipher = new byte[nOutDataLen];

			if (!IsValidSeq(seq) || !Open(seq))
				return false;

			this.LastErrorCode = Dongle_RsaPri(this.dongles[seq].hDongle, DongleConst.AUTHEN_KEY_DESCRIPTOR, 0, plain, (uint)plain.Length, cipher, ref nOutDataLen);

			// don't close dongle
			return Succ();
		}
		#endregion
	}
}