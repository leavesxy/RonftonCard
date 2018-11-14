using Bluemoon.Config;
using RonftonCard.Common.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RonftonCard.Common
{
	/// <summary>
	/// manage Card context
	/// </summary>
	public class CardContextManager
	{
		private static IDictionary<String, CardTemplete> templetes;
		private static IDictionary<String, CardReaderDescriptor> readerDescriptors;

		#region "--- load configuration ---"

		public static bool LoadCardReaderConfiguration(String configFileName, String sectionName=null)
		{
			readerDescriptors = XmlConfigHelper.CreateEntity<IDictionary<String, CardReaderDescriptor>>(configFileName, sectionName);
			return readerDescriptors != null;
		}

		public static bool LoadCardConfigTemplete(String configFileName, String sectionName = null)
		{
			templetes = XmlConfigHelper.CreateEntity<IDictionary<String, CardTemplete>>(configFileName, sectionName);
			return templetes != null;
		}

		#endregion

		#region "---Properties ---"

		/// <summary>
		/// get all config templete names
		/// </summary>
		public static String[] TempleteNames
		{
			get
			{
				return templetes == null ? new String[] { } : templetes.Keys.ToArray();
			}
		}

		public static String[] CardReaderNames
		{
			get
			{
				return readerDescriptors == null ? new String[] { } : readerDescriptors.Keys.ToArray();
			}
		}

		public static int[] AddrDescriptors
		{
			get
			{
				return templetes[CurrentTempleteName].SegmentAddr;
			}
		}
		#endregion

		public static CardContext CreateContext()
		{
			return new CardContext()
			{
				CardType = CurrentCardType,
				ConfigTemplete = templetes[CurrentTempleteName],
				ReaderDescriptor = readerDescriptors[CurrentReaderDescriptor]
			};
		}

		public static String CurrentTempleteName { get; set; }

		public static String CurrentReaderDescriptor { get; set; }

		public static EntityCardType CurrentCardType { get; set; }
	}
}
