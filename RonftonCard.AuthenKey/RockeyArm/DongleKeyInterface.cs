using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.AuthenKey.RockeyArm
{
	public partial class DongleKey
	{
		/// <summary>
		/// enumerate Key, if IntPtr is null, return count of keys
		/// </summary>
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_Enum(IntPtr pDongleInfo, out long pCount);
		
		/// <summary>
		/// Open dog, should invoke Dongle_Enum
		/// </summary>
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_Open(ref Int64 hDongle, int seq);

		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_Close(Int64 hDongle);

		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_VerifyPIN(Int64 hDongle, uint flag, byte[] pin, out int pRemainCount);

		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_RFS(Int64 hDongle);

		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_CreateFile(Int64 hDongle, uint fileType, ushort descriptor, IntPtr pFileAttr);

		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_WriteFile(Int64 hDongle, uint fileType, ushort descriptor, short offset, byte[] buffer, int bufferLen);

		//[DllImport(@"Plugin\Dongle_d.dll")]
		//static extern uint Dongle_RsaGenPubPriKey(Int64 hDongle, ushort wPriFileID, ref RSA_PUBLIC_KEY pPubBakup, ref RSA_PRIVATE_KEY pPriBakup);
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_RsaGenPubPriKey(Int64 hDongle, ushort descriptor, IntPtr pubKey, IntPtr priKey);

	}
}
