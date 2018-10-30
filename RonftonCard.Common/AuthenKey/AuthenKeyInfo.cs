using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Common.AuthenKey
{
	public class AuthenKeyInfo
	{
		public short Seq { get; set; }
		public String Version { get; set; }
		public String ProductId { get; set; }
		public String UserInfo { get; set; }
		public String KeyId { get; set; }

		public String GetName()
		{
			return this.Seq.ToString() + ": id=[" + this.KeyId + "], pid=[" + ProductId + "], uid=[" + UserInfo +"]";
		}

		public override String ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(this.Seq).Append(" : ");
			sb.Append(this.Version).Append(",");
			sb.Append(this.ProductId).Append(",");
			sb.Append(this.UserInfo).Append(",");
			sb.Append(this.KeyId);
			return sb.ToString();
		}
	}
}