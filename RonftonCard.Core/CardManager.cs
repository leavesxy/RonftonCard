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
		/// initialize card
		/// </summary>
		public static ResultArgs Initialize(byte[] cardId)
		{
			//ICardReader reader = ConfigManager.GetCardReader();
			IKeyService keyService = ConfigManager.GetKeyService();

			UInt16 [] sectors = ConfigManager.GetCardTemplete().SegmentAddr;

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

			return keyService.ComputeKey(request.ToArray());

			//if( result.Succ )
			//{
			//	CardKeyResponse[] response = result.Result as CardKeyResponse[];

			//	for (int i = 0; i < response.Length; i++)
			//	{
			//		logger.Debug(String.Format("CardId = {0}, Sector = {1},KeyA={2}, KeyB={3}, Control={4}",
			//			BitConverter.ToString(response[i].CardId),
			//			response[i].Sector,
			//			BitConverter.ToString(response[i].KeyA),
			//			BitConverter.ToString(response[i].KeyB),
			//			BitConverter.ToString(response[i].ControlBlock)));
			//	}
			//}

			//return result.Succ;
		}
	}
}