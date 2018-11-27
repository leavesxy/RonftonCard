using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluemoon;

namespace RonftonCard.Core
{
	using DTO;

	public interface IKeyService
	{
		ResultArgs ComputeKey(CardKeyRequest[] request );
	}
}