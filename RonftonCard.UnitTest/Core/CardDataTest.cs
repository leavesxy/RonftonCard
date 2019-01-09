using Microsoft.VisualStudio.TestTools.UnitTesting;
using RonftonCard.Core;
using RonftonCard.Core.Card;
using RonftonCard.Core.Card.DataTypeHandler;
using RonftonCard.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.UnitTest.Core
{
    using Bluemoon;

    [TestClass]
    public class CardDataTest
    {
        [TestMethod]
        public void Test_1()
        {
            CardInfo info = CardInfo.CreateTestCardInfo();
			Console.WriteLine(info.ToString());

			VirtualCard vc = new VirtualCard(128);
			vc.Write<CardInfo>(info);
			DbgUtil.Dbg(vc.DumpBuffer(), 16);
        }

		[TestMethod]
        public void Test_2()
        {
			String hexString = "10 01 23 45 32 30 31 38 31 32 30 36 30 30 30 31 " +
								"01 00 00 00 20 12 31 00 00 00 00 00 00 00 00 00 " +
								"00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 " +
								"00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 " +
								"00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 " +
								"00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 " +
								"00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 " +
								"00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00";

			byte[] buffer = HexString.FromHexString(hexString, " ");

			VirtualCard vc = new VirtualCard(buffer);
			CardInfo cardInfo = vc.Parse<CardInfo>();
			Console.WriteLine(cardInfo.ToString());
		}
    }
}