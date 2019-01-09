using System;

namespace RonftonCard.Core.Card.DataTypeHandler
{
	using Bluemoon;

    public class BoolTypeHandler : ICardDataTypeHandler
    {
        public byte[] GetBytes(object obj, int length)
        {
            if (!obj.GetType().Equals(typeof(bool)))
                return new byte[] { 0x00 };

            return new byte[] { (bool)obj ? (byte)0x01 : (byte)0x00 };
        }

        public Object Parse(Type type, byte[] byteArray)
        {
			if (byteArray.IsNullOrEmpty())
				return (Boolean)false;

            return byteArray[0] == (byte)0x00 ? false : true;
        }
    }
}