using RonftonCard.Common.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Common.Card
{
	/// <summary>
	/// virtual card
	/// 
	/// </summary>
	public class VirtualCard
	{
		private CardAddrItem[] cardAddrTable;
		private byte[] virtualAddress;
		public int Size { get; private set; }

		public VirtualCard(List<CardAddrItem> addrTable)
		{
			// sorted by addr offset
			cardAddrTable = addrTable.OrderBy(addr => addr.Offset).ToArray();

			if (IsValidAddress())
			{
				this.Size = ComputeSize();
				this.virtualAddress = new byte[Size];
			}
		}

		private int ComputeSize()
		{
			return cardAddrTable[cardAddrTable.Length - 1].Offset + cardAddrTable[cardAddrTable.Length - 1].Length;
		}

		private const int MAX_CARD_SIZE_M1 = 1024;
		private const int MAX_CARD_CPU = 8 * 1024;

		public bool IsValidAddress()
		{
			for (int i = 1; i < cardAddrTable.Length; i++)
			{
				if (cardAddrTable[i - 1].Offset + cardAddrTable[i - 1].Length != cardAddrTable[i].Offset)
					return false;
			}
			return true;
		}

		private void Clear()
		{
			Array.Clear(virtualAddress, 0, Size);
		}

		public bool WriteCardItem( CardStruItem item )
		{
			return true;
		}

		public bool WriteCardStru(List<CardStruItem> cardStru)
		{
			Clear();
			return true;
		}

		private bool IsFree( int addr )
		{
			if (virtualAddress[addr] == 0x00)
				return true;
			return false;
		}

		private void SetUsed (int addr)
		{
			virtualAddress[addr] = 0x55;
		}

		public bool CheckCardStru(List<CardStruItem> cardStru, out String errMsg)
		{
			bool ret = true;

			Clear();
			StringBuilder sb = new StringBuilder();

			foreach( CardStruItem item in cardStru)
			{
				int idx = item.Offset;

				for (int i = 0; i < item.Length; i++)
				{
					if (IsFree(idx + i))
						SetUsed(idx + i);
					else
					{
						ret = false;
						sb.Append(String.Format("Address conflicted at [0x{0}], name={1}{2}",
							(idx + i).ToString("X4"),
							item.Name,
							Environment.NewLine));
						break;
					}
				}
			}

			errMsg = sb.ToString();
			return ret;
		}
	}
}