using log4net;
using System.Web.Http;
using RonftonCard.Core.Dongle;
using RonftonCard.Core;
using Bluemoon;

namespace RonftonCard.Service
{
	public class DongleController : ApiController
	{
		protected static ILog logger = LogManager.GetLogger("RonftonCardService");

		[HttpGet]
		[Route("dongle/test")]
		public IHttpActionResult Test()
		{
			return Json<ResultArgs>(
				new ResultArgs(true)
				{
					Result = "this is a test",
					Msg = "OK"
				});
		}

		[HttpGet]
		[Route("dongle")]
		public IHttpActionResult Enumerate()
		{
			IDongle dongle = ConfigManager.GetDongle();
			DongleInfo[] dongleInfo = null;

			if (dongle != null)
			{
				dongle.Enumerate();
				dongleInfo = dongle.Dongles;
			}

			return Json<ResultArgs>(
				new ResultArgs(true)
				{
					Result = dongleInfo,
					Msg = "OK"
				});
		}


		//[HttpPost]
		//[Route("api/send")]
		//public IHttpActionResult Send(TestInfo testInfo)
		//{
		//	logger.Debug(JsonConvert.SerializeObject(testInfo));
		//	TestInfo t = new TestInfo()
		//	{
		//		Id = "1111",
		//		Desc = "Description",
		//		Price = 99
		//	};

		//	return Json< TestInfo>(t);
		//}

		//private String EnumerateKey()
		//{
		//	DongleKey key = new DongleKey();
		//	AuthenKeyInfo[] keyInfo = key.Enumerate();

		//	if (keyInfo.IsNullOrEmpty())
		//		return "no key found !";

		//	return JsonConvert.SerializeObject(keyInfo);
		//}
	}
}
