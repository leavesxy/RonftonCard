using BlueMoon.Config;
using RonftonCard.Common.Config;
using RonftonCard.Common.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using BlueMoon;
using RonftonCard.Common.Reader;

namespace RonftonCard.Common
{
	/// <summary>
	/// manage Card context
	/// </summary>
	public class ContextManager
	{
		private static IDictionary<String, CardConfigTemplete> configTempletes;
		private static IDictionary<String, CardReaderDescriptor> readerDescriptors;

		static ContextManager()
		{
			//readerDescriptors = new Dictionary<String, CardReaderDescriptor>(StringComparer.CurrentCultureIgnoreCase);
			//configTempletes = new Dictionary<String, CardConfigTemplete>(StringComparer.CurrentCultureIgnoreCase);
		}

		/// <summary>
		/// Create card context for next operation
		/// </summary>
		public static CardContext CreateContext(CardType cardType, String configTempleteName, String readerName)
		{
			return new CardContext()
			{
				CardType = cardType,
				ReaderDescriptor = readerDescriptors[readerName],
				ConfigTemplete = configTempletes[configTempleteName]
			};
		}

		public static void LoadCardReaderConfig(String configFile, String nodeTagName )
		{
			readerDescriptors = ConfigureUtil.LoadConfiguration<CardReaderDescriptor>(configFile, nodeTagName);
		}

		public static void LoadCardConfigTemplete(String configFile, String nodeTagName)
		{
			configTempletes = new Dictionary<String, CardConfigTemplete>(StringComparer.CurrentCultureIgnoreCase);

			int seq = 1;
			foreach (XmlNode node in XmlConfigHelper.GetRootElement(configFile))
			{
				if (XmlNodeType.Element == node.NodeType &&
					node.Name.Equals(nodeTagName, StringComparison.CurrentCultureIgnoreCase))
				{
					configTempletes.Add( ((XmlElement)node).GetAttrValue("name", "Unknown#" + seq.ToString()), 
						CardConfigTemplete.Create(node));
					seq++;
				}
			}
		}

		public static String[] GetTempleteNames()
		{
			return configTempletes.Keys.ToArray();
		}

		public static String[] GetCardReaderNames()
		{
			return readerDescriptors.Keys.ToArray();
		}

		private static ICardReader GetCardReader(String readerName)
		{
			ICardReader reader = null;
			try
			{
				CardReaderDescriptor desc = readerDescriptors[readerName];
				Type type = RonftonCard.Common.Util.TypeUtil.GetType(desc.DrvType);

				if (type != null)
					reader = (ICardReader)Activator.CreateInstance(type, new object[] { desc.PortType, desc.Baud });
			}
			catch (Exception ex)
			{
			}

			return reader;
		}
	}
}
