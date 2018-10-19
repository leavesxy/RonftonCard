using RonftonCard.Common.Reader;
using System;
using System.Collections.Generic;

namespace RonftonCard.Common
{
	public class CardContext
	{
		public CardType CardType { get; set; }

		public CardTemplete CardTemplete { get; set; }

		public ICardReader CardReader { get; set; }

		public IDictionary<String, Object> Args { get; private set; }

		public CardContext()
		{
			this.Args = new Dictionary<String, Object>(StringComparer.CurrentCultureIgnoreCase);
		}
	}
}