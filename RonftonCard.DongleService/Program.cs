using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluemoon;
using Microsoft.Owin.Hosting;
using RonftonCard.Core;

namespace RonftonCard.DongleService
{
	class Program
	{
		static void Main(string[] args)
		{
			String fullName;
			if (FileUtil.Lookup("Logger.xml", out fullName))
			{
				log4net.Config.XmlConfigurator.Configure(new FileInfo(fullName));
			}

			ContextManager.InitAll("RonftonDongleService");
			ContextManager.SetDongleSelected("Rockey-Arm");

			string baseAddress = @"http://localhost:9000/";

			// Start OWIN host 
			using (WebApp.Start<StartService>(url: baseAddress))
			{
				Console.WriteLine("RonftonCard service run OK!, " + baseAddress);
				HelperInfo.DbgHelperInfo();

				Console.WriteLine("Any key to exit ...");
				Console.ReadLine();
			}
		}
	}
}
