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
			IDictionary<String, CardTemplete> configTempletes = (IDictionary<String, CardTemplete>)XmlUtil.CreateEntity(root, typeof(Dictionary<String, CardTemplete>));
			
			foreach( String key in configTempletes.Keys)
			{
				Console.Out.WriteLine(configTempletes[key].TempleteName);
				Console.Out.WriteLine(configTempletes[key].DbgTempleteDataDescriptor());
				Console.Out.WriteLine(configTempletes[key].DbgTempleteStorageDescriptor());
			}
		}

		[TestMethod]
		public void GetPhysicalAddress()
		{
			CardContextManager.LoadCardConfigTemplete("CardTemplete.xml");

			foreach(String name in CardContextManager.TempleteNames)
			{
				Console.Out.WriteLine(name);
			}

			CardContextManager.CurrentTempleteName = "RFC";

			int[] addrs = CardContextManager.AddrDescriptors;

			foreach (int i in addrs)
				Console.Out.Write("{0} ", i);
		}
	}
}
