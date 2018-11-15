using Bluemoon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.Config
{
	public class DongleDescriptor
	{
		public String Name { get; set; }

		public String Desc { get; set; }

		public String Charset { get; set; }

		public String Seed { get; set; }

		[Alias("type")]
		public String DrvType { get; set; }
	}
}
