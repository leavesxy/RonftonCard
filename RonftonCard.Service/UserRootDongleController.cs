using System;
using System.Web.Http;
using Bluemoon;
using log4net;
using Newtonsoft.Json;
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
			byte[] rootKeyBytes = HexString.FromHexString(rootKey);

			String jsonString = Convert.ToString(request.keyInfo);

			DongleKeyInfo keyInfo = JsonConvert.DeserializeObject<DongleKeyInfo>(jsonString);

			ResultArgs ret = DongleUtil.dongle.CreateUserRootKey(userId, rootKeyBytes, keyInfo);
			return Json<ResultArgs>(ret);
		}

		[HttpPost]
		[Route("dongle/userRoot/encrypt")]
		public IHttpActionResult Encrypt(dynamic request)
		{
			String plain = Convert.ToString(request.plain);
			byte[] plainBytes = DongleUtil.dongle.Encoder.GetBytes(plain);

			byte[] cipher;

			ResultArgs ret = DongleUtil.dongle.Encrypt(DongleType.USER_ROOT, plainBytes, out cipher ) ? 
					new ResultArgs(true, BitConverter.ToString(cipher), "OK") : 
					new ResultArgs(false, null, DongleUtil.dongle.LastErrorMessage);

			return Json<ResultArgs>(ret);
		}

		[HttpPost]
		[Route("dongle/userRoot/restore")]
		public IHttpActionResult Restore(dynamic request)
		{
			String keyPwd = Convert.ToString(request.keyPwd);
			return Json <ResultArgs>( DongleUtil.Restore(keyPwd));
		}
	}
}