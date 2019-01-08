using System;
using Bluemoon;
using RonftonCard.Core;
using RonftonCard.Core.Dongle;

namespace RonftonCard.DongleService
{
	public class DongleUtil
	{
		public static IDongle dongle;

		static DongleUtil()
		{
			dongle = ContextManager.GetDongle();
		}

		public static ResultArgs Restore(String keyPwd)
		{
			byte[] keyPwdBytes = dongle.Encoder.GetBytes(keyPwd);
			ResultArgs ret = dongle.Restore(keyPwdBytes) 
				? new ResultArgs(true, null, "OK")
				: new ResultArgs(false, null, dongle.LastErrorMessage);

			return ret;
		}
	}
}