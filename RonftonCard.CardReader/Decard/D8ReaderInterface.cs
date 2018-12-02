using System;
using System.Runtime.InteropServices;

namespace RonftonCard.CardReader.Decard
{
	using DEV_HANDLER = System.Int32;

	/// <summary>
	/// interface for card reader device
	/// </summary>
	public partial class D8Reader
	{
		#region "--- device interface ---"
		[DllImport(@"Plugin\dcrf32.dll")]
		static extern short dc_init(Int16 port, int baud);

		[DllImport(@"Plugin\dcrf32.dll")]
		static extern short dc_exit(DEV_HANDLER hReader);

		[DllImport(@"Plugin\dcrf32.dll")]
		static extern short dc_beep(DEV_HANDLER hReader, ushort duration);

		[DllImport(@"Plugin\dcrf32.dll")]
		static extern short dc_light(DEV_HANDLER hReader, ushort flag);

		/// <summary>
		/// verstion allocate 128 bytes at least 
		/// </summary>
		[DllImport(@"Plugin\dcrf32.dll")]
		static extern short dc_getver(DEV_HANDLER hReader, [Out] byte[] version);

		/// <summary>
		/// msec : millisecond, it is recommanded to set 10 msec
		/// </summary>
		[DllImport(@"Plugin\dcrf32.dll")]
		static extern short dc_reset(DEV_HANDLER hReader, ushort msec);

		/// <summary>
		/// initialize,
		/// cardType : 'A' --- TypeA, 'B' --- TypeB
		/// </summary>
		[DllImport("dcrf32.dll")]
		static extern short dc_config_card(DEV_HANDLER hReader, char cardType);

		#endregion

		#region "--- M1 Card interface ---"

		[DllImport(@"Plugin\dcrf32.dll")]
		static extern short dc_request(DEV_HANDLER hReader, byte mode, ref ushort atqa);

		/// <summary>
		/// bcnt : 0x00
		/// </summary>
		[DllImport(@"Plugin\dcrf32.dll")]
		static extern short dc_anticoll(DEV_HANDLER hReader, byte bcnt, [Out]byte[] cardId);

		[DllImport(@"Plugin\dcrf32.dll")]
		static extern short dc_select(DEV_HANDLER hReader, byte[] cardId, ref byte sak);


		/// <summary>
		/// include dc_request,dc_anticol1,dc_select,dc_anticoll2,dc_select2
		/// mode : 0x00 -- for free card, 0x01 for all card
		/// </summary>
		[DllImport(@"Plugin\dcrf32.dll")]
		static extern short dc_card_n(DEV_HANDLER hReader, byte mode, ref uint cardIdLen, [Out]byte[] cardId);

		/// <summary>
		/// update control block,include KeyA,control block,KeyB
		/// keyA & keyB are six bytes
		/// b0 --- bit for controlling block 0 (or 0~4),
		/// the low three bit (D2D1D0) equal 
		/// b0 --- C10,C20,C30
		/// b1 --- C11,C21,C31
		/// b2 --- C12,C22,C32
		/// b3 --- C13,C23,C33
		/// bk reserved, 0x00 default
		/// </summary>
		[DllImport(@"Plugin\dcrf32.dll")]
		static extern short dc_changeb3(DEV_HANDLER hReader, byte sector, [In]byte[] keyA, byte b0, byte b1, byte b2, byte b3, byte bk, [In]byte[] keyB);

		/// <summary>
		/// read block data
		/// block : block number
		/// M1: S50(0~63), S70(0~255)
		/// ML: 0~11
		/// </summary>
		[DllImport(@"Plugin\dcrf32.dll")]
		static extern short dc_read(DEV_HANDLER hReader, byte block, [Out] byte[] outData);

		[DllImport(@"Plugin\dcrf32.dll")]
		static extern short dc_write(DEV_HANDLER hReader, byte block, [In] byte[] inData);

		/// <summary>
		/// verify pin
		/// mode : 0x00--KeyA; 0x04--KeyB
		/// </summary>
		[DllImport(@"Plugin\dcrf32.dll")]
		static extern short dc_authentication_passaddr(DEV_HANDLER hReader, byte mode, byte block, [In]byte[] pin);
		#endregion
	}
}