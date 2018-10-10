using BlueMoon.Attribute;
using System;
using System.Text;

namespace RonftonCard.Common.Config
{
	public class CardStruItem
	{
		public String Name { get; set; }

		[MapTo("type")]
		public CardDataType DataType { get; set; }

		public int Length { get; set; }

		public int Offset { get; set; }

		public String Description { get; set; }

		public override String ToString()
		{
			return String.Format("name={0,-15},type={1,-10},length={2},offset=0x{3},desc={4}{5}",
				this.Name,
				this.DataType,
				this.Length.ToString("d2"),
				this.Offset.ToString("X4"),
				this.Description,
				Environment.NewLine);
		}
	}
}
