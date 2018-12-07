using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.DTO
{
	/// <summary>
	/// information of selecting card
	/// </summary>
	public class CardSelectResult
	{
		/// <summary>
		/// serial number of card, 4 bytes,big endian
		/// </summary>
		public byte[] SN { get; set; }

		/// <summary>
		/// Answer To Request for Type A (B)
		/// there are two bytes for ATQA
		///		first byte has no defined(RFU)
		///		two-bit high b7b6 of the second byte indicates the serial number length of the card
		///			00 -- 4 bytes, 01 -- 7 bytes, 10 -- 10bytes
		///	example: block 0 of M1 card 
		///		80A8D92B DA 28 04 00 9010150100000000
		///		card_id     ^  ^^ ^^ 
		///		           SAK(cpu simulate M1)
		///		                ATQA b7b6='00', card_id 4 bytes
		/// M1_S50:			0x0004
		/// M1_S70:			0x0002
		/// M1_UltraLight:	0x0044
		/// M1_Light:		0x0010
		/// M1_Desfire:		0x0344
		/// </summary>
		public UInt16 ATQA { get; set; }

		/// <summary>
		/// select acknowledge
		/// M1: sak = 0x08
		/// CPU simulate M1: sak=0x28
		/// </summary>
		public byte SAK { get; set; }

	}
}
