using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Bluemoon;
using RonftonCard.Core.CardReader;
using Newtonsoft.Json;
using log4net;

namespace RonftonCard.CardService
{
	public class CardServiceController : ApiController
	{
		protected static ILog logger = ContextManager.GetLogger();

        //{"http://localhost:9001/reader/open   ",  "[GET]: 打开读卡器"},
        //{"http://localhost:9001/reader/info   ",  "[GET]: 获取读卡器信息" },
        //{"http://localhost:9001/reader/select ",  "[GET]: 寻卡" },
        //{"http://localhost:9001/reader/read/id",  "[GET]: 读卡信息,id代表扇区号" },

        class TestDto
        {
            public byte[] testBytes { get; set; }
            public String byteString { get; set; }
            public String base64String { get; set; }
        }

        [HttpGet]
        [Route("reader/test")]
        public IHttpActionResult Test()
        {
            byte[] byteBuffer = new byte[] { 0x01, 0x02, 0x03, 0x4 };

            TestDto dto = new TestDto()
            {
                testBytes = byteBuffer,
                byteString = Encoding.UTF8.GetString(byteBuffer),
                base64String = Convert.ToBase64String(byteBuffer)
            };

            logger.Debug("test value = " + JsonConvert.SerializeObject(dto));

            byte[] byteBuffer2 = Convert.FromBase64String(dto.base64String);
            logger.Debug("original byte array = " + BitConverter.ToString(byteBuffer2));

            return Json<TestDto>(dto);
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

		[HttpGet]
		[Route("reader/read")]
		public IHttpActionResult Read()
		{
			return null;
		}
	}
}
