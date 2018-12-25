using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluemoon;
using Microsoft.Owin.Hosting;
using Spring.Context;
using Spring.Context.Support;

namespace RonftonCard.CardService
{
	class Program
	{
		private static String BASE_ADDRESS = @"http://localhost:9001/";
		static void Main(string[] args)
		{
			ContextManager.Init();

			// Start OWIN host 
			using (WebApp.Start<StartService>(url: BASE_ADDRESS))
			{
				Console.WriteLine("Reader service run OK!, " + BASE_ADDRESS);
				HelperInfo.DbgHelperInfo();

				Console.WriteLine("Any key to exit ...");
				Console.ReadLine();
			}
		}
	}
}