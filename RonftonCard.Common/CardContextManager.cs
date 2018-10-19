
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using RonftonCard.Common.Reader;
using RonftonCard.Common.Utils;
using RonftonCard.Common.Entity;

namespace RonftonCard.Common
{
	public class CardContextManager
	{
		private static IDictionary<String, CardTemplete> cardTempletes;
		private static IDictionary<String, CardReaderDescriptor> cardReaders;

		static CardContextManager()
		{
			cardReaders = new Dictionary<String, CardReaderDescriptor>(StringComparer.CurrentCultureIgnoreCase);
			cardTempletes = new Dictionary<String, CardTemplete>(StringComparer.CurrentCultureIgnoreCase);
		}

		public static CardContext CreateCardContext(CardType cardType, String templeteName, String readerName)
		{
			return new CardContext()
			{
				//CardReader = GetCardReader(readerName),
				CardTemplete = GetCardTemplete(templeteName),
				CardType = cardType
			};
		}

		#region "--- Card reader ---"
		public static bool LoadCardReader(String configName)
		{
			return ConfigureUtil.LoadConfiguration<CardReaderDescriptor>(configName, cardReaders, "CardReader");
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
		//public static bool LoadCardTemplete(String configName)
		//{
		//	return ConfigureUtil.LoadConfiguration<CardTemplete>(configName, cardTempletes, "templete");
		//}

		private const String ROOT_TAG_NAME = "templete";
		public static bool LoadCardTemplete(String configName)
		{
			int seq = 1;

			foreach (XmlNode node in ConfigureUtil.GetRootNode(configName))
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
