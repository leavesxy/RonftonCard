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
			ICardReader reader = ConfigManager.GetCardReader();
			IKeyService keyService = ConfigManager.GetKeyService();
			UInt16 [] sectors = ConfigManager.GetCardTemplete().SegmentAddr;

			if (!reader.Open())
				return new ResultArgs(false, null, "Can't open card reader !");

			if (!keyService.IsOK())
				return new ResultArgs(false, null, "Can't get key service !");

			if (sectors.IsNullOrEmpty())
				return new ResultArgs(false, null, "Sectors config error !");

			byte[] cardId;
			if (!reader.Select(out cardId))
				return new ResultArgs(false, null, "not card selected !");

			List<CardKeyRequest> request = new List<CardKeyRequest>();
			for(int i=0; i<sectors.Length; i++)
			{
				request.Add(new CardKeyRequest()
				{
					CardId = cardId,
					Sector = sectors[i],
					SectorType = 'I',
					CardType = '5'
				});
			}

			ResultArgs ret = keyService.ComputeKey(request.ToArray());

			if (ret.Succ)
			{
				CardKeyResponse[] response = ret.Result as CardKeyResponse[];

				for (int i = 0; i < response.Length; i++)
				{
					// write card
					logger.Debug(String.Format("CardId = {0}, Sector = {1},KeyA={2}, KeyB={3}, Control={4}",
						BitConverter.ToString(response[i].CardId),
						response[i].Sector,
						BitConverter.ToString(response[i].ReadKey),
						BitConverter.ToString(response[i].WriteKey),
						BitConverter.ToString(response[i].ControlBlock)));
				}
			}

			return ret;
		}
	}
}