using System;
using System.Collections.Generic;

namespace RonftonCard.Core
{
	using Bluemoon;
	using DTO;
	using Config;
	using CardReader;
	using log4net;

	public class CardManager
	{
		protected static ILog logger = LogManager.GetLogger("RonftonCardLog");

		/// <summary>
		/// initialize M1 card
		/// </summary>
		public static ResultArgs MifareInitialize()
		{
			//ICardReader reader = ConfigManager.GetCardReader();
			//IKeyService keyService = ConfigManager.GetKeyService();
			//UInt16 [] sectors = ConfigManager.GetCardTemplete().SegmentAddr;
			
			return new ResultArgs(false);
		}
	}
}