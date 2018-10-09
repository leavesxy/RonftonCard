using BlueMoon.Util;
using RonftonCard.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RonftonCard.Common.Config
{
	public abstract class AbstractConfigTemplete
	{
		protected String fileName;
		protected String nodeTagName;
		protected String itemTagName;

		protected AbstractConfigTemplete(String fileName, String nodeTagName, String itemTagName)
		{
			this.fileName = fileName;
			this.nodeTagName = nodeTagName;
			this.itemTagName = itemTagName;
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

		protected bool LoadConfig<RT>(String fileName)
		{
			String fullFileName;

			if (FileUtil.Locate(fileName, out fullFileName))
			{
				XmlDocument doc = new XmlDocument();
				doc.Load(fullFileName);

				foreach (XmlNode node in doc.DocumentElement)
				{
					if (XmlNodeType.Element == node.NodeType &&
						node.Name.Equals(nodeTagName, StringComparison.CurrentCultureIgnoreCase))
					{
						//this.cardStru.Add(node.Attributes["name"].Value, base.CreateTempleteItem<CardDataItem>(node));
						AddConfig<RT>(node.Attributes["name"].Value, CreateTempleteItem<RT>(node));
					}
				}
				return true;
			}
			return false;
		}

		protected abstract void AddConfig<RT>(String name, List<RT> items);
	}
}
