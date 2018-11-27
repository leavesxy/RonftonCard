using Bluemoon;
using Bluemoon.Config;
using log4net;
using RonftonCard.Core.CardReader;
using RonftonCard.Core.Config;
using RonftonCard.Core.Dongle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RonftonCard.Core
{
	/// <summary>
	/// configuration management
	/// </summary>
	#pragma warning disable 168
	public class ConfigManager
	{
		protected static ILog logger = LogManager.GetLogger("RonftonCardLog");

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

		#region "--- selected ---"
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

		public static String[] DongleNames
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
		#endregion

		#region "--- Create instance from config ---"

		/// <summary>
		/// create card reader instance
		/// </summary>
		public static ICardReader GetCardReader()
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
				logger.Error("GetCardReader error ! msg = " + ex.Message);
			}
			return reader;
		}

		/// <summary>
		/// Create dongle instance
		/// </summary>
		public static IDongle GetDongle()
		{
			IDongle dongle = null;
			try
			{
				DongleDescriptor desc = dongleDescriptors[DongleSelected];
				Type type = TypeUtil.ParseType(desc.DrvType);

				if (type != null)
					dongle = (IDongle)Activator.CreateInstance(
						type, 
						new object[] { desc.Charset, desc.Seed,desc.ErrorMessageFileName });
			}
			catch (Exception ex)
			{
				logger.Error("GetDongle error ! msg = " + ex.Message);
			}
			return dongle;
		}

		public static CardTempleteDescriptor GetCardTemplete()
		{
			return templeteDescriptors[TempleteSelected];
		}

		#endregion
	}
}