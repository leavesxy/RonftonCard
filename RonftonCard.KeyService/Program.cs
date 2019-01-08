using System;
using Microsoft.Owin.Hosting;
using RonftonCard.Core;

namespace RonftonCard.KeyService
{
	class Program
	{
		private static String BASE_ADDRESS = @"http://localhost:9002/";
		static void Main(string[] args)
		{
			ContextManager.Init("RonftonKeyService");
			
			// Start OWIN host 
			using (WebApp.Start<StartService>(url: BASE_ADDRESS))
			{
				Console.WriteLine("Key service run OK!, " + BASE_ADDRESS);
				HelperInfo.DbgHelperInfo();

				Console.WriteLine("Any key to exit ...");
				Console.ReadLine();
			}
		}
	}
}
