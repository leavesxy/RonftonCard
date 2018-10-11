using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Common
{
	public class CardTempleteManager
	{
		private static IDictionary<String, CardTemplete> cardTempletes;

		public static bool LoadCardTemplete(String fileName)
		{
			return true;
		}

		public static List<String> GetTempleteNames()
		{
			return cardTempletes.Keys.ToList();
		}

		public static CardTemplete GetCardTemplete(String name)
		{
			if( cardTempletes.ContainsKey(name))
			{
				return cardTempletes[name];
			}
			return null;
		}
	}
}