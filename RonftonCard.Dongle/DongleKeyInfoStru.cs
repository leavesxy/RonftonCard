using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Dongle
{
	/// <summary>
	/// User info structure
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct DongleKeyInfoStru
	{
		public byte DongleType;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		public byte[] UserId;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		public byte[] UserName;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
		// yyyyMMddhhmmss
		public byte[] CreateDate;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public byte[] Operator;
	}
}
