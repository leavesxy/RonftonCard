﻿using log4net;
using System.Web.Http;
using RonftonCard.Core.Dongle;
using RonftonCard.Core;
using Bluemoon;
using System;

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
			DongleUtil.dongle.Enumerate();

			return Json<ResultArgs>(
				new ResultArgs(true)
				{
					Result = DongleUtil.dongle.Dongles,
					Msg = "OK"
				});
		}

		[HttpPost]
		[Route("dongle/open")]
		public IHttpActionResult Open(dynamic request)
		{
			int seq = Convert.ToInt32(request.seq);
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
