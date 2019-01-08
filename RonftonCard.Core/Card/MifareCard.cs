using System;

namespace RonftonCard.Core.Card
{
	using System.Collections.Generic;
	using Bluemoon;
	using CardReader;
	using DTO;
	using log4net;

	public class MifareCard : ICard
	{
		private IKeyService keyService;
		private ICardReader reader;
		private ILog logger;
		private byte[] defaultKey;
		private M1KeyMode keyMode;

		public MifareCard(ILog logger, IKeyService keyService, ICardReader reader, M1KeyMode keyMode, byte[] defaultKey)
		{
			this.logger = logger;
			this.reader = reader;
			this.keyService = keyService;
			this.keyMode = keyMode;
			this.defaultKey = defaultKey;
		}

		public bool Personalize()
		{
			return true;
		}

		public bool Restore()
		{
			return true;
		}

		public bool Initialize(UInt16[] sector)
		{
			ResultArgs ret = this.reader.Select();
			if (!ret.Succ)
				return false;

			CardSelectResult card = (CardSelectResult)ret.Result;

			this.logger.Debug(String.Format("card_id={0}, atqa=0x{1}, sak=0x{2}",BitConverter.ToString(card.SN),card.ATQA.ToString("X4"),card.SAK.ToString("X2")));

			List<CardInitRequest> req = PrepareRequest(card.SN, sector);
			List<CardInitResponse> response = keyService.ComputeKey(req);

			return UpdateCardKey(response);
		}

		public bool Initialize(UInt16 sector)
		{
			return Initialize(new UInt16[] { sector });
		}


		private List<CardInitRequest> PrepareRequest(byte[] sn, UInt16[] sector)
		{
			List<CardInitRequest> req = new List<CardInitRequest>();
			for (int i = 0; i < sector.Length; i++)
			{
				//req.Add(new CardInitRequest()
				//{
				//	SN = sn,
				//	Sector = sector[i],
				//	SectorType = (byte)'I'
				//});
			}
			return req;
		}

		private bool UpdateCardKey(List<CardInitResponse> response)
		{
			if (response == null || response.Count == 0)
				return true;

			foreach (CardInitResponse __response in response)
			{
				//this.reader.ChangeControlBlock(__response.Sector, this.keyMode, this.defaultKey, __response.KeyA, __response.KeyB);
			}
			return true;
		}
	}
}