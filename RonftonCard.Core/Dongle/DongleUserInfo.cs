using System;
using RonftonCard.Core.Dongle;

namespace RonftonCard.Core.Dongle
{
	public class DongleUserInfo
	{
		public DongleType DongleType { get; set; }

		public String UserId { get; set; }

		public String UserName { get; set; }

		// yyyyMMddhhmmss
		public String CreateDate { get; set; }

		public String Operator { get; set; }

		public static DongleUserInfo CreateTestDongleKeyInfo(DongleType dongleType,String userId)
		{
			DongleUserInfo userInfo = new DongleUserInfo();
			userInfo.DongleType = dongleType;
			userInfo.UserId = userId;
			userInfo.UserName = "Test User";
			userInfo.CreateDate = DateTime.Now.ToString("yyyyMMddHHmmss");
			userInfo.Operator = "999";

			return userInfo;
		}
	}
}