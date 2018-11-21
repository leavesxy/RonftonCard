using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.Dongle
{

	public enum DongleType : byte
	{
		/// <summary>
		/// company seed key
		/// </summary>
		COMPANY_SEED = 0x01,

		/// <summary>
		/// user root key
		/// </summary>
		USER_ROOT = 0x02,

		/// <summary>
		/// application authentication key
		/// </summary>
		AUTHEN = 0x03
	}
}