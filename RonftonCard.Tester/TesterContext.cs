using RonftonCard.Common.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Tester
{
	public class TesterContext
	{
		private const String STRU_TEMPLETE_FILE = "CardStru.xml";
		private const String ADDR_TEMPLETE_FILE = "CardAddr.xml";

		public static CardAddrTemplete addrTemplete { get; private set; }
		public static CardStruTemplete struTemplete { get; private set; }

		public static void Init()
		{
			addrTemplete = new CardAddrTemplete(ADDR_TEMPLETE_FILE);
			struTemplete = new CardStruTemplete(STRU_TEMPLETE_FILE);
		}
	}
}
