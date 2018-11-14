using Bluemoon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
		/// restore current key
		/// </summary>
		public bool Restore()
		{
			//if (!Authen(AuthenMode.ADMIN, pin))
			//	return false;
			this.LastErrorCode = Dongle_RFS(this.hDongle);
			return IsSucc();
		}
		#endregion

		/// <summary>
		/// Create user root key
		/// </summary>
		public ResultArgs CreateUserRootKey(String userId, String appId, byte[] seed, byte[] rootPin)
		{
			/*
			1、唯一化(seed)
			2、创建用户文件
			3、创建用户根密钥文件
			4、写入用户根密钥(*)
			5、修改APP_ID(*)
			6、修改USER_ID(*)
			*/

		}
	}
}
