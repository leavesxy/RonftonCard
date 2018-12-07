using System;

namespace RonftonCard.Core.Card
{
	using System.Collections.Generic;
	using Bluemoon;
	using CardReader;
	using Config;
	using DTO;
	using log4net;

	public class MifareCard : ICard
	{
		private IKeyService keyService;
		private ICardReader reader;
		private CardTempleteDescriptor cardTemplete;
		private ILog logger;

		public MifareCard(ILog logger, CardTempleteDescriptor cardTemplete, IKeyService keyService, ICardReader reader)
		{
			this.logger = logger;
			this.cardTemplete = cardTemplete;
			this.reader = reader;
			this.keyService = keyService;
		}

		public bool Personalize()
		{
			return true;
		}

		public bool Restore()
		{
			return true;
		}

		public bool Initialize()
		{
			ResultArgs ret = this.reader.Select();
			if (!ret.Succ)
				return false;

			CardSelectResult card = (CardSelectResult)ret.Result;

			this.logger.Debug(String.Format("card_id={0}, atqa=0x{1}, sak=0x{2}",
					BitConverter.ToString(card.SN),
					card.ATQA.ToString("X4"),
					card.SAK.ToString("X2")));

			ushort[] sectors = this.cardTemplete.SegmentAddr;

			//ResultArgs keyResponse = keyService.ComputeKey();
			ret = this.keyService.ComputeKey(card.SN);
			if (ret.Succ)
			{
				WriteCardKey((IDictionary<ushort, byte[]>)ret.Result, sectors);
			}
			return true;
		}

		public bool WriteCardKey(IDictionary<ushort, byte[]> keys, ushort[] sectors)
		{
			for (int i = 0; i < sectors.Length; i++)
			{
				//if( IsWalletSector( sectors[s] )
			}

			return true;
		}
	}
}