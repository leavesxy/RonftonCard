using System;
using System.Web.Http;
using Bluemoon;
using RonftonCard.Core.CardReader;
using log4net;
using RonftonCard.Core.DTO;
using RonftonCard.Core;
using Newtonsoft.Json;

namespace RonftonCard.CardService
{
	public class CardServiceController : ApiController
	{
		protected static ILog logger = ContextManager.GetLogger();

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

		/// <summary>
		/// read sector
		/// </summary>
		[HttpPost]
		[Route("reader/readSector")]
		public IHttpActionResult ReadSector(dynamic request)
		{
			int sector = Convert.ToInt32(request.sector);
			M1KeyMode keyMode = Enum.Parse(typeof(M1KeyMode), Convert.ToString(request.mode), true);
			byte[] key = HexString.FromHexString(Convert.ToString(request.key), "-");

			logger.Debug(String.Format("read sector {0}, mode = {1}, key={2} ", sector, keyMode, BitConverter.ToString(key)));

			return Json < ResultArgs > (CardUtil.ReadSector(sector, keyMode, key));
		}

		[HttpPost]
		[Route("reader/readBlock")]
		public IHttpActionResult ReadBlock(dynamic request)
		{
			int block = Convert.ToInt32(request.block);
			M1KeyMode keyMode = Enum.Parse(typeof(M1KeyMode), Convert.ToString(request.mode), true);
			byte[] key = HexString.FromHexString(Convert.ToString(request.key), "-");

			return Json<ResultArgs>(new ResultArgs(true, null, "ok"));
			//logger.Debug(String.Format("read sector {0}, mode = {1}, key={2} ", sector, keyMode, BitConverter.ToString(key)));

			//return Json<ResultArgs>(CardUtil.ReadSector(sector, keyMode, key));
		}


		[HttpGet]
		[Route("reader/init")]
		public IHttpActionResult Init()
		{
			return null;
		}

		[HttpPost]
		[Route("reader/personalize")]
		public IHttpActionResult Personalize(dynamic request)
		{
			int sector = Convert.ToInt32(request.sector);
			M1KeyMode keyMode = Enum.Parse(typeof(M1KeyMode), Convert.ToString(request.mode), true);
			byte[] key = HexString.FromHexString(Convert.ToString(request.key), "-");

			CardInfo cardInfo = JsonConvert.DeserializeObject<CardInfo>(Convert.ToString(request.data));
			logger.Debug(String.Format("personalize : {0}, keyMode={1}, Key={2}",
							sector, keyMode, BitConverter.ToString(key)));
			logger.Debug(cardInfo.ToString()); 

			return Json<ResultArgs>( new ResultArgs( true, cardInfo, "OK") );
		}
	}
}