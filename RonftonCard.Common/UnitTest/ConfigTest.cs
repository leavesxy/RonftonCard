using Microsoft.VisualStudio.TestTools.UnitTesting;
using RonftonCard.Common.Config;
using RonftonCard.Common.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RonftonCard.Common.UnitTest
{
	[TestClass]
	public class ConfigTest
	{
		[TestMethod]
		public void LoadCardReader2List()
		{
			List<CardReaderDescriptor> readerDescriptors = XmlUtil.CreateEntity<List<CardReaderDescriptor>>("CardReader.xml");

			Console.Out.WriteLine("LoadCardReader2List");
			foreach(CardReaderDescriptor desc in readerDescriptors)
			{
				Console.Out.WriteLine(desc.ToString());
			}
		}

		[TestMethod]
		public void LoadCardReader2Array()
		{
			XmlElement root = BlueMoon.Config.XmlConfigHelper.GetRootElement("CardReader.xml");

			CardReaderDescriptor[] readerDescriptors = (CardReaderDescriptor[])XmlUtil.CreateEntity(root, typeof(CardReaderDescriptor[]));

			Console.Out.WriteLine("LoadCardReader2Array");
			foreach (CardReaderDescriptor desc in readerDescriptors)
			{
				Console.Out.WriteLine(desc.ToString());
			}
		}

		[TestMethod]
		public void LoadCardTemplete()
		{
			XmlElement root = BlueMoon.Config.XmlConfigHelper.GetRootElement("CardTemplete.xml");
			IDictionary<String, CardConfigTemplete> configTempletes = (IDictionary<String, CardConfigTemplete>)XmlUtil.CreateEntity(root, typeof(Dictionary<String, CardConfigTemplete>));
			
			foreach( String key in configTempletes.Keys)
			{
				Console.Out.WriteLine(configTempletes[key].TempleteName);
				Console.Out.WriteLine(configTempletes[key].DbgDataDescriptor());
				Console.Out.WriteLine(configTempletes[key].DbgStorageDescriptor());
			}
		}
	}
}
