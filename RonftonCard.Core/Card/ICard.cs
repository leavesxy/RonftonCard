using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.Card
{
	public interface ICard
	{
		bool Initialize();

		bool Personalize();

		bool Restore();
	}
}
