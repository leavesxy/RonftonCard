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

		/// <summary>
		/// detail information
		/// </summary>
		public String Description { get; set; }


		/// <summary>
		/// handler of device
		/// </summary>
		public Int64 hDongle;

		public DongleInfo()
		{
			this.hDongle = -1;
		}


		public String GetInfo()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("[" + this.Seq.ToString("d2") +"]").Append(" : ");
			sb.Append(this.KeyId).Append(",");
			sb.Append(this.Version).Append(",");
			sb.Append(this.AppId).Append(",");
			sb.Append(this.UserId).Append(",");
			sb.Append(this.Description);
			return sb.ToString();
		}

		public override String ToString()
		{
			return "["+ this.Seq.ToString("d2") + "] : " + "key_id=" + this.KeyId  + ", app_id=" + this.AppId + ",user_id="+ this.UserId ;
		}
	}
}