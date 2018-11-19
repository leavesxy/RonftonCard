using Microsoft.Owin.Hosting;
using System;
using System.IO;
using Bluemoon;
using RonftonCard.Core;

namespace RonftonCard.Service
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

			ConfigManager.Init();
			ConfigManager.DongleSelected = "Rockey-Arm";

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
