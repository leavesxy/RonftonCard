using RonftonCard.Common.Config;
using RonftonCard.Common.Reader;
using System;
using System.Collections.Generic;

namespace RonftonCard.Common
{
	public class CardContext
	{
		public CardType CardType { get; set; }

		public CardConfigTemplete ConfigTemplete { get; set; }

		public CardReaderDescriptor ReaderDescriptor { get; set; }

		public IDictionary<String, Object> Args { get; private set; }

		public CardContext(IDictionary<String, Object> args)
		{
			this.Args = args;
		}

		public ICardReader GetCardReader(String readerName)
		{
			ICardReader reader = null;
			try
			{
				Type type = RonftonCard.Common.Util.TypeUtil.GetType(this.ReaderDescriptor.DrvType);

				if (type != null)
					reader = (ICardReader)Activator.CreateInstance(type, new object[] { this.ReaderDescriptor.PortType, this.ReaderDescriptor.Baud });
			}
			catch (Exception ex)
			{
			}
			return reader;
		}
	}
}