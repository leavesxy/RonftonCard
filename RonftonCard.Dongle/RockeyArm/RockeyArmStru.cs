﻿using System;
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
		/// rockey arm key device information
		/// </summary>
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

		//数据文件属性数据结构
		[StructLayout(LayoutKind.Sequential)]
		public struct DATA_FILE_ATTR
		{
			public uint m_Size;      //数据文件长度，该值最大为4096
			public DATA_LIC m_Lic;       //授权
		}

		//数据文件授权结构
		[StructLayout(LayoutKind.Sequential)]
		public struct DATA_LIC
		{
			public ushort m_Read_Priv;     //读权限: 0为最小匿名权限，1为最小用户权限，2为最小开发商权限            	
			public ushort m_Write_Priv;    //写权限: 0为最小匿名权限，1为最小用户权限，2为最小开发商权限
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

		//对称加密算法(SM4/TDES)密钥文件授权结构
		[StructLayout(LayoutKind.Sequential)]
		public struct KEY_LIC
		{
			public uint m_Priv_Enc;   //加密时的调用权限: 0为最小匿名权限，1为最小用户权限，2为最小开发商权限
		}

		//对称加密算法(SM4/TDES)密钥文件属性数据结构
		[StructLayout(LayoutKind.Sequential)]
		public struct KEY_FILE_ATTR
		{
			public uint m_Size;			//密钥数据长度=16
			public KEY_LIC m_Lic;		//授权
		}

		//ECCSM2/RSA私钥文件属性数据结构
		[StructLayout(LayoutKind.Sequential)]
		public struct PRIKEY_FILE_ATTR
		{
			public ushort m_Type;       //数据类型:ECCSM2私钥 或 RSA私钥
			public ushort m_Size;       //数据长度:RSA该值为1024或2048, ECC该值为192或256, SM2该值为0x8100
			public PRIKEY_LIC m_Lic;        //授权
		}

		//私钥文件授权结构
		[StructLayout(LayoutKind.Sequential)]
		public struct PRIKEY_LIC
		{
			public uint m_Count;        //可调次数: 0xFFFFFFFF表示不限制, 递减到0表示已不可调用
			public byte m_Priv;         //调用权限: 0为最小匿名权限，1为最小用户权限，2为最小开发商权限
			public byte m_IsDecOnRAM;   //是否是在内存中递减: 1为在内存中递减，0为在FLASH中递减
			public byte m_IsReset;      //用户态调用后是否自动回到匿名态: TRUE为调后回到匿名态 (开发商态不受此限制)
			public byte m_Reserve;      //保留,用于4字节对齐
		}

	}
}
