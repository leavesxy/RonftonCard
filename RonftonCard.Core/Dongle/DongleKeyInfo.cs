using System;
using RonftonCard.Core.Dongle;

namespace RonftonCard.Core.Dongle
{
	public class DongleKeyInfo
	{
		public DongleType DongleType { get; set; }

		public String UserId { get; set; }

		public String UserName { get; set; }

		// yyyyMMddhhmmss
		public String CreateDate { get; set; }

		public String Operator { get; set; }

		public static DongleKeyInfo CreateTestDongleKeyInfo(DongleType dongleType,String userId)
		{
			DongleKeyInfo keyInfo = new DongleKeyInfo();
			keyInfo.DongleType = dongleType;
			keyInfo.UserId = userId;
			keyInfo.UserName = "Test User";
			keyInfo.CreateDate = DateTime.Now.ToString("yyyyMMddHHmmss");
			keyInfo.Operator = "999";

			return keyInfo;
		}
	}
}