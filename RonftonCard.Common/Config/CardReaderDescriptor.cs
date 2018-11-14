using Bluemoon;
using RonftonCard.Common.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Common.Config
{
	public class CardReaderDescriptor
	{
		public String Name { get; set; }
		public String Desc { get; set; }

		[Alias("port")]
		public PortType PortType { get; set; }

		/// <summary>
		/// only for com
		/// </summary>
		public int Baud { get; set; }

		[Alias("type")]
		public String DrvType { get; set; }

		public override String ToString()
		{
			return String.Format("name = {0}, port = {1}, baud = {2}, desc = {3}, type={4}",
				this.Name,
				this.PortType.ToString(),
				this.Baud,
				this.Desc,
				this.DrvType);
		}
	}
}