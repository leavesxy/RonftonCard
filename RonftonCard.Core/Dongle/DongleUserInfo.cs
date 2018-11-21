using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.Dongle
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