using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.Dongle
{
	public class DongleInfo
	{
		/// <summary>
		/// sequence number for enumerate
		/// </summary>
		public short Seq { get; set; }

		/// <summary>
		/// version of KEY
		/// Dongle: COS_VER + type + factory_date
		/// </summary>
		public String Version { get; set; }

		/// <summary>
		/// only for authen_key
		/// </summary>
		public String AppId { get; set; }

		/// <summary>
		/// for user
		/// </summary>
		public String UserId { get; set; }

		/// <summary>
		/// hardware id
		/// </summary>
		public String KeyId { get; set; }

		public String GetName()
		{
			return this.Seq.ToString() + ": id=[" + this.KeyId + "], pid=[" + AppId + "], uid=[" + UserId + "]";
		}

		public override String ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(this.Seq).Append(" : ");
			sb.Append(this.Version).Append(",");
			sb.Append(this.AppId).Append(",");
			sb.Append(this.UserId).Append(",");
			sb.Append(this.KeyId);
			return sb.ToString();
		}
	}
}