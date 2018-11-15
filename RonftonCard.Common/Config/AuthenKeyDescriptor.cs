using Bluemoon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Common.Config
{
	public class AuthenKeyDescriptor
	{
		public String Name { get; set; }

		public String Desc { get; set; }

		public String Charset { get; set; }

		[Alias("type")]
		public String DrvType { get; set; }
	}
}