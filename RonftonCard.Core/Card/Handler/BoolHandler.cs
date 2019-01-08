using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.Card.Handler
{
    public class BoolHandler : ICardDataHandler
    {
        public byte[] GetBytes(object obj, int length)
        {
            if (!obj.GetType().Equals(typeof(bool)))
                return new byte[] { 0x00 };

            return new byte[] { (bool)obj ? (byte)0x01 : (byte)0x00 };
        }

        public Object Parse(Type type, byte[] byteArray)
        {
            return byteArray[0] == (byte)0x00 ? false : true;
        }
    }
}
