using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.DTO
{
	public class DescriptorKey
	{
		public UInt16 Descriptor { get; set; }
		public byte[] ReadKey { get; set; }
		public byte[] WriteKey { get; set; }
		public byte[] ControlBlock { get; set; }
	}
}
