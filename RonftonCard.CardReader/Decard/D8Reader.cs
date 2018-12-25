using Bluemoon;
using RonftonCard.Core;
using RonftonCard.Core.CardReader;
using System;
using System.Linq;

namespace RonftonCard.CardReader.Decard
{
	using log4net;
	using DEV_HANDLER = System.Int32;

	public partial class D8Reader : ICardReader
	{
		private static ILog logger = LogManager.GetLogger("RonftonCardLog");

		private const short SUCC = 0;
		protected DEV_HANDLER hReader;
		protected int port;
		protected int baud;
		protected CardType cardType;

		///Com: 0~99,USB:100~199,PCSC:200~299,Bluetooth:300~399
		public D8Reader()
			: this((int)ReaderPortType.USB, 0)
		{
		}

		public D8Reader(int port, int baud)
			: this(port,baud, CardType.TYPE_A)
		{
		}

		public D8Reader(int port, int baud, CardType cardType)
		{
			this.port = port;
			this.baud = baud;
			this.hReader = -1;
			this.cardType = cardType;
		}
	}
}
