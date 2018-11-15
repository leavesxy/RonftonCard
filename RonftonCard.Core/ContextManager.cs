using Bluemoon;
using Bluemoon.Config;
using RonftonCard.Core.CardReader;
using RonftonCard.Core.Config;
using System;
using System.Collections.Generic;

namespace RonftonCard.Core
{
	/// <summary>
	/// context management
	/// </summary>
	public class ContextManager
	{
		private static IDictionary<String, CardTempleteDescriptor> templeteDescriptors;
		private static IDictionary<String, CardReaderDescriptor> readerDescriptors;
		private static IDictionary<String, DongleDescriptor> dongleDescriptors;

		private const String CardReaderConfigFileName = "CardReader.xml";
		private const String CardTempleteConfigFileName = "CardTemplete.xml";
		private const String DongleConfigFileName = "Dongle.xml";

		public static bool Init()
		{
			try
			{
				readerDescriptors = XmlConfigHelper.CreateEntity<IDictionary<String, CardReaderDescriptor>>(CardReaderConfigFileName);
				templeteDescriptors = XmlConfigHelper.CreateEntity<IDictionary<String, CardTempleteDescriptor>>(CardTempleteConfigFileName);
				dongleDescriptors = XmlConfigHelper.CreateEntity<IDictionary<String, DongleDescriptor>>(DongleConfigFileName);
			}
			catch (Exception ex)
			{
				return false;
			}
			return true;
		}

		public static String CurrentTempleteName { get; set; }

		public static String CurrentReaderDescriptor { get; set; }

		public static String CurrentDongle { get; set; }

		//public static EntityCardType CurrentCardType { get; set; }

		/// <summary>
		/// create card reader instance
		/// </summary>
		public static ICardReader GetCardReader()
		{
			ICardReader reader = null;
			try
			{
				CardReaderDescriptor desc = readerDescriptors[CurrentReaderDescriptor];
				Type type = TypeUtil.ParseType(desc.DrvType);

				if (type != null)
					reader = (ICardReader)Activator.CreateInstance(type, new object[] { desc.Port, desc.Baud });
			}
			catch (Exception ex)
			{
			}
			return reader;
		}

		/// <summary>
		/// get all config templete names
		/// </summary>
		public static String[] TempleteNames
		{
			get
			{
				return templeteDescriptors == null ? new String[] { } : templeteDescriptors.get;
			}
		}

		public static String[] CardReaderNames
		{
			get
			{
				return readerDescriptors == null ? new String[] { } : readerDescriptors.Keys.ToArray();
			}
		}

		public static String[] Dongles
		{
			get
			{
				return dongleDescriptors == null ? new String[] { } : dongleDescriptors.Keys.ToArray();
			}
		}
	}
}
