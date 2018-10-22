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
	public class CardContextManager
	{
		private static IDictionary<String, CardConfigTemplete> configTempletes;
		private static IDictionary<String, CardReaderDescriptor> readerDescriptors;

		#region "--- load configuration ---"

		public static bool LoadCardReaderConfiguration(String configFileName, String sectionName=null)
		{
			readerDescriptors = XmlUtil.CreateEntity<IDictionary<String, CardReaderDescriptor>>(configFileName, sectionName);
			return readerDescriptors != null;
		}

		public static bool LoadCardConfigTemplete(String configFileName, String sectionName = null)
		{
			configTempletes = XmlUtil.CreateEntity<IDictionary<String, CardConfigTemplete>>(configFileName, sectionName);
			return configTempletes != null;
		}

		#endregion

		#region "---Properties ---"
		public static String[] CardTempleteNames
		{
			get
			{
				return configTempletes == null ? new String[] { } : configTempletes.Keys.ToArray();
			}
		}

		public static String[] CardReaderNames
		{
			get
			{
				return readerDescriptors == null ? new String[] { } : readerDescriptors.Keys.ToArray();
			}
		}
		#endregion

		public static CardContext CreateContext(String templeteName, String readerName)
		{
			return new CardContext()
			{
				ConfigTemplete = configTempletes[templeteName],
				ReaderDescriptor = readerDescriptors[readerName]
			};
		}
	}
}
