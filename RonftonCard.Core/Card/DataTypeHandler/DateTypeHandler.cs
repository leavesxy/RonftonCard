using Bluemoon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.Card.DataTypeHandler
{
    public class DateTypeHandler : ICardDataTypeHandler
    {
        public byte[] GetBytes(object obj, int length)
        {
            DateTime dt = (DateTime)obj;

            String str;

            if (length == 3)
                str = dt.ToString("yyMMdd");
            else if (length == 4)
                str = dt.ToString("yyyyMMdd");
            else if (length == 2)
                str = dt.ToString("MMdd");
            else
                str = null;

            return HexString.FromHexString(str);
        }

        public Object Parse(Type type, byte[] byteArray)
        {
            String val = HexString.ToHexString(byteArray);
			if (byteArray.Length == 3)
				val = "20" + val;
			else if (byteArray.Length == 2)
			{
				// use current year
				val = DateTime.Now.ToString("yyyy") + val;
			}
            return DateTime.ParseExact(val, "yyyyMMdd", null);
        }
    }
}