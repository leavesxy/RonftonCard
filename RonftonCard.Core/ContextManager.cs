using System;
using log4net;
using Spring.Context;
using Spring.Context.Support;

namespace RonftonCard.Core
{
	using System.Collections.Generic;
	using System.IO;
	using Bluemoon;
	using CardReader;
	using Config;
	using Dongle;

	public class ContextManager
	{
		private static IDictionary<String, String> configSelected;

		private static IDictionary<String, CardTempleteDescriptor> templeteDescriptors;
		
		private static IApplicationContext applicationContext;
		public static ILog logger;
		public static ICardReader reader;
		public static IDongle dongle;

		#region "--- Initialize ---"
		public static bool Init()
		{
			return Init( ContextConst.DEFAULT_LOGGER_NAME );
		}

		public static bool Init(String loggerName)
		{
			return Init(ContextConst.LOGGER_CONFIG_FILE_NAME, loggerName);
		}
				
		public static bool Init(String loggerFileName, String loggerName)
		{
			configSelected = new Dictionary<String, String>();

			// configure log4net
			String fullName;
			if (FileUtil.Lookup(loggerFileName, out fullName))
			{
				log4net.Config.XmlConfigurator.Configure(new FileInfo(fullName));
			}

			ContextManager.logger = LogManager.GetLogger(loggerName ?? ContextConst.DEFAULT_LOGGER_NAME);

			// configure spring container
			ContextManager.applicationContext = new XmlApplicationContext( ContextConst.SPRING_CONFIG_FILE_NAME);
			
			return true;
		}

		private const String CardTempleteConfigFileName = "CardTemplete.xml";

		public static bool InitAll()
		{
			return InitAll(ContextConst.DEFAULT_LOGGER_NAME);
		}

		public static bool InitAll(String loggerName)
		{
			return InitAll(ContextConst.LOGGER_CONFIG_FILE_NAME, loggerName);
		}
		public static bool InitAll(String loggerFileName, String loggerName)
		{
			Init(loggerFileName, loggerName);

			// card templete
			ContextManager.templeteDescriptors = XmlConfigUtil.CreateEntity<IDictionary<String, CardTempleteDescriptor>>(CardTempleteConfigFileName);

			return true;
		}

		#endregion


		public static void SetConfigSelected(String configName, String selectedName)
		{
			if (!String.IsNullOrEmpty(configName) && !String.IsNullOrEmpty(selectedName))
			{
				configSelected[configName] = selectedName;
			}
		}

		public static void SetCardReaderSelected(String readerName)
		{
			SetConfigSelected( ContextConst.CardReaderSelectedKey, readerName);
		}

		public static void SetDongleSelected(String dongleName)
		{
			SetConfigSelected( ContextConst.DongleSelectedKey, dongleName);
		}

		public static void SetCardTempleteSelected(String templeteName)
		{
			SetConfigSelected(ContextConst.TempleteSelectedKey, templeteName);
		}


		public static ICardReader GetCardReader()
		{
			if (ContextManager.reader == null)
			{
				ContextManager.reader = applicationContext.GetObject<ICardReader>(configSelected[ ContextConst.CardReaderSelectedKey] );
			}
			return ContextManager.reader;
		}

		public static IDongle GetDongle()
		{
			if (ContextManager.dongle == null)
			{
				ContextManager.dongle = applicationContext.GetObject<IDongle>(configSelected[ ContextConst.DongleSelectedKey]);
			}
			return ContextManager.dongle;
		}

		public static ILog GetLogger()
		{
			return ContextManager.logger;
		}

		public static CardTempleteDescriptor GetCardTemplete()
		{
			return templeteDescriptors[ configSelected[ ContextConst.TempleteSelectedKey] ];
		}

		public static void Close()
		{
			if (ContextManager.dongle != null)
			{
				ContextManager.dongle.Close();
				ContextManager.dongle = null;
			}

			if (ContextManager.reader != null)
			{
				ContextManager.reader.Close();
				ContextManager.reader = null;
			}
		}
	}
}