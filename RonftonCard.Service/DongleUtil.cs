using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluemoon;
using RonftonCard.Core;
using RonftonCard.Core.Dongle;

namespace RonftonCard.Service
{
	public class DongleUtil
	{
		public static IDongle dongle;

		static DongleUtil()
		{
			dongle = ConfigManager.GetDongle();
		}

		public static ResultArgs Restore(String keyPwd)
		{
			byte[] keyPwdBytes = dongle.Encoder.GetBytes(keyPwd);
			ResultArgs ret = null;
			ret = dongle.Restore(keyPwdBytes) ? new ResultArgs(true, null, "OK") : new ResultArgs(false, null, dongle.LastErrorMessage);

			return ret;
		}
	}
}