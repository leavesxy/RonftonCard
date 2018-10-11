using BlueMoon.Util;
using RonftonCard.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RonftonCard.Common
{
	public abstract class AbstractCardTemplete
	{
		protected const String ROOT_TAG_NAME = "templete";
		protected const String DATA_TAG_NAME = "data";
		protected const String DATA_ITEM_TAG_NAME = "item";
		protected const String STORAGE_TAG_NAME = "storage";
		protected const String STORAGE_ITEM_TAG_NAME = "addr";

		protected AbstractCardTemplete()
		{
		}

		protected List<RT> CreateTempleteItem<RT>(XmlNode node)
		{
			List<RT> items = new List<RT>();

			foreach (XmlNode n in node.ChildNodes)
			{
				if (XmlNodeType.Element == n.NodeType &&
					n.Name.Equals(this.itemTagName, StringComparison.CurrentCultureIgnoreCase))
				{
					items.Add(EntityUtil.CreateEntity<RT>(n));
				}
			}
			return items;
		}

		protected bool LoadConfiguration(String fileName)
		{
			String fullFileName;

			if (!FileUtil.Locate( fileName, out fullFileName))
				throw new Exception(String.Format("{0} templete is not existed!", fileName));

			XmlDocument doc = new XmlDocument();
			doc.Load(fullFileName);
			foreach (XmlNode node in doc.DocumentElement)
			{
				if (XmlNodeType.Element == node.NodeType &&
					node.Name.Equals(ROOT_TAG_NAME, StringComparison.CurrentCultureIgnoreCase))
				{
					AddConfig<RT>(node.Attributes["name"].Value, CreateTempleteItem<RT>(node));
				}
			}

			return true;
		}
	}
}
