using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Bluemoon;
using Common.Logging;
using RonftonCard.Core.CardReader;
using Spring.Context;
using Spring.Context.Support;

namespace RonftonCard.CardService
{
	public class ContextManager
	{
		private const String LOGGER_FILE_NAME = @"etc/logger.xml";
		private const String SPRING_FILE_NAME = @"etc/spring4net.xml";

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

			logger = LogManager.GetLogger("RonftonCardService");

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