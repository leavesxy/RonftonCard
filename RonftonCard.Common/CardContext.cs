using Bluemoon;
using RonftonCard.Common.Config;
using RonftonCard.Common.Reader;
using System;
using System.Collections.Generic;

namespace RonftonCard.Common
{
	/// <summary>
	/// context of card
	/// </summary>
	public class CardContext
	{
		/// <summary>
		/// type of Card
		/// </summary>
		public EntityCardType CardType { get; set; }

		/// <summary>
		/// config templete
		/// </summary>
		public CardTemplete ConfigTemplete { get; set; }

		/// <summary>
		/// card reader descriptor
		/// </summary>
		public CardReaderDescriptor ReaderDescriptor { get; set; }

		//public DongleKey Key { get; set; }

		/// <summary>
		/// arguments
		/// </summary>
		public IDictionary<String, Object> Args { get; private set; }

		public CardContext()
		{
			this.Args = new Dictionary<String,Object>();
		}


	}
}