using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.Dongle
{
	/// <summary>
	/// define const for dongle key
	/// </summary>
	public class DongleConst
	{
		// length of RSA
		public const int RSA_KEY_LEN = 128;

		/// <summary>
		/// new admin pwd = BE5A1DB3DF98E81C 
		/// </summary>
		public const String DEFAULT_SEED_KEY = "0123456789abcdef";

		/// <summary>
		/// length of Key information file
		/// </summary>
		public const ushort KEY_INFO_FILE_LEN = 128;

		/// <summary>
		/// descriptor of Key info file
		/// </summary>
		public const ushort KEY_INFO_DESCRIPTOR = 0x1001;

		/// <summary>
		/// descriptor of company seed file
		/// </summary>
		public const ushort COMPANY_SEED_KEY_DESCRIPTOR = 0x1002;

		/// <summary>
		/// descriptor of user root key file
		/// </summary>
		public const ushort USER_ROOT_KEY_DESCRIPTOR = 0x1003;

		/// <summary>
		/// descriptor of authen private key file
		/// </summary>
		public const ushort AUTHEN_KEY_DESCRIPTOR = 0x1004;

	}
}
