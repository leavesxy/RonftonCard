using RonftonCard.Common.Authen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Authen.RockeyArm
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
				KeyVer = String.Format("v{0,02d}.{1,02d}-({2,02x},{3}",
					pDongleInfo.m_Ver >> 8 & 0xff,
					pDongleInfo.m_Ver & 0xff,
					pDongleInfo.m_Type,
					pDongleInfo.m_BirthDay),
				UserInfo = pDongleInfo.m_UserID.ToString(),
				ProductId = pDongleInfo.m_PID.ToString(),
				KeyId = pDongleInfo.m_HID
			};
		}
	}
}
