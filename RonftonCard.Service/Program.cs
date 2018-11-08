using Microsoft.Owin.Hosting;
using System;
using System.IO;

namespace RonftonCard.Service
{
	class Program
	{
		static void Main(string[] args)
		{
			log4net.Config.XmlConfigurator.Configure(new FileInfo(@"config\log4net.xml"));

			string baseAddress = @"http://localhost:9000/";

			// Start OWIN host 
			using (WebApp.Start<StartService>(url: baseAddress))
			{
				Console.WriteLine("Any key to exit ...");
				Console.ReadLine();
			}
		}
	}
}
