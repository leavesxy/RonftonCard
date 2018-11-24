using System;
using RonftonCard.Core.Dongle;

namespace RonftonCard.Core.DTO
{
	public class DongleUserInfo
	{
		public DongleType DongleType { get; set; }

		public String AppId { get; set; }

		public String UserId { get; set; }

		public String AppName { get; set; }

		public String UserName { get; set; }

		// yyyyMMddhhmmss
		public String CreateDate { get; set; }
		
	}
}