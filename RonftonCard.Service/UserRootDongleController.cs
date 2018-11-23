using System;
using System.Web.Http;
using Bluemoon;
using log4net;
using RonftonCard.Core;
using RonftonCard.Core.Dongle;

namespace RonftonCard.Service
{
	public class UserRootDongleController : ApiController
	{
		protected static ILog logger = LogManager.GetLogger("RonftonCardService");

		[HttpPost]
		[Route("dongle/userRoot/create")]
		public IHttpActionResult Create(dynamic request)
		{
			String userId = Convert.ToString(request.userId);
			String rootKey = Convert.ToString(request.rootKey);
			int seq = Convert.ToInt32(request.seq);

			byte[] rootKeyBytes = HexString.FromHexString(rootKey);
			ResultArgs ret=null;

			ret = DongleUtil.dongle.CreateUserRootKey(userId, rootKeyBytes);
			ret.Msg = DongleUtil.dongle.LastErrorMessage;

			return Json<ResultArgs>(ret);
		}

		[HttpPost]
		[Route("dongle/userRoot/encrypt")]
		public IHttpActionResult Encrypt(dynamic request)
		{
			String plain = Convert.ToString(request.plain);
			int seq = Convert.ToInt32(request.seq);
			byte[] plainBytes = DongleUtil.dongle.Encoder.GetBytes(plain);

			ResultArgs ret = null;
			byte[] cipher;

			ret = DongleUtil.dongle.Encrypt(DongleType.USER_ROOT, plainBytes, out cipher ) ? 
					new ResultArgs(true, BitConverter.ToString(cipher), "OK") : 
					new ResultArgs(false, null, DongleUtil.dongle.LastErrorMessage);

			return Json<ResultArgs>(ret);
		}

		[HttpPost]
		[Route("dongle/userRoot/restore")]
		public IHttpActionResult Restore(dynamic request)
		{
			String keyPwd = Convert.ToString(request.keyPwd);
			int seq = Convert.ToInt32(request.seq);

			return Json <ResultArgs>( DongleUtil.Restore(keyPwd));
		}
	}
}