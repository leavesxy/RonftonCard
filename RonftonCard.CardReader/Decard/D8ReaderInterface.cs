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
		static extern short dc_light(DEV_HANDLER hReader, ushort onOff);

		/// <summary>
		/// verstion allocate 128 bytes at least 
		/// </summary>
		[DllImport(@"Plugin\dcrf32.dll")]
		static extern short dc_getver(DEV_HANDLER hReader, [Out] byte[] version);

		#endregion

		/// <summary>
		/// include dc_request,dc_anticol1,dc_select,dc_anticoll2,dc_select2
		/// mode : 0x00 -- for free card, 0x01 for all card
		/// </summary>
		[DllImport(@"Plugin\dcrf32.dll")]
		static extern short dc_card_n(DEV_HANDLER hReader, byte mode, ref uint cardIdLen, [Out]byte[] cardId);

		/**
		 * 修改M1卡密码配置块数据，M1卡密码配置块也就是每个扇区的最后一块，包含密码A、控制字节、密码B数据。
		 * @param[in] icdev 设备标识符。
		 * @param[in] _SecNr 扇区号。
		 * @param[in] _KeyA 密码A，固定为6个字节。
		 * @param[in] _B0 块0控制字（当一扇区有16块时，对应为块0~4的控制字），低3位（D2D1D0）对应C10、C20、C30。
		 * @param[in] _B1 块1控制字（当一扇区有16块时，对应为块5~9的控制字），低3位（D2D1D0）对应C11、C21、C31。
		 * @param[in] _B2 块2控制字（当一扇区有16块时，对应为块10~14的控制字），低3位（D2D1D0）对应C12、C22、C32。
		 * @param[in] _B3 块3控制字（当一扇区有16块时，对应为块15的控制字），低3位（D2D1D0）对应C13、C23、C33。
		 * @param[in] _Bk 保留，固定为0x00。
		 * @param[in] _KeyB 密码B，固定为6个字节。
		 */
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
	}
}