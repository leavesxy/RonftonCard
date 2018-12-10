using Bluemoon;
using log4net;
using RonftonCard.Core.CardReader;
using RonftonCard.Core.Config;
using RonftonCard.Core.Dongle;
using RonftonCard.Core.KeyService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RonftonCard.Core
{
	/// <summary>
	/// configuration management
	/// </summary>
	#pragma warning disable 168
	public class ConfigManager
	{
		///////////////////////////////////////////////////////////////////////////////////

		private static ILog logger = LogManager.GetLogger("RonftonCardLog");
		private static ICardReader cardReader;
		private static IKeyService keyService;

		///////////////////////////////////////////////////////////////////////////////////

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
				readerDescriptors = XmlConfigUtil.CreateEntity<IDictionary<String, CardReaderDescriptor>>(CardReaderConfigFileName);
				templeteDescriptors = XmlConfigUtil.CreateEntity<IDictionary<String, CardTempleteDescriptor>>(CardTempleteConfigFileName);
				dongleDescriptors = XmlConfigUtil.CreateEntity<IDictionary<String, DongleDescriptor>>(DongleConfigFileName);
				keyService  = new LocalTestKeyService(logger, GetDongle(), "012345");
			}
			catch (Exception ex)
			{
				return false;
			}
			return true;
		}


		/// <summary>
		/// create card reader instance
		/// </summary>
		public static bool OpenCardReader(String readerName)
		{
			bool ret = true;
			try
			{
				if (cardReader != null)
					cardReader.Close();

				CardReaderDescriptor desc = readerDescriptors[readerName];
				Type type = TypeUtil.ParseType(desc.DrvType);

				if (type != null)
					cardReader = (ICardReader)Activator.CreateInstance(type, new object[] { desc.Port, desc.Baud });

				ret = cardReader.Open();
			}
			catch (Exception ex)
			{
				logger.Error("GetCardReader error ! msg = " + ex.Message);
				ret = false;
			}
			return ret;
		}

		public static IKeyService GetKeyService()
		{
			return keyService;
		}

		public static ILog GetLogger()
		{
			return logger;
		}

		public static ICardReader GetCardReader()
		{
			return cardReader;
		}

		///////////////////////////////////////////////////////////////////////////////////

		public static String TempleteSelected { get; set; }

		public static String ReaderSelected { get; set; }

		public static String DongleSelected { get; set; }
		
		public static CardType CardType { get; set; }
		 
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

		public static String[] CardReaderName
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


		#region "--- Create instance from config ---"
		

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