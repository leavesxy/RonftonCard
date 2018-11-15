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
		/*************************文件授权结构***********************************/
		//数据文件授权结构
		[StructLayout(LayoutKind.Sequential)]
		public struct DATA_LIC
		{
			public ushort m_Read_Priv;     //读权限: 0为最小匿名权限，1为最小用户权限，2为最小开发商权限            	
			public ushort m_Write_Priv;    //写权限: 0为最小匿名权限，1为最小用户权限，2为最小开发商权限
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

		//对称加密算法(SM4/TDES)密钥文件授权结构
		[StructLayout(LayoutKind.Sequential)]
		public struct KEY_LIC
		{
			public uint m_Priv_Enc;   //加密时的调用权限: 0为最小匿名权限，1为最小用户权限，2为最小开发商权限
		}


		//可执行文件授权结构
		[StructLayout(LayoutKind.Sequential)]
		public struct EXE_LIC
		{
			public ushort m_Priv_Exe;   //运行的权限: 0为最小匿名权限，1为最小用户权限，2为最小开发商权限
		}

		/****************************文件属性结构********************************/
		//数据文件属性数据结构
		[StructLayout(LayoutKind.Sequential)]
		public struct DATA_FILE_ATTR
		{
			public uint m_Size;      //数据文件长度，该值最大为4096
			public DATA_LIC m_Lic;       //授权
		}

		//ECCSM2/RSA私钥文件属性数据结构
		[StructLayout(LayoutKind.Sequential)]
		public struct PRIKEY_FILE_ATTR
		{
			public ushort m_Type;       //数据类型:ECCSM2私钥 或 RSA私钥
			public ushort m_Size;       //数据长度:RSA该值为1024或2048, ECC该值为192或256, SM2该值为0x8100
			public PRIKEY_LIC m_Lic;        //授权
		}

		//对称加密算法(SM4/TDES)密钥文件属性数据结构
		[StructLayout(LayoutKind.Sequential)]
		public struct KEY_FILE_ATTR
		{
			public uint m_Size;       //密钥数据长度=16
			public KEY_LIC m_Lic;        //授权
		}

		//可执行文件属性数据结构
		[StructLayout(LayoutKind.Sequential)]
		public struct EXE_FILE_ATTR
		{
			public EXE_LIC m_Lic;        //授权	
			public ushort m_Len;        //文件长度
		}
		/*************************文件列表结构***********************************/
		//获取私钥文件列表时返回的数据结构
		[StructLayout(LayoutKind.Sequential)]
		public struct PRIKEY_FILE_LIST
		{
			public ushort m_FILEID;  //文件ID
			public ushort m_Reserve; //保留,用于4字节对齐
			public PRIKEY_FILE_ATTR m_attr;    //文件属性
		}

		//获取SM4及TDES密钥文件列表时返回的数据结构
		[StructLayout(LayoutKind.Sequential)]
		public struct KEY_FILE_LIST
		{
			public ushort m_FILEID;  //文件ID
			public ushort m_Reserve; //保留,用于4字节对齐
			public KEY_FILE_ATTR m_attr;    //文件属性
		}

		//获取数据文件列表时返回的数据结构
		[StructLayout(LayoutKind.Sequential)]
		public struct DATA_FILE_LIST
		{
			public ushort m_FILEID;  //文件ID
			public ushort m_Reserve; //保留,用于4字节对齐
			public DATA_FILE_ATTR m_attr;    //文件属性
		}

		//获取可执行文件列表时返回的数据结构
		[StructLayout(LayoutKind.Sequential)]
		public struct EXE_FILE_LIST
		{
			public ushort m_FILEID;    //文件ID
			public EXE_FILE_ATTR m_attr;
			public ushort m_Reserve;  //保留,用于4字节对齐
		}

		//下载和列可执行文件时填充的数据结构
		[StructLayout(LayoutKind.Sequential)]
		public struct EXE_FILE_INFO
		{
			public ushort m_dwSize;           //可执行文件大小
			public ushort m_wFileID;          //可执行文件ID
			public byte m_Priv;             //调用权限: 0为最小匿名权限，1为最小用户权限，2为最小开发商权限

			public byte[] m_pData;            //可执行文件数据
		}


		//需要发给空锁的初始化数据
		[StructLayout(LayoutKind.Sequential)]
		public struct SON_DATA
		{
			public int m_SeedLen;                 //种子码长度
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string m_SeedForPID;        //产生产品ID和开发商密码的种子码 (最长250个字节)
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 18)]
			public string m_UserPIN;         //用户密码(16个字符的0终止字符串)
			public sbyte m_UserTryCount;            //用户密码允许的最大错误重试次数
			public sbyte m_AdminTryCount;           //开发商密码允许的最大错误重试次数
													//RSA_PRIVATE_KEY m_UpdatePriKey;   //远程升级私钥
			public int m_UserID_Start;            //起始用户ID
		}

		//母锁数据
		[StructLayout(LayoutKind.Sequential)]
		public struct MOTHER_DATA
		{
			public SON_DATA m_Son;                  //子锁初始化数据
			public int m_Count;                //可产生子锁初始化数据的次数 (-1表示不限制次数, 递减到0时会受限)
		}

		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_ReadFile(Int64 hDongle, short wFileID, short wOffset, byte[] buffer, int nDataLen);
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_ListFile(Int64 hDongle, uint nFileType, DATA_FILE_LIST[] pFileList, ref int pDataLen);
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_DeleteFile(Int64 hDongle, uint nFileType, short wFileID);
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_WriteShareMemory(Int64 hDongle, byte[] pData, int nDataLen);
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_ReadShareMemory(Int64 hDongle, byte[] pData);
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_WriteData(Int64 hDongle, int nOffset, byte[] pData, int nDataLen);
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_ReadData(Int64 hDongle, int nOffset, byte[] pData, int nDataLen);
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_LEDControl(Int64 hDongle, uint nFlag);
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_SwitchProtocol(Int64 hDongle, uint nFlag);
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_GetUTCTime(Int64 hDongle, ref uint pdwUTCTime);
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_SetDeadline(Int64 hDongle, uint dwTime);


		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_ChangePIN(Int64 hDongle, uint nFlags, byte[] pOldPIN, byte[] pNewPIN, int nTryCount);

		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_SetUserID(Int64 hDongle, uint dwUserID);
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_ResetUserPIN(Int64 hDongle, byte[] pAdminPIN);
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_RsaPri(Int64 hDongle, ushort wPriFileID, uint nFlag, byte[] pInData, uint nInDataLen, byte[] pOutData, ref uint pOutDataLen);
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_RsaPub(Int64 hDongle, uint nFlag, ref RSA_PUBLIC_KEY pPubKey, byte[] pInData, uint nInDataLen, byte[] pOutData, ref uint pOutDataLen);
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_TDES(Int64 hDongle, ushort wKeyFileID, uint nFlag, byte[] pInData, byte[] pOutData, uint nDataLen);
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_DeleteFile(Int64 hDongle, uint nFileType, ushort wFileID);
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_HASH(Int64 hDongle, uint nFlag, byte[] pInData, uint nDataLen, byte[] pHash);
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_LimitSeedCount(Int64 hDongle, int nCount);
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_Seed(Int64 hDongle, byte[] pSeed, uint nSeedLen, byte[] pOutData);
	}
}