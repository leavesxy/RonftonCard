using System;
using System.Web.Http;
using Bluemoon;
using RonftonCard.Core.CardReader;
using log4net;
using RonftonCard.Core.DTO;
using RonftonCard.Core;
using System.Collections.Generic;
using System.Text;

namespace RonftonCard.KeyService
{
	public class KeyServiceController : ApiController
	{
		protected static ILog logger = ContextManager.GetLogger();
		
        [HttpGet]
        [Route("test")]
        public IHttpActionResult Test()
        {
			return Json<ResultArgs>(new ResultArgs(true)
			{
				Msg = "OK",
				Result = "this is a service test !"
			});
		}

		/// <summary>
		/// JSon{ userid: '000001', sn : '01-02-03-04', sector: 4, sectorType : }
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
        [HttpPost]
		[Route("m1")]
		public IHttpActionResult Init(dynamic request)
		{
			String userId = Convert.ToString(request.userid);
			byte[] sn = HexString.FromHexString(Convert.ToString(request.sn), "-");
			ushort sector = Convert.ToUInt16( request.sector) ;

			return Json<ResultArgs>(
				new ResultArgs(true, M1KeyServiceUtil.ComputeKey(
					new CardInitRequest()
					{
						UserId = userId,
						SN = sn,
						Sector = sector
					}), "ok"));
		}
	}
}