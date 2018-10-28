using BlueMoon;
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
		public CardType CardType { get; set; }

		/// <summary>
		/// config templete
		/// </summary>
		public CardTemplete ConfigTemplete { get; set; }

		/// <summary>
		/// card reader descriptor
		/// </summary>
		public CardReaderDescriptor ReaderDescriptor { get; set; }

		/// <summary>
		/// arguments
		/// </summary>
		public IDictionary<String, Object> Args { get; private set; }

		public CardContext()
		{
			this.Args = new Dictionary<String,Object>();
		}

		/// <summary>
		/// create card reader instance
		/// </summary>
		/// <returns></returns>
		public ICardReader GetCardReader()
		{
			ICardReader reader = null;
			try
			{
				Type type = TypeHelper.ParseType(this.ReaderDescriptor.DrvType);

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