using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Common.Config
{
	public class CardAddrItem
	{
		/// <summary>
		/// offset in virtual address
		/// </summary>
		public int Offset { get; set; }

		/// <summary>
		/// size of this block
		/// </summary>
		public int Length { get; set; }

		/// <summary>
		/// descriptor handler
		/// for m1, descriptor is sector number
		/// for CPU,descriptor is file descriptor
		/// </summary>
		public int Descriptor { get; set; }

		/// <summary>
		/// block number
		/// for CPU, this value always is 0
		/// </summary>
		public int Block { get; set; }

		public override String ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("Offset = 0x").Append(this.Offset.ToString("x4"));
			sb.Append(",Length = ").Append(this.Length);
			sb.Append(",Descriptor = ").Append(this.Descriptor);
			sb.Append(",Block = ").Append(this.Block).Append(Environment.NewLine);
			return sb.ToString();
		}
	}
}
