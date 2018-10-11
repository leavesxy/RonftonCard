using BlueMoon.Util;
using RonftonCard.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RonftonCard.Common.Config
{
	/// <summary>
	/// card structure defined
	/// </summary>
	public class CardStruTemplete : AbstractCardTemplete
	{
		private IDictionary<String, List<CardStruItem>> cardStru;

		private const String NODE_TAG_NAME = "data";
		private const String ITEM_TAG_NAME = "item";

		public CardStruTemplete(String fileName)
			: this(fileName, NODE_TAG_NAME, ITEM_TAG_NAME)
		{
		}

		public CardStruTemplete(String fileName, String nodeTagName, String itemTagName)
			: base(nodeTagName, itemTagName )
		{
			this.cardStru = new Dictionary<String, List<CardStruItem>>();
			base.LoadConfiguration<CardStruItem>(fileName);
		}


		protected override void AddConfig<RT>(String name, List<RT> items)
		{
			if( typeof(RT).Equals( typeof(CardStruItem)) )
			{
				this.cardStru.Add(name, items as List<CardStruItem> );
			}
		}

		public override List<String> GetTempleteName()
		{
			return this.cardStru.Keys.ToList();
		}

		public List<CardStruItem> GetTempleteItem(String name)
		{
			return this.cardStru[name];
		}
	}
}
