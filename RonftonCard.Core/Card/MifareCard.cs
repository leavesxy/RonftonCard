using System;

namespace RonftonCard.Core.Card
{
	using CardReader;
	using Config;

	public class MifareCard : ICard
	{
		private IKeyService keyService;
		private ICardReader reader;
		private CardTempleteDescriptor cardTemplete;

		public MifareCard(CardTempleteDescriptor cardTemplete, IKeyService keyService, ICardReader reader)
		{
			this.cardTemplete = cardTemplete;
			this.reader = reader;
			this.keyService = keyService;

			if (!this.reader.Open())
				throw new Exception("Can't open reader !");
		}


		public bool Initialize()
		{
			// get sectors
			// select card id
			// get keyA & keyB
			// for loop
			//	use default pin to authen
			//	update KeyA,KeyB
			//	update Control block
			// reset
			// beep 

			return true;
		}

		public bool Personalize()
		{
			throw new NotImplementedException();
		}
	}
}