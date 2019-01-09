using Microsoft.VisualStudio.TestTools.UnitTesting;
using RonftonCard.Core.Card;
using RonftonCard.Core.Card.DataTypeHandler;
using System;

namespace RonftonCard.UnitTest.Core
{

    [TestClass]
    public class ByteHandlerTest
    {
        [TestMethod]
        public void Test_1()
        {
            ICardDataTypeHandler bh = new ByteTypeHandler();

            byte[] buffer = bh.GetBytes( (byte)0x05, 1);

            System.Console.WriteLine(BitConverter.ToString(buffer));
        }

        [TestMethod]
        public void Test_2()
        {
            ICardDataTypeHandler bh = new ByteTypeHandler();

            byte[] buffer = bh.GetBytes( new byte[] { 0x01,0x02,0x03 }, 3);

            Console.WriteLine(BitConverter.ToString(buffer));

            buffer = bh.GetBytes(new byte[] { 0x01, 0x02, 0x03 }, 4);
            Console.WriteLine(BitConverter.ToString(buffer));

            buffer = bh.GetBytes(new byte[] { 0x01, 0x02, 0x03 }, 2);
            Console.WriteLine(BitConverter.ToString(buffer));
        }
    }
}
