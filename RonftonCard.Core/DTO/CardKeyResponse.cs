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
		/// for M1, is KeyA
		/// </summary>
		public byte[] ReadKey { get; set; }

		/// <summary>
		/// for M1, is KeyB
		/// </summary>
		public byte[] WriteKey { get; set; }

		/// <summary>
		/// for CPU, this is MF root
		/// </summary>
		public byte[] ControlBlock { get; set; }
	}
}