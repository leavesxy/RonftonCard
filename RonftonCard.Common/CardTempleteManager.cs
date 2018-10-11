using BlueMoon.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RonftonCard.Common
{
	public class CardTempleteManager
	{
		private const String ROOT_TAG_NAME = "templete";

		private static IDictionary<String, CardTemplete> cardTempletes;

		public static bool LoadCardTemplete(String fileName)
		{
			String fullFileName;

			if (!FileUtil.Locate(fileName, out fullFileName))
				throw new Exception(String.Format("{0} templete is not existed!", fileName));

			cardTempletes = new Dictionary<String, CardTemplete>( StringComparer.CurrentCultureIgnoreCase);

			XmlDocument doc = new XmlDocument();
			doc.Load(fullFileName);
			foreach (XmlNode node in doc.DocumentElement)
			{
				if (XmlNodeType.Element == node.NodeType &&
					node.Name.Equals(ROOT_TAG_NAME, StringComparison.CurrentCultureIgnoreCase))
				{
					cardTempletes.Add(node.Attributes["name"].Value, CardTemplete.CreateCardTemplete(node));
				}
			}
			return true;
		}

		public static List<String> GetTempleteNames()
		{
			return cardTempletes.Keys.ToList();
		}

		/// <summary>
		/// get particular card templete
		/// </summary>
		/// <param name="name"></param>
		public static CardTemplete GetCardTemplete(String name)
		{
			if( cardTempletes.ContainsKey(name))
			{
				return cardTempletes[name];
			}
			return null;
		}
	}
}