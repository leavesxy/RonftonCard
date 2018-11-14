using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Common.AuthenKey
{
	public class UserRootKeyResponse
	{
		public String NewAdminPin { get; set; }

		public String AppId { get; set; }
		
		public String VerifyText { get; set; } 
	}
}
