using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.Card
{
    public interface ICardDataTypeHandler
    {
        byte[] GetBytes(Object obj, int length);

        Object Parse(Type type, byte[] byteArray);
    }
}