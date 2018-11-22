using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Bluemoon;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RonftonCard.Core;
using RonftonCard.Core.Dongle;
using RonftonCard.Dongle.RockeyArm;
using System.Xml;

namespace RonftonCard.UnitTest.Dongle
{
	[TestClass]
	public class RockeyArmDongleTester
	{


		[TestInitialize]
		public void init()
		{
			//ConfigManager.Init();
			String fullName;
			if (FileUtil.Lookup("Logger.xml", out fullName))
			{
				log4net.Config.XmlConfigurator.Configure(new FileInfo(fullName));
			}
		}

		[TestMethod]
		public void ReadTime()

		}
	}
}