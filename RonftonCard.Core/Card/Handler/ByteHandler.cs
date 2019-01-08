using Bluemoon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.Card.Handler
{
    public class ByteHandler : ICardDataHandler
    {
        public ByteHandler()
        {
        }

        public byte[] GetBytes(object obj, int length)
        {
            if (obj.GetType().IsArray)
                return GetBytes((byte[])obj, length);

            if (obj.GetType().IsList())
                return GetBytes(ArrayUtil.CopyFromList((IList)obj, typeof(byte)), length);

            return new byte[] { (byte)(obj) };
        }

        private byte[] GetBytes(byte[] obj, int length)
        {
            return ArrayUtil.CopyFrom(obj, length);
        }

        public Object Parse(Type type, byte[] byteArray)
        {
            if (byteArray.Length == 1)
                return (byte)byteArray[0];

            if (type.IsList())
                return byteArray.ToList();

            return byteArray;
        }
    }
}