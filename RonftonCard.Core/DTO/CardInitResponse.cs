using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.DTO
{
	public class CardInitResponse
	{
		public UInt16 Sector { get; set; }

		public byte[] KeyA { get; set; }

		public byte[] KeyB { get; set; }

	}
}