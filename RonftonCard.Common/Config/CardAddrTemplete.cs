using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Common.Config
{
	public class CardAddrTemplete : AbstractConfigTemplete
	{
		private IDictionary<String, List<CardAddrItem>> cardAddr;

		private const String NODE_TAG_NAME = "card";
		private const String ITEM_TAG_NAME = "addr";

		public CardAddrTemplete(String fileName)
			: this(fileName, NODE_TAG_NAME, ITEM_TAG_NAME)
		{
		}

		public CardAddrTemplete(String fileName, String nodeTagName, String itemTagName)
			: base(nodeTagName, itemTagName )
		{
			this.cardAddr = new Dictionary<String, List<CardAddrItem>>();
			base.LoadConfiguration<CardAddrItem>(fileName);
		}
		
		protected override void AddConfig<RT>(String name, List<RT> items)
		{
			if (typeof(RT).Equals(typeof(CardAddrItem)))
			{
				this.cardAddr.Add(name, items as List<CardAddrItem>);
			}
		}

		public override List<String> GetTempleteName()
		{
			return this.cardAddr.Keys.ToList();
		}

		public List<CardAddrItem> GetTempleteItem(String name)
		{
			return this.cardAddr[name];
		}

	}
}