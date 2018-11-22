using Bluemoon;
using Bluemoon.WinForm;
using RonftonCard.Main.Forms;
using RonftonCard.Main.Resources;
using System;
using System.IO;
using System.Resources;
using System.Windows.Forms;
using System.Management;

namespace RonftonCard.Main
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

			String fullName;
			if (FileUtil.Lookup("Logger.xml", out fullName))
			{
				log4net.Config.XmlConfigurator.Configure(new FileInfo(fullName));
			}

			//RegisterUsbWatcher();

			if (!ProcessUtil.IsRunning(rm.GetAppName()))
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainFrm(rm));
			}
			else
				ProcessUtil.SetForeground();
		}

		//public enum EventType
		//{
		//	Inserted = 2,
		//	Removed = 3
		//}

		//static void RegisterUsbWatcher()
		//{
		//	ManagementEventWatcher watcher = new ManagementEventWatcher();
		//	WqlEventQuery query = new WqlEventQuery("SELECT * FROM Win32_VolumeChangeEvent WHERE EventType = 2 or EventType = 3");

		//	watcher.EventArrived += (s, e) =>
		//	{
		//		string driveName = e.NewEvent.Properties["DriveName"].Value.ToString();
		//		EventType eventType = (EventType)(Convert.ToInt16(e.NewEvent.Properties["EventType"].Value));

		//		string eventName = Enum.GetName(typeof(EventType), eventType);

		//		MessageBox.Show(String.Format("{0}: {1} {2}", DateTime.Now, driveName, eventName));
		//	};

		//	watcher.Query = query;
		//	watcher.Start();
		//}
	}
}
