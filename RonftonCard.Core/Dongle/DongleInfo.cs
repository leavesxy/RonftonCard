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
		/// hardware id
		/// </summary>
		public String KeyId { get; set; }

		/// <summary>
		/// COS_VER
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
		/// date of manufacture
		/// </summary>
		public String ManufactureDate { get; set; }

		/// <summary>
		/// model of this dongle
		/// </summary>
		public String Model { get; set; }

		/// <summary>
		/// detail information
		/// </summary>
		public String Description { get; set; }

		public String GetInfo()
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("[" + this.Seq.ToString("d2") +"]").Append(" : ");
			sb.Append(this.KeyId).Append(",");
			sb.Append(this.Version).Append(",");
			sb.Append(this.AppId).Append(",");
			sb.Append(this.UserId).Append(",");
			sb.Append(this.Model).Append(",");
			sb.Append(this.ManufactureDate).Append(",");
			sb.Append(this.Description);
			return sb.ToString();
		}

		public override String ToString()
		{
			return "["+ this.Seq.ToString("d2") + "] : " + "key_id=" + this.KeyId  + ", app_id=" + this.AppId + ",user_id="+ this.UserId ;
		}
	}
}