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
		[StructLayout(LayoutKind.Sequential)]
		public struct DONGLE_INFO
		{
			public ushort m_Ver;				//COS_version, example: 0x0201 -> v2.01             	
			public ushort m_Type;				//产品类型: 0xFF表示标准版, 0x00为时钟锁,0x01为带时钟的U盘锁,0x02为标准U盘锁  
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			public byte[] m_BirthDay;			//出厂日期
			public uint m_Agent;				//代理商编号,比如:默认的0xFFFFFFFF
			public uint m_PID;					//产品ID
			public uint m_UserID;				//用户ID
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]		
			public byte[] m_HID;				//8字节的硬件ID
			public uint m_IsMother;				//母锁标志: 0x01表示是母锁, 0x00表示不是母锁
			public uint m_DevType;				//设备类型(PROTOCOL_HID或者PROTOCOL_CCID)
		}

		//RSA_PUB_KEY(support 1024,2048)
		[StructLayout(LayoutKind.Sequential)]
		public struct RSA_PUBLIC_KEY
		{
			public uint bits;                   // length in bits of modulus        	
			public uint modulus;                // modulus
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
			public byte[] exponent;             // public exponent
		}

		//RSA_PRI_KEY(support 1024,2048)
		[StructLayout(LayoutKind.Sequential)]
		public struct RSA_PRIVATE_KEY
		{
			public uint bits;                       // length in bits of modulus        	
			public uint modulus;                    // modulus  
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
			public byte[] publicExponent;           // public exponent
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
			public byte[] exponent;                 // public exponent
		}
	}
}
