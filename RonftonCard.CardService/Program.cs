using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluemoon;
using Microsoft.Owin.Hosting;
using RonftonCard.Core;
using Spring.Context;
using Spring.Context.Support;

namespace RonftonCard.CardService
{
	class Program
	{
		private static String DEFAULT_HOST_ADDRESS = @"http://localhost:9001/";
		static void Main(String[] args)
		{
            String host;

            if (args.IsNullOrEmpty())
                host = DEFAULT_HOST_ADDRESS;
            else
            {
                host = args[0];
                if (!host.EndsWith(@"/"))
                    host += @"/";
            }

			ContextManager.InitAll("RonftonCardService");
			ContextManager.SetCardReaderSelected("Decard-d8");
			
			// Start OWIN host 
			using (WebApp.Start<StartService>(url: host))
			{
				Console.WriteLine("Reader service run OK!, " + host);
				HelperInfo.DbgHelperInfo();

				Console.WriteLine("Any key to exit ...");
				Console.ReadLine();
			}
		}
	}
}