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
		static extern uint Dongle_Open(ref Int64 phDongle, int nIndex);

		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_Close(Int64 hDongle);

		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_VerifyPIN(Int64 hDongle, uint nFlags, byte[] pPIN, out int pRemainCount);

		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_RFS(Int64 hDongle);

		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_CreateFile(Int64 hDongle, uint nFileType, ushort wFileID, IntPtr pFileAttr);

	}
}
