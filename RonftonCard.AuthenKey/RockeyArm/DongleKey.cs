using RonftonCard.Common.Authen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.AuthenKey.RockeyArm
{
	public partial class DongleKey : IAuthenKey
	{
		// handle for dog
		private Int64 hDongle;

		public DongleKey()
		{
			this.hDongle = -1;
		}

		#region "--- Close device ---"
		public void Close()
		{
			if( hDongle > 0 )
			{
				Dongle_Close(hDongle);
				this.hDongle = -1;
			}
		}

		public void Dispose()
		{
			Close();
		}
		#endregion

		public KeyInfo GetKeyInfo()
		{
			DONGLE_INFO pDongleInfo = new DONGLE_INFO();
			long count = 0;

			uint ret = Dongle_Enum(ref pDongleInfo, out count);
			if (ret != 0)
			{
				return KeyInfo.ErrorKey();
			}

			return new KeyInfo()
			{
				KeyVer = String.Format("v{0}.{1:d2}-({2:x2},{3}",
					pDongleInfo.m_Ver >> 8 & 0xff,
					pDongleInfo.m_Ver & 0xff,
					pDongleInfo.m_Type,
					BitConverter.ToString(pDongleInfo.m_BirthDay)
					),
				UserInfo = pDongleInfo.m_UserID.ToString("X08"),
				ProductId = pDongleInfo.m_PID.ToString("X08"),
				KeyId = BitConverter.ToString(pDongleInfo.m_HID)
			};
		}

		[MethodImpl(MethodImplOptions.Synchronized)]
		public bool Open(int sequence=0 )
		{
			if (this.hDongle > 0)
				return true;

			uint ret = Dongle_Open(ref this.hDongle, sequence);
			if (ret == 0)
				return true;

			return false;
		}

		bool Authen(AuthenMode authenMode, byte[] pin)
		{
			uint flag = authenMode == AuthenMode.ADMIN ? (uint)1 : (uint)0;
			int pRemainCount;
			uint ret = Dongle_VerifyPIN(this.hDongle, flag, pin, out pRemainCount);
			if (ret == 0)
				return true;
			return false;
		}

		public bool Initialize(byte[] defaultAdminPwd, out byte[] seed, out byte[] newAdminPwd, out byte[] pid)
		{
			seed = new byte[32];
			pid = new byte[9];
			newAdminPwd = new byte[17];

			if (!Authen(AuthenMode.ADMIN, defaultAdminPwd))
				return false;

			//unique key, requst admin privilege
			Array.Clear()

			Array.Clear(cPid, 0, cPid.Length);
			Array.Clear(cAdminPin, 0, cAdminPin.Length);

			ret = Dongle_GenUniqueKey(hDongle, 32, seed, cPid, cAdminPin);
			if (ret != 0)
			{
				textBox1.Text += "Gen unique key Failed! Return value:" + ret.ToString("X") + "\r\n";
				return;

			}
			else
			{
				textBox1.Text += "Gen unique key Success! \r\n";
			}
		}
	}
}
