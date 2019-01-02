using System;
using log4net;
using Spring.Context;

namespace RonftonCard.Core
{
	using System.IO;
	using Bluemoon;
	using CardReader;
	using Spring.Context.Support;

	public class ContextManager
	{
		private const String LOGGER_CONFIG_FILE_NAME = @"etc\logger.xml";
		private const String SPRING_CONFIG_FILE_NAME = @"etc\spring4net.xml";

		private const String DEFAULT_LOGGER_NAME = "RonftonCardLogger";

		private static ILog logger;
		private static IApplicationContext applicationContext;

		#region "--- Init Context ---"
		public static bool Init()
		{
			return Init(LOGGER_CONFIG_FILE_NAME, DEFAULT_LOGGER_NAME);
		}

		public static bool Init(String configFileName, String loggerName)
		{
			// configure log4net
			String fullName;
			if (FileUtil.Lookup(configFileName, out fullName))
			{
				log4net.Config.XmlConfigurator.Configure(new FileInfo(fullName));
			}

			logger = LogManager.GetLogger( loggerName ?? DEFAULT_LOGGER_NAME);

			logger.Debug("ContextManager init ok !");

			// configure spring container
			applicationContext = new XmlApplicationContext(SPRING_CONFIG_FILE_NAME);
			return true;
		}

		#endregion

		#region "--- interface ---"
		public static ILog GetLogger()
		{
			return logger;
		}

		public static ICardReader GetCardReader()
		{
			if (!String.IsNullOrEmpty(ReaderSelected))
				return applicationContext.GetObject<ICardReader>(ReaderSelected);

			return null;
		}


		#endregion

		#region "--- static properties ---"
		public static String TempleteSelected { get; set; }

		public static String ReaderSelected { get; set; }

		public static String DongleSelected { get; set; }

		public static String CardTypeSelected { get; set; }

		#endregion
	}
}