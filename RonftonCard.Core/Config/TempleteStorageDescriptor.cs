using System;
using Bluemoon;

namespace RonftonCard.Core.Config
{
	/// <summary>
	/// Storage description
	/// </summary>
	public class TempleteStorageDescriptor
	{
		/// <summary>
		/// Physical address
		///		for M1 :	sector
		///		for CPU:	file descriptor
		///		for flash : physical address
		/// </summary>
		public UInt16 Address { get; set; }

		/// <summary>
		/// block number
		/// for CPU, this value always is 0
		/// </summary>
		public int Block { get; set; }

		/// <summary>
		/// size of this address space
		/// </summary>
		public int Size { get; set; }

		[Alias("desc")]
		public String Description { get; set; }
		
		private const String TO_STRING_FORMATTER = "Address = 0x{0:X4}, block = {1:D2}, size = {2:D4}, Description = {3}, {4}";
		public override String ToString()
		{
			return String.Format(TO_STRING_FORMATTER,
				this.Address,
				this.Block,
				this.Size,
				this.Description ?? "", 
				Environment.NewLine);
		}
	}
}
