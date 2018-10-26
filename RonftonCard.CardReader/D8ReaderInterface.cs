using System;
using System.Runtime.InteropServices;

namespace RonftonCard.CardReader
{
	public partial class D8Reader
	{
		[DllImport(@"Plugin\dcrf32.dll")]
		public static extern short dc_init(Int16 port, int baud);

		[DllImport(@"Plugin\dcrf32.dll")]
		public static extern short dc_exit(int hdev);

		[DllImport(@"Plugin\dcrf32.dll")]
		public static extern short dc_beep(int hdev, ushort duration);

		[DllImport(@"Plugin\dcrf32.dll")]
		public static extern short dc_light(int hdev, ushort onOff);

		[DllImport(@"Plugin\dcrf32.dll")]
		public static extern short dc_getver(int hdev, [Out] byte[] ver);


		/**
		 * @brief  寻卡请求、防卡冲突、选卡操作。
		 * 内部包含了 ::dc_request ::dc_anticoll ::dc_select ::dc_anticoll2 ::dc_select2 的功能。
		 * @param[in] icdev 设备标识符。
		 * @param[in] _Mode 模式，同 ::dc_request 的 @a _Mode 。
		 * @param[out] SnrLen 返回卡序列号的长度。
		 * @param[out] _Snr 返回的卡序列号，请至少分配8个字节。
		 */
		[DllImport(@"Plugin\dcrf32.dll")]
		public static extern short dc_card_n(int hdev, byte mode, ref uint cardIdLen, [Out]byte[] cardId);

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
		public static extern short dc_changeb3(int hdev, byte sector, [In]byte[] keyA, byte b0, byte b1, byte b2, byte b3, byte bk, [In]byte[] keyB);

		/**
		 * 读取卡内数据，对于M1卡，一次读取一个块的数据，为16个字节；对于ML卡，一次读取相同属性的两页，为8个字节。
		 * @param[in] icdev 设备标识符。
		 * @param[in] _Adr 地址。
		 * @n M1卡 - S50块地址（0~63），S70块地址（0~255）。
		 * @n ML卡 - 页地址（0~11）。
		 * @param[out] _Data 固定返回16个字节数据，真实数据可能小于16个字节。
		 */
		[DllImport(@"Plugin\dcrf32.dll")]
		public static extern short dc_read(int hdev, byte addr, [Out] byte[] data);

		/**
		 * 写入数据到卡片内，对于M1卡，一次必须写入一个块的数据，为16个字节；对于ML卡，一次必须写入一个页的数据，为4个字节。
		 * @param[in] icdev 设备标识符。
		 * @param[in] _Adr 地址。
		 * @n M1卡 - S50块地址（1~63），S70块地址（1~255）。
		 * @n ML卡 - 页地址（2~11）。
		 * @param[out] _Data 固定传入16个字节数据，真实数据可能小于16个字节。
		 */
		[DllImport(@"Plugin\dcrf32.dll")]
		public static extern short dc_write(int hdev, byte addr, [In] byte[] data);

		/**
		 * 使用传入的密码来验证M1卡密码。
		 * @param[in] icdev 设备标识符。
		 * @param[in] _Mode 模式，0x00表示验证A密码，0x04表示验证B密码。
		 * @param[in] _Addr 要验证密码的块号。
		 * @param[in] passbuff 密码，固定为6个字节。
		 */
		[DllImport(@"Plugin\dcrf32.dll")]
		public static extern short dc_authentication_passaddr(int hdev, byte mode, byte addr, [In]byte[] pwd);
	}
}