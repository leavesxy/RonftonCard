using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Common.AuthenKey
{
	public class AuthenKeyInfo
	{
		public String Version { get; set; }
		public String ProductId { get; set; }
		public String UserInfo { get; set; }
		public String KeyId { get; set; }

		public static AuthenKeyInfo ErrorKey()
		{
			return new AuthenKeyInfo()
			{
				Version = "No Key found!",
				ProductId = "",
				UserInfo = "",
				KeyId = ""
			};
		}

		public override String ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(this.Version).Append(",");
			sb.Append(this.ProductId).Append(",");
			sb.Append(this.UserInfo).Append(",");
			sb.Append(this.KeyId);
			return sb.ToString();
		}
	}
}