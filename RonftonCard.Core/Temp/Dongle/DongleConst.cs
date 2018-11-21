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

		public const String DEFAULT_SEED_KEY = "0123456789abcdef";

		#region "---descriptor for key file---"

		// company seed key file
		public const ushort COMPANY_SEED_KEY_DESCRIPTOR = 0x1005;

		// user information file
		public const ushort USER_INFO_DESCRIPTOR = 0x1006;

		// user root key file(3DES)
		public const ushort USER_ROOT_KEY_DESCRIPTOR = 0x1007;

		// user authen key file(RSA)
		public const ushort AUTHEN_KEY_DESCRIPTOR = 0x1008;

		#endregion

		#region "--- const for dongle pin ---"

		// default pin for dongle
		public const String DEFAULT_ADMIN_PIN_DONGLE = "FFFFFFFFFFFFFFFF";
		public const String DEFAULT_USER_PIN_DONGLE = "12345678";

		#endregion

	}
}
