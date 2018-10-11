using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Common.Utils
{
	public static class StringUtil
	{
		public static String RightPadding(this String @this, int totalLen)
		{
			if (String.IsNullOrEmpty(@this))
				return new string(' ', totalLen);

			int len = totalLen - Encoding.GetEncoding("gb2312").GetBytes(@this).Length;
			//int len = totalLen - Encoding.GetEncoding("ascii").GetBytes(@this).Length;
			if (len < 0)
				return @this;

			return @this + new string(' ', len);
		}
	}
}
