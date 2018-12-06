using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Dongle.RockeyArm
{
	using DONGLE_HANDLER = Int64;
	public partial class RockeyArmDongle
	{
		/// <summary>
		/// enumerate dongle, if IntPtr is null, return count of keys
		/// </summary>
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_Enum(IntPtr pDongleInfo, out Int64 count);

		/// <summary>
		/// Open device, should invoke Dongle_Enum first
		/// </summary>
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_Open(ref DONGLE_HANDLER hDongle, int seq);

		/// <summary>
		/// close device
		/// </summary>
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_Close(DONGLE_HANDLER hDongle);

		/// <summary>
		/// verfiy protected pin
		/// </summary>
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_VerifyPIN(DONGLE_HANDLER hDongle, uint flag, byte[] pin, out int pRemainCount);

		/// <summary>
		/// restore
		/// </summary>
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_RFS(DONGLE_HANDLER hDongle);

		/// <summary>
		/// reset status as anonymous
		/// </summary>
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_ResetState(DONGLE_HANDLER hDongle);

		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_SetUserID(DONGLE_HANDLER hDongle, uint dwUserID);

		/// <summary>
		/// unique key, reset app_id
		/// </summary>
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_GenUniqueKey(DONGLE_HANDLER hDongle, int seedLen, byte[] seed, byte[] appId, byte[] newAdminPin);

		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_CreateFile(DONGLE_HANDLER hDongle, uint fileType, ushort descriptor, IntPtr pFileAttr);

		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_WriteFile(DONGLE_HANDLER hDongle, uint fileType, ushort descriptor, short offset, byte[] buffer, int length);

		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_ReadFile(DONGLE_HANDLER hDongle, ushort descriptor, short offset, byte[] buffer, int length);

		/// <summary>
		/// TripleDES encrypt
		/// flag=0 Encrypt, flag=1 Decrypt 
		/// </summary>
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_TDES(DONGLE_HANDLER hDongle, ushort descriptor, uint flag, byte[] plain, byte[] cipher, uint len);

		/// <summary>
		/// generate private & public pair
		/// </summary>
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_RsaGenPubPriKey(DONGLE_HANDLER hDongle, ushort descriptor, IntPtr pubKey, IntPtr priKey);

		/// <summary>
		/// private key encrypt
		/// </summary>
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_RsaPri(DONGLE_HANDLER hDongle, ushort descriptor, uint flag, byte[] plain, uint plainLen, byte[] cipher, ref uint cipherLen);

		/// <summary>
		/// get UTC time of dongle
		/// </summary>
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_GetUTCTime(DONGLE_HANDLER hDongle, ref uint pdwUTCTime);

		/// <summary>
		/// flag:0(off),1(on),2(blink)
		/// </summary>
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_LEDControl(DONGLE_HANDLER hDongle, uint flag);
	}
}