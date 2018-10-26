using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Common.Authen
{
	public class KeyInfo
	{
		public String KeyVer { get; set; }
		public String ProductId { get; set; }
		public String UserInfo { get; set; }
		public String KeyId { get; set; }

		public static KeyInfo ErrorKey()
		{
			return new KeyInfo()
			{
				KeyVer = "No Key found!",
				ProductId = "",
				UserInfo = "",
				KeyId = ""
			};
		}

		public override String ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("Key verstion = ").Append(this.KeyVer).Append(Environment.NewLine);
			sb.Append("Product id = ").Append(this.ProductId).Append(Environment.NewLine);
			sb.Append("User info = ").Append(this.UserInfo).Append(Environment.NewLine);
			sb.Append("Key id = ").Append(this.KeyId).Append(Environment.NewLine);
			return sb.ToString();
		}
	}
}