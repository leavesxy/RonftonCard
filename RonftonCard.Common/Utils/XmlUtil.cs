using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RonftonCard.Common.Utils
{
	public static class XmlUtil
	{
		public static String GetAttributeValue(this XmlNode @this, String attrName, String defaultValue="")
		{
			return @this.Attributes[attrName] == null ? defaultValue : @this.Attributes[attrName].Value;
		}
	}
}