using Bluemoon;
using RonftonCard.Common.AuthenKey;
using System;
using System.Runtime.CompilerServices;

namespace RonftonCard.AuthenKey.RockeyArm
{
	public partial class DongleKey
	{

		#region "--- implement AbstractAuthenKey ---"

		/// <summary>
		/// Close device
		/// </summary>
		public override void Close()
		{
			if (hDongle > 0)
			{
				Dongle_Close(hDongle);
				this.hDongle = -1;
			}
		}

		/// <summary>
		/// open specified key by seq , and first is default
		/// </summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override bool Open(int seq = 0)
		{
			//avoid to re-open again
			if (this.hDongle > 0)
				return true;

			this.LastErrorCode = Dongle_Open(ref this.hDongle, seq);
			return IsSucc();
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
		public override bool Restore(byte[] adminPin)
		{
			if (!Authen(AuthenMode.ADMIN, adminPin))
				return false;

			this.LastErrorCode = Dongle_RFS(this.hDongle);
			return IsSucc();
		}
		#endregion

		/// <summary>
		/// Create user root key
		/// </summary>
		public override ResultArgs CreateUserRootKey(String userId, String appId, byte[] userRootKey)
		{
			/*
			1、唯一化(seed)
			2、创建用户文件
			3、创建用户根密钥文件
			4、写入用户根密钥(*)
			5、修改APP_ID(*)
			6、修改USER_ID(*)
			*/
			byte[] newAdminPin, newAppId;

			if (!Initialize(out newAdminPin, out newAppId))
				return new ResultArgs(false);

			if (!Authen(AuthenMode.ADMIN, newAdminPin))
				return new ResultArgs(false);

			if (userId.Length % 2 != 0)
				userId = "0" + userId;

			// HexString ---> to uint ...
			// set user id

			// update admin pin

			//CreateKeyFile(AuthenKeyConst.USER_ROOT_KEY_DESCRIPTOR, userRootKey);
			return new ResultArgs(true)
			{
				Result = new UserRootKeyResponse
				{
					NewAdminPin = encoding.GetString(newAdminPin),
					AppId = encoding.GetString(newAppId)
				}
			};
		}
	}
}