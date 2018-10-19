using BlueMoon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Common.DataTypeHandlers
{
	public class BcdHandler : IDataHandler
	{

		public bool CanHandle(CardDataType dataType)
		{
			return dataType == CardDataType.BCD;
		}

		public byte[] DoHandle(CardDataType dataType, Object instance, int length)
		{
			if (! (instance is String ))
				return new byte[] { };

			return HexString.FromString((String)instance);
		}
	}
}