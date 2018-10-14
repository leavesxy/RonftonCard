using System;
using BlueMoon.Attribute;

namespace RonftonCard.Common.Entity
{
	/// <summary>
	/// Storage description
	/// </summary>
	public class StorageItemDescriptor
	{
		/// <summary>
		/// Physical address
		/// </summary>
		public int PhysicalAddr { get; set; }

		/// <summary>
		/// block number
		/// for CPU, this value always is 0
		/// </summary>
		public int Block { get; set; }

		/// <summary>
		/// size of this address space
		/// </summary>
		public int Size { get; set; }

		[MapTo("desc")]
		public String Description { get; set; }
		
		private const String TO_STRING_FORMATTER = "PhysicalAddr = 0x{0:X4}, block = {1:D2}, size = {2:D4}, Description = {3}, {4}";
		public override String ToString()
		{
			return String.Format(TO_STRING_FORMATTER,
				this.PhysicalAddr,
				this.Block,
				this.Size,
				this.Description ?? "", 
				Environment.NewLine);
		}
	}
}
