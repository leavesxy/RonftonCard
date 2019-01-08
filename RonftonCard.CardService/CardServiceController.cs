using System;
using System.Web.Http;
using Bluemoon;
using RonftonCard.Core.CardReader;
using log4net;
using RonftonCard.Core.DTO;
using RonftonCard.Core;

namespace RonftonCard.CardService
{
	public class CardServiceController : ApiController
	{
		protected static ILog logger = ContextManager.GetLogger();

        //{"http://localhost:9001/reader/open   ",  "[GET] : 打开读卡器"},
        //{"http://localhost:9001/reader/info   ",  "[GET] : 获取读卡器信息" },
        //{"http://localhost:9001/reader/select ",  "[GET] : 寻卡" },
        //{"http://localhost:9001/reader/read/id",  "[POST]: 读卡信息,id代表扇区号" },

        [HttpGet]
        [Route("reader/test")]
        public IHttpActionResult Test()
        {
			return Json<ResultArgs>(new ResultArgs(true)
			{
				Msg = "OK",
				Result = new CardSelectResult()
				{
					SN = new byte[] { 0x01, 0x02, 0x03, 0x04 },
					ATQA = 0x40,
					SAK = 0x28
				}
			});
		}

        [HttpGet]
		[Route("reader/open")]
		public IHttpActionResult Open()
		{
			ICardReader cardReader = ContextManager.GetCardReader();
			bool ret = false;
			if (cardReader != null)
				ret = cardReader.Open();

			return Json<ResultArgs>(
				new ResultArgs(ret)
				{
					Result = "open card reader",
					Msg = ret ? "OK" : "failed"
				});
		}

		[HttpGet]
		[Route("reader/info")]
		public IHttpActionResult Info()
		{
			ICardReader cardReader = ContextManager.GetCardReader();

			return Json < ResultArgs >(new ResultArgs(true)
			{
				Result = cardReader.GetVersion(),
				Msg = "OK"
			});
		}

		[HttpGet]
		[Route("reader/select")]
		public IHttpActionResult Select()
		{
			ICardReader cardReader = ContextManager.GetCardReader();

            IHttpActionResult result = Json<ResultArgs>(cardReader.Select());
            logger.Debug( result.ToString() );
            return result;
		}

		[HttpPost]
		[Route("reader/read")]
		public IHttpActionResult Read(dynamic request)
		{
			int sector = Convert.ToInt32(request.sector);
			M1KeyMode keyMode = Enum.Parse(typeof(M1KeyMode), Convert.ToString(request.mode), true);
			byte[] key = HexString.FromHexString(Convert.ToString(request.key), "-");

			logger.Debug(String.Format("read sector {0}, mode = {1}, key={2} ", sector, keyMode, BitConverter.ToString(key)));

			return Json < ResultArgs > (CardUtil.ReadSector(sector, keyMode, key));
		}

		[HttpGet]
		[Route("reader/initialize")]
		public IHttpActionResult Initialize()
		{
			return null;
		}

		[HttpGet]
		[Route("reader/write")]
		public IHttpActionResult Write()
		{
			return null;
		}
	}
}