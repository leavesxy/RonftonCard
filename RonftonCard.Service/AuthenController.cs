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
using System.Collections.Specialized;
using RonftonCard.Service.Test;

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

        public IHttpActionResult Post([FromBody] TestInfo testInfo)
        {
            String ret = testInfo == null ? "null" : testInfo.ToString();
            logger.Debug("Received : " + ret);

            return Json<String>("I received request " + ret);
        }

        // POST api/authen [FromBody]  <------> public IHttpActionResult Post([FormBody]String name )
        // add httpHeader : Content-Type: application/x-www-form-urlencoded
        // POST api/authen/Values
        //      post data only one String, like =hubin  
        //                                 ^ no key !!!
        // POST api/authen?name=hubin <-----> Post(String name)
        // POST api/authen  <-------> Post()

        private String getAllRequestData(IDictionary<String, Object> nv)
        {
            if (nv == null)
                return "";

            StringBuilder sb = new StringBuilder();

            foreach (String key in nv.Keys)
            {
                sb.Append(key).Append("=").Append(nv[key]);
            }
            return sb.ToString();
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
