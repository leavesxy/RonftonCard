using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.DTO
{
	public class CardKeyResponse
	{
		public byte[] CardId { get; set; }

		public UInt16 Sector { get; set; }

		/// <summary>
		/// for CPU, this is key for reading
		/// </summary>
		public byte[] KeyA { get; set; }

		/// <summary>
		/// for CPU, this is key for writing
		/// </summary>
		public byte[] KeyB { get; set; }

		/// <summary>
		/// for CPU, this is MF root
		/// </summary>
		public byte[] ControlBlock { get; set; }
	}
}