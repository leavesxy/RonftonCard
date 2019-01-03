using System;
using log4net;
using Spring.Context;
using Spring.Context.Support;

namespace RonftonCard.Core
{
	using System.IO;
	using Bluemoon;
	using CardReader;
	using Dongle;

	public class ContextManager
	{
		private const String LOGGER_CONFIG_FILE_NAME = @"etc\logger.xml";
		private const String SPRING_CONFIG_FILE_NAME = @"etc\spring4net.xml";

		private const String DEFAULT_LOGGER_NAME = "RonftonCardLogger";

		private static IApplicationContext applicationContext;
		public static ILog Logger { get; private set; }
		public static ICardReader Reader { get; private set; }
		public static IDongle Dongle { get; private set; }

		#region "--- Init Context ---"
		public static bool InitConfig()
		{
			return InitConfig(LOGGER_CONFIG_FILE_NAME, DEFAULT_LOGGER_NAME);
		}

		public static bool InitConfig(String configFileName, String loggerName)
		{
			// configure log4net
			String fullName;
			if (FileUtil.Lookup(configFileName, out fullName))
			{
				log4net.Config.XmlConfigurator.Configure(new FileInfo(fullName));
			}

			ContextManager.Logger = LogManager.GetLogger( loggerName ?? DEFAULT_LOGGER_NAME);

			ContextManager.Logger.Debug("ContextManager init ok !");

			// configure spring container
			ContextManager.applicationContext = new XmlApplicationContext(SPRING_CONFIG_FILE_NAME);
			return true;
		}

		#endregion

		public static void InitCardReader(String readerName )
		{
			if (ContextManager.Reader != null)
			{
				ContextManager.Reader.Close();
				ContextManager.Reader = null;
			}

			ContextManager.Reader = applicationContext.GetObject<ICardReader>(readerName);
		}

		public static void InitDongle(String dongleName)
		{
			if (ContextManager.Dongle != null)
			{
				ContextManager.Dongle.Close();
				ContextManager.Dongle = null;
			}

			ContextManager.Dongle = applicationContext.GetObject<IDongle>(dongleName);
		}
	}
}