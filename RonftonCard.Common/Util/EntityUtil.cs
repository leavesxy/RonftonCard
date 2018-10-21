using BlueMoon;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace RonftonCard.Common.Util
{
	public class EntityUtil
	{
		public static RT CreateEntity<RT>(XmlNode node)
		{
			RT entity = (RT)Activator.CreateInstance(typeof(RT));
			PropertyInfo[] props = entity.GetType().GetProperties();

			IDictionary<String, String> dict = GetXmlNodeAttributes(node);

			foreach (PropertyInfo pi in props)
			{
				String n = pi.GetAliasName() ?? pi.Name;
				if (dict.Keys.Contains(n) )
				{
					pi.SetValue(entity, StringConverterManager.ConvertTo(pi.PropertyType, dict[n]));
				}
			}
			return entity;
		}

		/// <summary>
		/// get all attributes of xmlNode
		/// </summary>
		public static IDictionary<String, String> GetXmlNodeAttributes(XmlNode node)
		{
			IDictionary<String, String> dict = new Dictionary<String, String>(StringComparer.CurrentCultureIgnoreCase);

			if (node == null)
				return dict;

			// add all attributes
			if (node.Attributes.Count > 0)
			{
				foreach (XmlAttribute attr in node.Attributes)
				{
					dict.Add(attr.Name, attr.Value);
				}
			}

			foreach (XmlNode subNode in node.ChildNodes)
			{
				if (!String.IsNullOrEmpty(subNode.InnerText))
					dict.Add(subNode.Name, subNode.InnerText);
			}
			return dict;
		}
	}
}
