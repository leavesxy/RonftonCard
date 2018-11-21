using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.Dongle
{
	/// <summary>
	/// authentication mode
	/// </summary>
	public enum DongleAuthenMode
	{
		/// <summary>
		/// by admin pin
		/// </summary>
		ADMIN,

		/// <summary>
		/// by user pin
		/// </summary>
		USER,

		/// <summary>
		/// no pin
		/// </summary>
		ANONYMOUS
	}
}