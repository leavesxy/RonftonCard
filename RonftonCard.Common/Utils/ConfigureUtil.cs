using BlueMoon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RonftonCard.Common.Utils
{
	public class ConfigureUtil
	{
		/// <summary>
		/// load root document
		/// </summary>
		public static XmlElement GetRootNode(String configName)
		{
			String fullFileName;

			if (!FileUtil.Lookup(configName, out fullFileName))
				throw new Exception(String.Format("[{0}] config file is not existed!", configName));

			XmlDocument doc = new XmlDocument();

			doc.Load(fullFileName);
			return doc.DocumentElement;
		}

		public static bool LoadConfiguration<ConfigEntity>(String configName, IDictionary<String, ConfigEntity> config, String tagName )
		{
			int seq = 1;

			foreach (XmlNode node in GetRootNode(configName))
			{
				if (XmlNodeType.Element == node.NodeType &&
					node.Name.Equals(tagName, StringComparison.CurrentCultureIgnoreCase))
				{
					config.Add(node.GetAttributeValue("name", "Unknown#" + seq.ToString()),
						EntityUtil.CreateEntity<ConfigEntity>(node));
					seq++;
				}
			}
			return true;
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
