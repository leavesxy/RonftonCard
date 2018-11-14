using Bluemoon.Converter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace RonftonCard.Common
{
	public class MifareVirtualCard : AbstractVirtualCard
	{
		private byte[] controlBlock;

		public MifareVirtualCard(CardContext cardContext)
			: base(cardContext)
		{

		}

		#region "--- compute control block ---"
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
				byte[] bb = StringConverterManager.ConvertTo<byte[]>(s);
				byteArray.Add(bb);
			}

			return byteArray.ToArray();
		}
		#endregion

		#region "--- debug---"
		public override String DbgBuffer()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("virtual Card information :" + Environment.NewLine);
			sb.Append("-------------------------------------------" + Environment.NewLine);
			for (int i = 0; i < base.virtualBuffer.Length; i++)
			{
				if (i % 16 == 0)
				{
					if (i != 0)
						sb.Append(Environment.NewLine);

					sb.Append(String.Format("{0}:", i.ToString("d4"))).Append(" ");
				}
				else
					sb.Append(" ");

				sb.Append(this.virtualBuffer[i].ToString("x2"));
			}
			sb.Append(Environment.NewLine);
			return sb.ToString();
		}

		public override String DbgArgs()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("M1 Control Block :").Append(Environment.NewLine);
			sb.Append(BitConverter.ToString(this.controlBlock));
			return sb.ToString();
		}
		#endregion
	}
}
