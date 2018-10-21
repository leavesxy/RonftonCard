using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using BlueMoon;
using System.Reflection;
using System.Collections;

namespace RonftonCard.Common.Util
{
	public class XmlUtil
	{
		public static RT CreateEntity<RT>(String configFileName, String sectionName=null)
		{
			XmlElement root = BlueMoon.Config.XmlConfigHelper.GetRootElement(configFileName, sectionName);
			return (RT)CreateEntity(root, typeof(RT));
		}

		public static Object CreateEntity(XmlElement element, Type type)
		{
			if( type.IsArray || type.IsList() )
			{
				return CreateArrayEntity(element, type);
			}
			else if( type.IsDictionary() )
			{
				return CreateDictionaryEntity(element, type);
			}
			// POO
			return CreatePOO(element, type);
		}

		public static Object CreateDictionaryEntity(XmlElement element, Type type)
		{
			Type[] elementType = type.GetGenericArguments();

			IDictionary dict = (IDictionary)Activator.CreateInstance(typeof(Dictionary<,>).MakeGenericType(elementType));
			int seq = 1;

			foreach (XmlNode child in element.ChildNodes.OfType<XmlElement>())
			{
				dict.Add(child.GetAttrValue("name", "Unknown#" + seq.ToString() ),CreateEntity((XmlElement)child, elementType[1]));
			}
			return dict;
		}

		public static Object CreateArrayEntity(XmlElement element, Type type)
		{
			Type elementType = type.IsArray ? type.GetElementType() : type.GetGenericArguments()[0];

			IList list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(elementType));
			foreach (XmlNode child in element.ChildNodes.OfType<XmlElement>())
			{
				list.Add(CreateEntity((XmlElement)child, elementType));
			}

			if (type.IsArray)
			{
				Array array = Array.CreateInstance(elementType, list.Count);
				list.CopyTo(array, 0);
				return array;
			}
			return list;
		}

		/// <summary>
		/// Create plain ordinary object
		/// </summary>
		public static Object CreatePOO(XmlElement element, Type type)
		{
			// create instance for type
			Object instance = Activator.CreateInstance(type);

			PropertyInfo[] props = type.GetProperties();

			// cache all attribute of this element
			XmlAttribute[] elementAttrs = element.Attributes.OfType<XmlAttribute>().ToArray();

			// cache all child node of this element
			XmlNode[] childNodes = element.ChildNodes.OfType<XmlElement>().ToArray();

			//iterator each property of instance built above
			foreach ( PropertyInfo p in props)
			{
				// must can be written!!!
				if (!p.CanWrite)
					continue;

				String propName = p.GetAliasName() ?? p.Name;

				if (SetValueByXmlAttribute(instance, p, elementAttrs, propName))
					continue;

				SetValueByChildNode(instance, p, childNodes, propName);

			}
			return instance;
		}

		private static bool SetValueByXmlAttribute(Object instance, PropertyInfo prop, XmlAttribute[] attrs, String propName)
		{
			if (attrs.IsNullOrEmpty())
				return false;

			XmlAttribute attr = attrs.FirstOrDefault(a => a.Name.Equals(propName, StringComparison.CurrentCultureIgnoreCase));

			if (attr == null)
				return false;

			//like: <elementTag name="name" ... >
			//                    ^    ^
			prop.SetValue(instance, StringConverterManager.ConvertTo(prop.PropertyType, attr.Value), null);
			return true;
		}

		private static bool SetValueByChildNode(Object instance, PropertyInfo prop, XmlNode[] childNodes, String propName)
		{
			if (childNodes.IsNullOrEmpty())
				return false;

			XmlNode node = childNodes.FirstOrDefault(n => 
					n.NodeType == XmlNodeType.Element && n.Name.Equals(propName, StringComparison.CurrentCultureIgnoreCase));

			if (node == null)
				return false;

			bool ret = false;

			// attribute first
			if (node.Attributes["value"] != null)
			{
				//<propName value="propValue"/>
				//this is valid also
				prop.SetValue(instance, StringConverterManager.ConvertTo(prop.PropertyType, node.Attributes["value"].Value), null);
				ret = true;
			}
			else if (!String.IsNullOrEmpty(node.InnerText))
			{
				//<propName>propValue</propName>
				prop.SetValue(instance, StringConverterManager.ConvertTo(prop.PropertyType, node.InnerText), null);
				ret = true;
			}
			else
			{
				//this property is another POO
				prop.SetValue(instance, CreateEntity((XmlElement)node, prop.PropertyType), null);
			}
			return ret;
		}
	}
}
