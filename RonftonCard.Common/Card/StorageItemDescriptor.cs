using System;
using BlueMoon.Attribute;

namespace RonftonCard.Common.Card
{
	/// <summary>
	/// Storage description
	/// </summary>
	public class StorageItemDescriptor
	{
		/// <summary>
		/// virtual base address
		/// </summary>
		[MapTo("base")]
		public int VirtualBaseAddr { get; set; }

		/// <summary>
		/// size of this address space
		/// </summary>
		public int Size { get; set; }

		/// <summary>
		/// physical descriptor handler, physical address, physical file etc.
		/// for m1, descriptor is sector number
		/// for CPU,descriptor is file descriptor
		/// </summary>
		[MapTo("descriptor")]
		public int PhysicalDescriptor { get; set; }

		/// <summary>
		/// block number
		/// for CPU, this value always is 0
		/// </summary>
		public int Block { get; set; }

		private const String TO_STRING_FORMATTER = "baseAddr = 0x{0:X4}, size = {1:D4}, physicalDescriptor = 0x{2:X4}, block = {3:D2}{4}";
		public override String ToString()
		{
			return String.Format(TO_STRING_FORMATTER,this.VirtualBaseAddr,this.Size,this.PhysicalDescriptor,this.Block, Environment.NewLine);
		}
	}
}
