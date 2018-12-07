using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.DTO
{
	public class CardInitializeResult
	{
		public byte[] SN { get; set; }

		public List<KeyValuePair<ushort,byte[]>> DescriptorKey;

	}
}