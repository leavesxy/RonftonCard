using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.DTO
{
	public class CardInitRequest
	{
		public String UserId { get; set; }

		public byte[] SN { get; set; }

		public UInt16 Sector { get; set; }
	}
}
