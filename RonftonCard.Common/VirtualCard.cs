using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Common
{
	public class VirtualCard
	{
		private CardContext cardContext;
		private byte[] virtualAddress;

		public VirtualCard( CardContext ctx )
		{
			this.cardContext = ctx;
			InitVirtualCard();
		}

		private bool InitVirtualCard()
		{
			this.virtualAddress = new byte[this.cardContext.CardTemplete.CardSize];

			return true;
		}

		public void WriteCard(int offset, byte[]data, int length)
		{
			Array.Copy(this.virtualAddress, offset, data, 0, length);
		}


		#region "--- debug ---"
		public String Dbg()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("Virtual Card debug information :" + Environment.NewLine);
			sb.Append("-------------------------------------------" + Environment.NewLine);
			for(int i=0; i<this.virtualAddress.Length; i++)
			{
				if ( i % 16 == 0)
				{
					if( i != 0 )
						sb.Append(Environment.NewLine);

					sb.Append(String.Format("{0}:", i.ToString("x4"))).Append(" ");
				}
				else
					sb.Append(" ");

				sb.Append(this.virtualAddress[i].ToString("x2"));
			}
			sb.Append(Environment.NewLine);
			return sb.ToString();
		}
		#endregion
	}
}