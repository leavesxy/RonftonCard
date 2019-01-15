using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.Card
{
	public class VirtualAddressUnit
	{
		/// <summary>
		/// descriptor for card
		/// for M1 represent logic sector no
		///		S50: from 0~15
		///		S70: from 0~39
		///	for CPU represent file descriptor
		/// </summary>
		public UInt32 Descriptor { get; set; }

		/// <summary>
		/// block No in descriptor
		/// for M1 represent logic block No
		///		S50: from 0~63
		///		S70: from 0~255
		///	for CPU equal descriptor
		/// </summary>
		public UInt32 BlockNo { get; set; }

		/// <summary>
		/// internal block no ,use for sorting
		/// </summary>
		public UInt32 internalBlockNo { get; set; }

		/// <summary>
		/// offset address in virtual card buffer
		/// </summary>
		public UInt32 Offset { get; set; }

		/// <summary>
		/// M1:  size of this Block
		/// CPU: size of this file
		/// </summary>
		public UInt32 Size { get; set; }

		public bool IsValid()
		{
            return Size <= 0;
		}
	}
}
