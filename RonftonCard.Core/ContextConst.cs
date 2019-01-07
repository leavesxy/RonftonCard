using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core
{
	public class ContextConst
	{
		public const String CardReaderSelectedKey = "reader";

		public const String DongleSelectedKey = "dongle";

		public const String TempleteSelectedKey = "templete";

		public const String CardTypeSelectedKey = "cardType";

		// default logger name
		public const String DEFAULT_LOGGER_NAME = "RonftonCardLogger";

		// log4net & spring.net configuration file
		public const String LOGGER_CONFIG_FILE_NAME = @"etc\logger.xml";
		public const String SPRING_CONFIG_FILE_NAME = @"etc\spring4net.xml";
	}
}
