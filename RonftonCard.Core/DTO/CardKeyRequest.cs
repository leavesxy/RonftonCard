using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.DTO
{
	public class CardKeyRequest
	{

		public byte[] CardId { get; set; }

		/// <summary>
		/// M1  : sector number
		/// CPU : file descriptor
		/// </summary>
		public UInt16 Sector { get; set; }

		/// <summary>
		/// 'I' for id
		/// 'W' for wallet
		/// </summary>
		public char SectorType { get; set; }

		/// <summary>
		/// '5' for S50
		/// '7' for S70
		/// 'C' for cpu
		/// </summary>
		public char CardType { get; set; }

	}
}
