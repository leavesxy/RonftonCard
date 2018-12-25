using System;
using System.IO;
using System.Runtime.CompilerServices;
using Bluemoon;
using RonftonCard.Core.CardReader;
using Spring.Context;
using Spring.Context.Support;
using log4net;

namespace RonftonCard.CardService
{
    public class ContextManager
	{
		private const String LOGGER_FILE_NAME = @"etc\logger.xml";
		private const String SPRING_FILE_NAME = @"etc\spring4net.xml";
        private const String LOGGER_NAME = "RonftonCardService";


        private static ILog logger;
		private static IApplicationContext applicationContext;
		private static ICardReader cardReader;

		public static bool Init()
		{
			// configure log4net
			String fullName;
			if (FileUtil.Lookup(LOGGER_FILE_NAME, out fullName))
			{
				log4net.Config.XmlConfigurator.Configure(new FileInfo(fullName));
			}

			logger = LogManager.GetLogger(LOGGER_NAME);

            logger.Debug("ContextManager init ok !");

			// configure spring container
			applicationContext = new XmlApplicationContext(SPRING_FILE_NAME);
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

		private const String READER_NAME = "cardReader";

		[MethodImpl( MethodImplOptions.Synchronized )]
		public static ICardReader GetCardReader()
		{
			if( cardReader == null )
				cardReader = applicationContext.GetObject<ICardReader>(READER_NAME);

			return cardReader;
		}
	}
}