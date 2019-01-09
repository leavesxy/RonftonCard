using Bluemoon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.Card.DataTypeHandler
{
    public class BcdStringTypeHandler : ICardDataTypeHandler
    {
        public byte[] GetBytes(object obj, int length)
        {
            String str = obj as String;

            if (String.IsNullOrEmpty(str))
                return ByteUtil.Malloc(length);

            return ArrayUtil.CopyFrom(HexString.FromHexString(str), length);
        }

        public Object Parse(Type type, byte[] byteArray)
        {
            return HexString.ToHexString(byteArray);
        }
    }
}