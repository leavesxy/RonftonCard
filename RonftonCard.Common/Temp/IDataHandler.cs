using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Common.DataTypeHandlers
{
	public interface IDataHandler
	{
		bool CanHandle(CardDataType dataType);

		byte[] DoHandle(CardDataType dataType, Object value, int length);
	}
}
