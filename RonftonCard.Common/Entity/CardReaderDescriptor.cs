using BlueMoon;
using RonftonCard.Common.Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Common.Entity
{
	public class CardReaderDescriptor
	{
		public String Name { get; set; }
		public String Desc { get; set; }
		public PortType PortType { get; set; }

		[Alias("type")]
		public String DrvType { get; set; }

		public override String ToString()
		{
			return String.Format("name = {0}, port = {1}, desc = {2}, type={3}",
				this.Name,
				this.PortType.ToString(),
				this.Desc,
				this.DrvType);
		}
	}
}