using BlueMoon.Form;
using log4net.Config;
using RonftonCard.Tester.Forms;
using RonftonCard.Tester.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RonftonCard.Tester
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			ResourceManager rm = new ResourceManager(typeof(AppResources).ToString(), typeof(AppResources).Assembly);
			log4net.Config.XmlConfigurator.Configure(new FileInfo(@"config\log4net.xml"));

			if (!ProcessUtil.IsRunning(rm.GetAppName()))
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new TestMainFrm(rm));
			}
			else
				ProcessUtil.SetForeground();
		}
	}
}
