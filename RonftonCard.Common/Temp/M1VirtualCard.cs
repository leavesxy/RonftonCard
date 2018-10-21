using RonftonCard.Common.Config;
using RonftonCard.Common.Reader;
using RonftonCard.Common.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RonftonCard.Common
{
	public class M1VirtualCard
	{
		private byte[] controlBlock;
		public CardConfigTemplete CardTemplete { get; private set; }
		public ICardReader CardReader { get; private set; }

		private byte[] virtualAddress;
		private int[] physicalSectors;

		public M1VirtualCard(String controlBlockString, CardConfigTemplete cardTemplete, ICardReader cardReader)
		{
			this.controlBlock = ComputeControlBlock(controlBlockString);
			this.CardTemplete = cardTemplete;
			this.CardReader = cardReader;
			Init();
		}

		private void Init()
		{
			this.virtualAddress = InitVirtualAddress();
			this.physicalSectors = this.CardTemplete.SegmentAddr;
		}

		public void InitCard()
		{
		}

		private const byte FLASH_DEFAULT_VALUE = (byte)0xFF;

		public byte[] InitVirtualAddress()
		{
			int size = this.CardTemplete.CardSize;
			return Enumerable.Repeat(FLASH_DEFAULT_VALUE, size).ToArray(); ;
		}

		// there are four bytes for control block, (offset 6-9)
		// and the 9th byte is reserved, 0x69 is default value
		private const byte DEFAULT_CONTROL_BLOCK9 = (byte)0x69;
		private byte[] ComputeControlBlock(String controlBlockString)
		{
			byte[][] controlBlockBit = ParseControlBlockString(controlBlockString);
			/*
				block0 C10 C20 C30 ;User Data block，except 0 sector 0 block
				block1 C11 C21 C31 ;User Data block
				block2 C12 C22 C32 ;User Data block
				block3 C13 C23 C33 ;key control block

				byte[6] = C23_b C22_b C21_b C20_b C13_b C12_b C11_b C10_b
				byte[7] = C13   C12   C11   C10   C33_b C32_b C31_b C30_b
				byte[8] = C33   C32   C31   C30   C23   C22   C21   C20
			 */

			byte[] cb = new byte[4];

			// { c1X c2x c3x } {c13 c23 c33}
			byte c1x = controlBlockBit[0][0];
			byte c2x = controlBlockBit[0][1];
			byte c3x = controlBlockBit[0][2];

			byte c13 = controlBlockBit[1][0];
			byte c23 = controlBlockBit[1][1];
			byte c33 = controlBlockBit[1][2];

			cb[0] = (byte)((~(c23 << 3 | c2x << 2 | c2x << 1 | c2x) << 4) | (~(c13 << 3 | c1x << 2 | c1x << 1 | c1x) & 0x0f));
			cb[1] = (byte)(((c13 << 3 | c1x << 2 | c1x << 1 | c1x) << 4) | (~(c23 << 3 | c3x << 2 | c3x << 1 | c3x) & 0x0f));
			cb[2] = (byte)(((c33 << 3 | c3x << 2 | c3x << 1 | c3x) << 4) | ((c23 << 3 | c2x << 2 | c2x << 1 | c2x) & 0x0f));

			cb[3] = DEFAULT_CONTROL_BLOCK9;
			return cb;
		}

		private String REGEX_EXPR = @"\{[^\}]*?\}";
		private byte[][] ParseControlBlockString(String controlBlockString)
		{
			Regex regex = new Regex(REGEX_EXPR);

			List<byte[]> byteArray = new List<byte[]>();

			foreach (Match match in regex.Matches(controlBlockString))
			{
				int p1 = match.Value.IndexOf('{');
				int len = match.Value.IndexOf('}') - p1 - 1;
				String s = match.Value.Substring(p1 + 1, len);
				byte[] bb = ArrayUtil.ToByteArray(s, new Char[] { ' ', ',' });
				byteArray.Add(bb);
			}

			return byteArray.ToArray();
		}

		#region "--- debug ---"

		public String DbgControlBlock()
		{
			return BitConverter.ToString(this.controlBlock);
		}

		public String DbgSectors()
		{
			StringBuilder sb = new StringBuilder();
			foreach(int ps in this.physicalSectors)
			{
				sb.Append(String.Format("{0},", ps));
			}
			return sb.ToString();
		}

		#endregion
	}
}
