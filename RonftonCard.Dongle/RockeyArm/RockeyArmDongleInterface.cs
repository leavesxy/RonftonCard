using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Dongle.RockeyArm
{
	public partial class RockeyArmDongle
	{
		/// <summary>
		/// enumerate dongle, if IntPtr is null, return count of keys
		/// </summary>
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_Enum(IntPtr pDongleInfo, out long pCount);

		/// <summary>
		/// Open device, should invoke Dongle_Enum first
		/// </summary>
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_Open(ref Int64 hDongle, int seq);

		/// <summary>
		/// close device
		/// </summary>
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_Close(Int64 hDongle);

		/// <summary>
		/// verfiy protected pin
		/// </summary>
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_VerifyPIN(Int64 hDongle, uint flag, byte[] pin, out int pRemainCount);

		/// <summary>
		/// restore
		/// </summary>
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_RFS(Int64 hDongle);

		/// <summary>
		/// reset status as anonymous
		/// </summary>
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_ResetState(Int64 hDongle);

		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_SetUserID(Int64 hDongle, uint dwUserID);

		/// <summary>
		/// unique key, reset app_id
		/// </summary>
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_GenUniqueKey(Int64 hDongle, int seedLen, byte[] seed, byte[] appId, byte[] newAdminPin);

		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_CreateFile(Int64 hDongle, uint fileType, ushort descriptor, IntPtr pFileAttr);

		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_WriteFile(Int64 hDongle, uint fileType, ushort descriptor, short offset, byte[] buffer, int bufferLen);

		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_RsaGenPubPriKey(Int64 hDongle, ushort descriptor, IntPtr pubKey, IntPtr priKey);

		/// <summary>
		/// flag=0 Encrypt, flag=1 Decrypt 
		/// </summary>
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_TDES(Int64 hDongle, ushort descriptor, uint flag, byte[] plain, byte[] cipher, uint len);

		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_RsaPri(Int64 hDongle, ushort descriptor, uint flag, byte[] plain, uint plainLen, byte[] cipher, ref uint cipherLen);

	}
}
