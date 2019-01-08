using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluemoon;
using log4net;
using RonftonCard.Core;
using RonftonCard.Core.CardReader;
using RonftonCard.Core.DTO;

namespace RonftonCard.CardService
{
	public class CardUtil
	{
		protected static ILog logger = ContextManager.GetLogger();
		public static ResultArgs ReadSector(int sector, M1KeyMode keyMode, byte[] key)
		{
			ICardReader cardReader = ContextManager.GetCardReader();

			ResultArgs ret = cardReader.Select();
			if (!ret.Succ)
				return new ResultArgs(false, null, "Select Card Error!");

			CardSelectResult info = (CardSelectResult)ret.Result;
			logger.Debug(String.Format("Select card_id={0}, ATQA=0x{1}, SAK={2}",
					BitConverter.ToString(info.SN),
					info.ATQA.ToString("X4"),
					info.SAK.ToString("X2")));

			if (!cardReader.Authen(keyMode, sector * 4, key))
			{
				logger.Debug(String.Format("Auth sector {0} failed !", sector));
				return new ResultArgs(false, null, "Auth sector failed !");
			}

			byte[] buffer;
			int len = 0;
			if (!cardReader.ReadSector(sector, out buffer, out len))
			{
				return new ResultArgs(false, null, "Read sector failed !");
			}

			return new ResultArgs(true, BitConverter.ToString(buffer), "OK");
		}
	}
}
