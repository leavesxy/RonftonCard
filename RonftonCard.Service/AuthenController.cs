using log4net;
using Newtonsoft.Json;
using RonftonCard.AuthenKey.RockeyArm;
using RonftonCard.Common.AuthenKey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using BlueMoon;
using System.Web;

namespace RonftonCard.Service
{
	public class AuthenController : ApiController
	{
		protected static ILog logger = LogManager.GetLogger("RonftonCardService");

		// GET api/authen 
		[HttpGet]
		public String Get()
		{
			String result = EnumerateKey();
			logger.Debug(result);
			return result;
		}

		// POST api/authen 
		[HttpPost]
		public IHttpActionResult Post()
		{
			int num = HttpContext.Current.Request.Form.Count;

			logger.Debug("receive post" + num.ToString());

			return Json<String>("I received request " + num.ToString());
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
