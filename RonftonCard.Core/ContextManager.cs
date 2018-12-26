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
		private static ICardReader cardReader;

		public static bool Init(String loggerName)
		{
			// configure log4net
			String fullName;
			if (FileUtil.Lookup(LOGGER_CONFIG_FILE_NAME, out fullName))
			{
				log4net.Config.XmlConfigurator.Configure(new FileInfo(fullName));
			}

			logger = LogManager.GetLogger( loggerName ?? DEFAULT_LOGGER_NAME);

			logger.Debug("ContextManager init ok !");

			// configure spring container
			applicationContext = new XmlApplicationContext(SPRING_CONFIG_FILE_NAME);
			return true;
		}

		public static ILog GetLogger()
		{
			return logger;
		}

		public static IApplicationContext GetApplicationContext()
		{
			return applicationContext;
		}


		public static RT GetComponent<RT>(String name)
		{
			return applicationContext.GetObject<RT>(name);
		}
	}
}