
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using BlueMoon.Util;
using RonftonCard.Common.Reader;
using RonftonCard.Common.Utils;
using RonftonCard.Common.Entity;

namespace RonftonCard.Common
{
	public class RFTCardContext
	{
		private static IDictionary<String, CardTemplete> cardTempletes;
		private static IDictionary<String, CardReaderDescriptor> cardReaders;

		static RFTCardContext()
		{
			cardReaders = new Dictionary<String, CardReaderDescriptor>(StringComparer.CurrentCultureIgnoreCase);
			cardTempletes = new Dictionary<String, CardTemplete>(StringComparer.CurrentCultureIgnoreCase);
		}

		private static XmlElement LoadConfiguration(String configName)
		{
			String fullFileName;

			if (!FileUtil.Locate(configName, out fullFileName))
				throw new Exception(String.Format("[{0}] config file is not existed!", configName));

			XmlDocument doc = new XmlDocument();

			doc.Load(fullFileName);
			return doc.DocumentElement;
		}

		#region "--- Card reader ---"
		private const String READER_TAG_NAME = "CardReader";
		public static bool LoadCardReader(String configName)
		{
			int seq = 1;

			foreach (XmlNode node in LoadConfiguration(configName))
			{
				if (XmlNodeType.Element == node.NodeType &&
					node.Name.Equals(READER_TAG_NAME, StringComparison.CurrentCultureIgnoreCase))
				{
					cardReaders.Add(node.GetAttributeValue("name", "Unknown#" + seq.ToString()),
						EntityUtil.CreateEntity<CardReaderDescriptor>(node));
					seq++;
				}
			}
			return true;
		}

		public static String[] GetCardReaderNames()
		{
			return cardReaders.Keys.ToArray();
		}

		public static String GetCardReaderDesc(String readerName)
		{
			return cardReaders[readerName].ToString();
		}

		public static ICardReader GetCardReader(String readerName)
		{
			ICardReader reader = null;

			try
			{
				CardReaderDescriptor descriptor = cardReaders[readerName];

				Type type = RonftonCard.Common.Utils.TypeUtil.GetType(descriptor.DrvType);

				if (type != null)
					reader = (ICardReader)Activator.CreateInstance(type, new object[] { descriptor.PortType, 9600 });
			}
			catch (Exception ex)
			{
			}

			return reader;
		}
		#endregion

		#region "--- Card Templete ---"
		/// <summary>
		/// load config from CardTemplete.xml
		/// </summary>
		private const String ROOT_TAG_NAME = "templete";
		public static bool LoadCardTemplete(String configName)
		{
			int seq = 1;

			foreach (XmlNode node in LoadConfiguration(configName))
			{
				if (XmlNodeType.Element == node.NodeType &&
					node.Name.Equals(ROOT_TAG_NAME, StringComparison.CurrentCultureIgnoreCase))
				{
					cardTempletes.Add(node.GetAttributeValue("name", "Unknown#" + seq.ToString()), CardTemplete.CreateCardTemplete(node));
					seq++;
				}
			}
			return true;
		}

		/// <summary>
		/// get all card templete names
		/// </summary>
		public static String[] GetCardTempleteNames()
		{
			return cardTempletes.Keys.ToArray();
		}

		/// <summary>
		/// get card templete by name
		/// </summary>
		/// <param name="name"></param>
		public static CardTemplete GetCardTemplete(String name)
		{
			if (cardTempletes.ContainsKey(name))
			{
				return cardTempletes[name];
			}
			return null;
		}

		#endregion
	}
}
