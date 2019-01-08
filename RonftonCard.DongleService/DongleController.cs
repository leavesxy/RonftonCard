using log4net;
using System.Web.Http;
using Bluemoon;
using System;

namespace RonftonCard.DongleService
{
	public class DongleController : ApiController
	{
		protected static ILog logger = LogManager.GetLogger("RonftonDongleService");

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
			bool ret = DongleUtil.dongle.Enumerate();

			return Json<ResultArgs>(
				new ResultArgs(ret)
				{
					Result = DongleUtil.dongle.Dongles,
					Msg = DongleUtil.dongle.LastErrorMessage
                });
		}

		[HttpPost]
		[Route("dongle/open")]
		public IHttpActionResult Open(dynamic request)
		{
			int seq = Convert.ToInt32(request.seq);

			logger.Debug(String.Format("Open {0} dongle.", seq));

			bool ret = DongleUtil.dongle.Open(seq);

			return Json<ResultArgs>(
				new ResultArgs(ret)
				{
					Result = null,
					Msg = DongleUtil.dongle.LastErrorMessage
				});
		}

		[HttpPost]
		[Route("dongle/close")]
		public IHttpActionResult Close()
		{
			DongleUtil.dongle.Close();

			return Json<ResultArgs>(
				new ResultArgs( DongleUtil.dongle.IsSucc )
				{
					Result = null,
					Msg = DongleUtil.dongle.LastErrorMessage
				});
		}
	}
}
