using Microsoft.VisualStudio.TestTools.UnitTesting;
using RonftonCard.Core.Card;
using RonftonCard.Core.Card.DataTypeHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.UnitTest.Core.CardDataHandler
{
    [TestClass]
    public class NumberHandlerTest
    {
        [TestMethod]
        public void Test_1()
        {
            ICardDataTypeHandler dh = new NumberTypeHandler();

            byte[] buffer;

            buffer = dh.GetBytes((short)20, 2);
            Console.WriteLine(BitConverter.ToString(buffer));

            buffer = dh.GetBytes((int)20, 4);
            Console.WriteLine(BitConverter.ToString(buffer));

            buffer = dh.GetBytes((long)20, 8);
            Console.WriteLine(BitConverter.ToString(buffer));

        }
    }
}
