using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.Entity
{
	public class UserRootKeyResponse
	{
		public String KeyPwd { get; set; }

		public String AppId { get; set; }

		public String Version { get; set; }

		public String UserId { get; set; }

		public String KeyId { get; set; }

		public String TestCipher { get; set; }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("AppId = ").Append(this.AppId).Append(",");
			sb.Append("KeyId = ").Append(this.KeyId).Append(",");
			sb.Append("UserId = ").Append(this.UserId).Append(",");
			sb.Append("KeyPwd = ").Append(this.KeyPwd).Append(",");
			sb.Append("Version = ").Append(this.Version);
			return sb.ToString();
		}
	}
}