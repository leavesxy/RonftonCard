using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RonftonCard.Core.Dongle;

namespace RonftonCard.Dongle.RockeyArm
{
	using DONGLE_HANDLER = Int64;

	public class RockeyArmFactory : IDongleFactory
	{
		private DongleInfo[] dongleInfo;

		/// <summary>
		/// return clone information
		/// </summary>
		public DongleInfo[] Dongles
		{
			get
			{
				DongleInfo[] tempInfo = new DongleInfo[this.dongleInfo.Length];
				Array.Copy(this.dongleInfo, tempInfo, this.dongleInfo.Length);
				return tempInfo;
			}
		}


		public DongleInfo Enumerate()
		{
			throw new NotImplementedException();
		}

		public IDongleService GetDongleService(string keyId)
		{
			throw new NotImplementedException();
		}

		public IDongleService GetDongleService(int seq)
		{
			throw new NotImplementedException();
		}
	}
}
