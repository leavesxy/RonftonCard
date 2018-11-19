using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Bluemoon;
using log4net;
using RonftonCard.Core;
using RonftonCard.Core.Dongle;
using RonftonCard.Core.Entity;

namespace RonftonCard.Service
{
	public class UserRootDongleController : ApiController
	{
		protected static ILog logger = LogManager.GetLogger("RonftonCardService");

		[HttpPost]
		[Route("dongle/userRoot/create")]
		public IHttpActionResult Create(dynamic args)
		{
			IDongle dongle = ConfigManager.GetDongle();

			String userId = Convert.ToString(args.userId);
			String rootKey = Convert.ToString(args.rootKey);
			int seq = Convert.ToInt32(args.seq);

			byte[] rootKeyBytes = HexString.FromHexString(rootKey);

			ResultArgs ret=null;

			if (dongle.Enumerate())
			{
				ret = dongle.CreateUserRootKey(userId, rootKeyBytes, seq);
			}

			return Json<ResultArgs>(ret);
		}

		[HttpPost]
		[Route("dongle/userRoot/encrypt")]
		public IHttpActionResult Encrypt(dynamic args)
		{
			IDongle dongle = ConfigManager.GetDongle();
			String plain = Convert.ToString(args.plain);
			int seq = Convert.ToInt32(args.seq);
			byte[] plainBytes = dongle.Encoder.GetBytes(plain);

			ResultArgs ret = null;
			byte[] cipher;

			if (dongle.Enumerate())
			{
				ret = dongle.Encrypt(plainBytes, out cipher, seq) ? 
					new ResultArgs(true, BitConverter.ToString(cipher), "OK") : 
					new ResultArgs(false, null, dongle.LastErrorMessage);
			}

			return Json<ResultArgs>(ret);
		}

		[HttpPost]
		[Route("dongle/userRoot/restore")]
		public IHttpActionResult Restore(dynamic args)
		{
			IDongle dongle = ConfigManager.GetDongle();

			String keyPwd = Convert.ToString(args.keyPwd);
			int seq = Convert.ToInt32(args.seq);

			byte[] keyPwdBytes = dongle.Encoder.GetBytes(keyPwd);

			ResultArgs ret = null;

			if (dongle.Enumerate())
			{
				ret = dongle.Restore(keyPwdBytes, seq) ? new ResultArgs(true, null, "OK") : new ResultArgs(false, null, dongle.LastErrorMessage);
			}

			return Json<ResultArgs>(ret);
		}
	}
}
