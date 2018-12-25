using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Bluemoon;
using Common.Logging;
using RonftonCard.Core.CardReader;

namespace RonftonCard.CardService
{
	public class CardServiceController : ApiController
	{
		protected static ILog logger = ContextManager.GetLogger();

		//{"http://localhost:9001/reader/open   ",  "[GET]: 打开读卡器"},
		//{"http://localhost:9001/reader/info   ",  "[GET]: 获取读卡器信息" },
		//{"http://localhost:9001/reader/select ",  "[GET]: 寻卡" },
		//{"http://localhost:9001/reader/read/id",  "[GET]: 读卡信息,id代表扇区号" },

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

			return Json<ResultArgs>(cardReader.Select());
		}

		[HttpGet]
		[Route("reader/read")]
		public IHttpActionResult Read()
		{
			return null;
		}
	}
}
