using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluemoon;

namespace RonftonCard.Core.Dongle
{

	public enum DongleType : byte
	{
		[Alias("空KEY")]
		EMPTY = 0x00,

		/// <summary>
		/// company seed key
		/// </summary>
		[Alias("公司种子KEY")]
		COMPANY_SEED = 0x01,

		/// <summary>
		/// user root key
		/// </summary>
		[Alias("用户根密钥KEY")]
		USER_ROOT = 0x02,

		/// <summary>
		/// application authentication key
		/// </summary>
		[Alias("应用授权KEY")]
		AUTHEN = 0x03
	}
}