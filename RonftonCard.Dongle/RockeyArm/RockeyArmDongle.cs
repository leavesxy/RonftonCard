using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluemoon;
using RonftonCard.Core.Dongle;

namespace RonftonCard.Dongle.RockeyArm
{
	using DONGLE_HANDLER = Int64;

	public partial class RockeyArmDongle : IDongle
	{

		public void Close(DONGLE_HANDLER hDongle)
		{
			if (hDongle != -1)
			{
				Dongle_Close(hDongle);
			}
		}
	}
}
