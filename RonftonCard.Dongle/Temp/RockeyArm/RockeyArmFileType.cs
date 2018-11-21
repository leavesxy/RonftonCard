using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Dongle.RockeyArm
{
	/// <summary>
	/// define file type stored in dongle key
	/// </summary>
	public sealed class RockeyArmFileType
	{
		//common data file
		public const ushort FILE_DATA = 1;

		//RSA private key file
		public const ushort FILE_PRIKEY_RSA = 2;

		//ECC or SM2 private key file
		public const ushort FILE_PRIKEY_ECCSM2 = 3;

		//3DES or SM4 private key file
		public const ushort FILE_KEY = 4;

		//executable file
		public const ushort FILE_EXE = 5;
	}
}
