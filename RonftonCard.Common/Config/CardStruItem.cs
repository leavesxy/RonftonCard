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
			StringBuilder sb = new StringBuilder();
			sb.Append("Name=").Append(this.Name);
			sb.Append(",Type=").Append(this.DataType);
			sb.Append(",Length=").Append(this.Length);
			sb.Append(",Offset=").Append(this.Offset);
			sb.Append(",Desc=").Append(this.Description).Append(Environment.NewLine);
			return sb.ToString();
		}
	}
}
