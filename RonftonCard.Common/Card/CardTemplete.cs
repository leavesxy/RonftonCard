using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Common.Card
{
	public class CardTemplete
	{
		public String TempleteName { get; private set; }
		public List<DataItemDescriptor> DataItems;
		public List<StorageItemDescriptor> StorageItems;

		public CardTemplete()
		{

		}
	}
}
