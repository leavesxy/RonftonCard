using BlueMoon;
using BlueMoon.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RonftonCard.Common.Util
{
	public class ConfigureUtil
	{
		public static IDictionary<String, RT> LoadConfiguration<RT>(String configName,  String tagName )
		{
			int seq = 1;
			IDictionary<String, RT> config = new Dictionary<String, RT>(StringComparer.CurrentCultureIgnoreCase);

			foreach (XmlNode node in XmlConfigHelper.GetRootElement(configName) )
			{
				if (XmlNodeType.Element == node.NodeType &&	node.Name.Equals(tagName, StringComparison.CurrentCultureIgnoreCase))
				{
					config.Add( ((XmlElement)node).GetAttrValue("name", "Unknown#" + seq.ToString()),
						EntityUtil.CreateEntity<RT>(node));
					seq++;
				}
			}
			return config;
		}


		public static List<RT> CreateItem<RT>(XmlNode node, String tagName)
		{
			List<RT> items = new List<RT>();

			// node maybe null !!!
			if (node != null)
			{
				foreach (XmlNode n in node.ChildNodes)
				{
					if (XmlNodeType.Element == n.NodeType &&
						n.Name.Equals(tagName, StringComparison.CurrentCultureIgnoreCase))
					{
						items.Add(EntityUtil.CreateEntity<RT>(n));
					}
				}
			}
			return items;
		}
	}
}
