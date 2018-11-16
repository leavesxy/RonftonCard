using Bluemoon;
using Bluemoon.Config;
using RonftonCard.Core.CardReader;
using RonftonCard.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RonftonCard.Core
{
	/// <summary>
	/// configuration management
	/// </summary>
	public class ConfigManager
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

		public static String TempleteSelected { get; set; }

		public static String ReaderSelected { get; set; }

		public static String DongleSelected { get; set; }
		
		/// <summary>
		/// get all config templete names
		/// </summary>
		public static String[] TempleteNames
		{
			get
			{
				return GetDescriptorKeys< CardTempleteDescriptor>(templeteDescriptors);
			}
		}

		public static String[] ReaderNames
		{
			get
			{
				return GetDescriptorKeys<CardReaderDescriptor>(readerDescriptors);
			}
		}

		public static String[] Dongles
		{
			get
			{
				return GetDescriptorKeys<DongleDescriptor>(dongleDescriptors);
			}
		}

		private static String[] GetDescriptorKeys<T>( IDictionary<String, T> descriptor )
		{
			return descriptor == null || descriptor.Keys.Count == 0 ? new String[] { } : descriptor.Keys.ToArray();
		}


		public static CardContext CreateCardContext()
		{
			return new CardContext()
			{
				CardTemplete = templeteDescriptors[TempleteSelected],
				Reader = GetCardReader()
			};
		}
		
		/// <summary>
		/// create card reader instance
		/// </summary>
		private static ICardReader GetCardReader()
		{
			ICardReader reader = null;
			try
			{
				CardReaderDescriptor desc = readerDescriptors[ReaderSelected];
				Type type = TypeUtil.ParseType(desc.DrvType);

				if (type != null)
					reader = (ICardReader)Activator.CreateInstance(type, new object[] { desc.Port, desc.Baud });
			}
			catch (Exception ex)
			{
			}
			return reader;
		}
	}
}