using System;
using System.Runtime.InteropServices;

namespace RonftonCard.CardReader
{
	public partial class D8Reader
	{
		[DllImport("dcrf32.dll")]
		public static extern short dc_init(Int16 port, int baud);

		[DllImport("dcrf32.dll")]
		public static extern short dc_exit(int hdev);

		[DllImport("dcrf32.dll")]
		public static extern short dc_beep(int hdev, ushort duration);

		[DllImport("dcrf32.dll")]
		public static extern short dc_light(int hdev, ushort onOff);

		[DllImport("dcrf32.dll")]
		public static extern short dc_getver(int hdev, [Out] byte[] ver);
	}
}
