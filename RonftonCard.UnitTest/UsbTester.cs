using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RonftonCard.UnitTest
{
	[TestClass]
	public class UsbTester
	{
		[TestMethod]
		public void Enumerate()
		{
			ManagementObjectCollection collection;
			using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_USBHub"))
			{
				collection = searcher.Get();
			}

			foreach (var device in collection)
			{
				Console.WriteLine("id={0}, pnp={1}, desc={2}",
					(string)device.GetPropertyValue("DeviceID"),
					(string)device.GetPropertyValue("PNPDeviceID"),
					(string)device.GetPropertyValue("Description"));
			}

			collection.Dispose();
		}
	}
}
