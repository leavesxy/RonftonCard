using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Common.Utils
{
	public class ArrayUtil
	{
		public static byte[] ToByteArray(String source, char[] split)
		{
			if (String.IsNullOrEmpty(source))
				return null;

			String[] strs = source.Split(split);
			List<byte> dest = new List<byte>();

			foreach( String s in strs )
			{
				dest.Add(Convert.ToByte(s));
			}

			return dest.ToArray();
		}
	}
}