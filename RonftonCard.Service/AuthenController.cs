using log4net;
using Newtonsoft.Json;
using RonftonCard.AuthenKey.RockeyArm;
using RonftonCard.Common.AuthenKey;
using System;
using System.Web.Http;
using BlueMoon;
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


		// POST api/authen [FromBody]  <------> public IHttpActionResult Post([FormBody]String name )
		// add httpHeader : Content-Type: application/x-www-form-urlencoded
		// POST api/authen/Values
		//      post data only one String, like =hubin  
		//                                 ^ no key !!!
		// POST api/authen?name=hubin <-----> Post(String name)
		// POST api/authen  <-------> Post()

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
