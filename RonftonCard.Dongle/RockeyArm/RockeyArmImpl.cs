using Bluemoon;
using RonftonCard.Core.Dongle;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace RonftonCard.Dongle.RockeyArm
{
	public partial class RockeyArmDongle
	{

		#region "--- implement AbstractAuthenKey ---"

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
			if (this.Dongles.IsNullOrEmpty() || seq > this.dongles.Length )
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
			//avoid to re-open again
			if ( this.dongles[seq].hDongle > 0 )
				return true;

			this.LastErrorCode = Dongle_Open(ref this.dongles[seq].hDongle, seq);
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
		public override bool Restore(byte[] adminPin)
		{
			if (!Authen(AuthenMode.ADMIN, adminPin))
				return false;

			this.LastErrorCode = Dongle_RFS(this.hDongle);
			return Succ();
		}

		public override bool Reset()
		{
			this.LastErrorCode = Dongle_ResetState(this.hDongle);
			return Succ();
		}

		#endregion

		/// <summary>
		/// Create user root key
		/// </summary>
		public override ResultArgs CreateUserRootKey(String userId, String appId, byte[] userRootKey)
		{
			byte[] newAdminPin, newAppId;
			ResultArgs ret = new ResultArgs(false);

			// unique key
			if (!Initialize(out newAdminPin, out newAppId))
				return ret;

			// re-authen
			if (!Authen(AuthenMode.ADMIN, newAdminPin))
				return ret;

			// set user_id
			// 10009 => 0x 00 01 00 09
			if (!SetUserID(ToUint32(appId)))
				return ret;

			// update admin pin??

			if(!CreateKeyFile(AuthenKeyConst.USER_ROOT_KEY_DESCRIPTOR, userRootKey) )
				return ret;

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