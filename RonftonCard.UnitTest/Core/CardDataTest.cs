using Microsoft.VisualStudio.TestTools.UnitTesting;
using RonftonCard.Core;
using RonftonCard.Core.Card;
using RonftonCard.Core.Card.Handler;
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
        IDictionary<String, ICardDataHandler> handler;

        [TestInitialize]
        public void Init()
        {
            handler = new Dictionary<String, ICardDataHandler>()
            {
                { "byte", new ByteHandler() },
                { "bcdString", new BcdStringHandler() },
                { "string", new StringHandler() },
                { "number", new NumberHandler() },
                { "bool", new BoolHandler() },
                { "date", new DateHandler() },
            };
        }

        [TestMethod]
        public void Test_1()
        {
            CardInfo info = CardInfo.CreateTestCardInfo();
            Console.WriteLine(info.ToString());

            byte[] buffer = ByteUtil.Malloc(128);

            foreach (PropertyInfo p in info.GetType().GetProperties())
            {
                M1CardAttribute m1Attr = p.GetCustomAttribute<M1CardAttribute>();

                if (m1Attr != null && handler.Keys.Contains(m1Attr.HandlerName))
                {
                    ICardDataHandler __handler = handler[m1Attr.HandlerName];
                    byte[] __buffer = __handler.GetBytes(p.GetValue(info), m1Attr.Length);
                    Array.Copy(__buffer, 0, buffer, m1Attr.Offset, m1Attr.Length);
                }
            }

            Console.WriteLine(" length is : " + buffer.Length);

            for (int i = 0; i < 128; i++)
            {
                if (i != 0 && i % 16 == 0)
                    Console.WriteLine();

                Console.Write(buffer[i].ToString("X2") + " ");
            }

            Console.WriteLine();
            CardInfo info2 = new CardInfo();
            foreach (PropertyInfo p in info.GetType().GetProperties())
            {
                M1CardAttribute m1Attr = p.GetCustomAttribute<M1CardAttribute>();

                if (m1Attr != null && handler.Keys.Contains(m1Attr.HandlerName))
                {
                    ICardDataHandler __handler = handler[m1Attr.HandlerName];
                    byte[] __buffer = new byte[m1Attr.Length];
                    Array.Copy(buffer, m1Attr.Offset, __buffer, 0, m1Attr.Length);

                    p.SetValue(info2, __handler.Parse(p.PropertyType, __buffer));
                }
            }
            Console.WriteLine(info2.ToString());
        }


        public void Test_2()
        {
        }
    }
}