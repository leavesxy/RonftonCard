using System;
using Microsoft.Owin.Hosting;
using RonftonCard.Core;
using Bluemoon;

namespace RonftonCard.KeyService
{
	class Program
	{
		private static String DEFAULT_HOST_ADDRESS = @"http://localhost:9002/";
		static void Main(string[] args)
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

            ContextManager.Init("RonftonKeyService");
			
			// Start OWIN host 
			using (WebApp.Start<StartService>(url: host))
			{
				Console.WriteLine("Key service run OK!, " + host);
				HelperInfo.DbgHelperInfo();

				Console.WriteLine("Any key to exit ...");
				Console.ReadLine();
			}
		}
	}
}
