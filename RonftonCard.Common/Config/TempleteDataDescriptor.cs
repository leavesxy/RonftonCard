using System;
using Bluemoon;

namespace RonftonCard.Common.Config
{
	/// <summary>
	/// data description which written to card
	/// </summary>
	public class TempleteDataDescriptor
	{
		/// <summary>
		/// name of this data item
		/// </summary>
		public String Name { get; set; }

		/// <summary>
		/// type of data item
		/// </summary>
		[Alias("type")]
		public CardDataType DataType { get; set; }

		/// <summary>
		/// size for saving this data item
		/// </summary>
		public int Length { get; set; }

		/// <summary>
		/// virtual offset of this data item
		/// </summary>
		public int Offset { get; set; }

		/// <summary>
		/// description of this data item
		/// </summary>
		public String Description { get; set; }

		private const String TO_STRING_FORMATTER = "name={0},type={1,-10},length={2:D2},offset=0x{3:X4},desc={4}{5}";

		public override String ToString()
		{
			return String.Format(TO_STRING_FORMATTER,
				this.Name.ToLower().RightPadding(20),
				this.DataType,
				this.Length.ToString("d2"),
				this.Offset.ToString("X4"),
				this.Description,
				Environment.NewLine);
		}
	}
}