using System;
using System.Web.Http;
using Bluemoon;
using log4net;
using RonftonCard.Core;
using RonftonCard.Core.Dongle;

namespace RonftonCard.Service
{
	public class AuthenDongleController : ApiController
	{
		protected static ILog logger = LogManager.GetLogger("RonftonCardService");

		[HttpPost]
		[Route("dongle/authen/create")]
		public IHttpActionResult Create(dynamic request)
		{
			String userId = Convert.ToString(request.userId);
			int seq = Convert.ToInt32(request.seq);
			ResultArgs ret = null;

			ret = DongleUtil.dongle.CreateAuthenKey(userId);
			ret.Msg = DongleUtil.dongle.LastErrorMessage;

			return Json<ResultArgs>(ret);
		}

		[HttpPost]
		[Route("dongle/authen/encrypt")]
		public IHttpActionResult Encrypt(dynamic request)
		{
			String plain = Convert.ToString(request.plain);
			int seq = Convert.ToInt32(request.seq);
			byte[] plainBytes = DongleUtil.dongle.Encoder.GetBytes(plain);

			ResultArgs ret = null;
			byte[] cipher;

			ret = DongleUtil.dongle.Encrypt(DongleType.AUTHEN, plainBytes, out cipher) ?
					new ResultArgs(true, BitConverter.ToString(cipher), "OK") :
					new ResultArgs(false, null, DongleUtil.dongle.LastErrorMessage);

			return Json<ResultArgs>(ret);
		}

		[HttpPost]
		[Route("dongle/authen/restore")]
		public IHttpActionResult Restore(dynamic request)
		{
			String keyPwd = Convert.ToString(request.keyPwd);
			int seq = Convert.ToInt32(request.seq);

			return Json<ResultArgs>(DongleUtil.Restore(keyPwd));
		}
	}
}
