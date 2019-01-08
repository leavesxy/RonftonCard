using Bluemoon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.Card.Handler
{
    public class StringHandler : ICardDataHandler
    {
        public byte[] GetBytes(object obj, int length)
        {
            String str = obj as String;

            if (String.IsNullOrEmpty(str))
                return ByteUtil.Malloc(length);

            return ArrayUtil.CopyFrom(Encoding.UTF8.GetBytes(str), length);
        }

        public Object Parse(Type type, byte[] byteArray)
        {
            return Encoding.UTF8.GetString(byteArray);
        }
    }
}