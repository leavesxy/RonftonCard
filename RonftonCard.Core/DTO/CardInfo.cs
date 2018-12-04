using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.DTO
{
	public class CardInfo
	{
		public byte[] CardId { get; set; }

		public UInt16 Atqa { get; set; }

		public byte Sak { get; set; }

	}
}
