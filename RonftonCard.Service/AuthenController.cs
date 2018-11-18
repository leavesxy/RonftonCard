using log4net;
using Newtonsoft.Json;
using System;
using System.Web.Http;
using RonftonCard.Service.Test;

namespace RonftonCard.Service
{
	public class AuthenController : ApiController
	{
		protected static ILog logger = LogManager.GetLogger("RonftonCardService");

		[HttpGet]
		[Route("api/get_test")]
		public IHttpActionResult GetTest()
		{
			return Ok("Hello! this is a get test ");
		}

		[HttpPost]
		[Route("api/post_test")]
		public IHttpActionResult PostTest()
		{
			return Ok("Hello! this is a post test ");
		}


		// enumerate all keys
		[HttpGet]
		[Route("api/enum")]
		public IHttpActionResult Enumerate()
		{
			String result = EnumerateKey();
			logger.Debug(result);

			return Ok(result);
		}

		[HttpPost]
		[Route("api/send")]
		public IHttpActionResult Send(TestInfo testInfo)
		{
			logger.Debug(JsonConvert.SerializeObject(testInfo));
			TestInfo t = new TestInfo()
			{
				Id = "1111",
				Desc = "Description",
				Price = 99
			};

			return Json< TestInfo>(t);
		}
		
		private String EnumerateKey()
		{
			DongleKey key = new DongleKey();
			AuthenKeyInfo[] keyInfo = key.Enumerate();

			if (keyInfo.IsNullOrEmpty())
				return "no key found !";

			return JsonConvert.SerializeObject(keyInfo);
		}
	}
}
