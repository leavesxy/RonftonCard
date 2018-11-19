using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using log4net;

namespace RonftonCard.Service
{
	public class AuthenDongleController : ApiController
	{
		protected static ILog logger = LogManager.GetLogger("RonftonCardService");

		[HttpPost]
		[Route("dongle/authen/create")]
		public IHttpActionResult Create()
		{
			return Ok<String>("CreateUserRootKey");
		}

		[HttpPost]
		[Route("dongle/authen/encrypt")]
		public IHttpActionResult Encrypt()
		{
			return Ok<String>("EncryptByUserRootKey");
		}

		[HttpPost]
		[Route("dongle/authen/restore")]
		public IHttpActionResult Restore()
		{
			return Ok<String>("EncryptByUserRootKey");
		}
	}
}
