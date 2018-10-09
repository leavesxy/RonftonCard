using BlueMoon.Form;
using RonftonCard.Tester.Forms;
using RonftonCard.Tester.Resources;
using System;
using System.Collections.Generic;
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
