using RonftonCard.Core.CardReader;
using RonftonCard.Core.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core
{
	public class CardContext
	{
		/// <summary>
		/// config templete
		/// </summary>
		public CardTempleteDescriptor CardTemplete { get; set; }

		/// <summary>
		/// card reader descriptor
		/// </summary>
		public ICardReader Reader { get; set; }

		/// <summary>
		/// arguments
		/// </summary>
		public IDictionary<String, Object> Args { get; private set; }

	}
}
