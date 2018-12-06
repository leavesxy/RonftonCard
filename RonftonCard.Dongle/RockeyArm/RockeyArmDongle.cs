using System;
using System.Collections.Generic;
using System.Text;

namespace RonftonCard.Dongle.RockeyArm
{
	using Bluemoon;
	using Bluemoon.Config;
	using log4net;
	using Core.Dongle;
	using DONGLE_HANDLER = Int64;
	using Core.DTO;
	using System.Runtime.InteropServices;

	public partial class RockeyArmDongle : IDongle
	{
		private static ILog logger = LogManager.GetLogger("RonftonCardLog");
		private const String defaultErrMsgFileName = "RockeyArmErrorMessage.properties";
		private readonly String defaultAdminPin = "FFFFFFFFFFFFFFFF";
		//private readonly String defaultUserPin = "12345678";
		private const uint SUCC = 0x00000000;

		/// <summary>
		/// dongles information
		/// </summary>
		private DongleInfo[] dongleInfo;

		/// <summary>
		/// current active dongle
		/// </summary>
		private DONGLE_HANDLER hDongle;
		private int selectedIndex;

		private Properties errorMsgProp;
		private readonly byte[] seed;
		private Encoding encoder;
		private uint lastErrorCode;

		#region "--- Contructor ---"
		public RockeyArmDongle()
			: this(Charset.UTF8.GetAliasName(), DongleConst.DEFAULT_SEED_KEY, defaultErrMsgFileName)
		{
		}

		public RockeyArmDongle(String encodingName, String seed, String errMsgFileName)
		{
			this.encoder = Encoding.GetEncoding(encodingName);

			if (String.IsNullOrEmpty(seed))
				seed = DongleConst.DEFAULT_SEED_KEY;

			this.seed = this.Encoder.GetBytes(seed);

			if (!String.IsNullOrEmpty(errMsgFileName))
				this.errorMsgProp = new Properties(errMsgFileName);

			this.lastErrorCode = 0;
			this.selectedIndex = -1;
			this.hDongle = -1;
		}
		#endregion

		#region "--- properties ---"

		public DongleInfo[] Dongles
		{
			get { return this.dongleInfo; }
		}

		public String LastErrorMessage
		{
			get
			{
				return this.errorMsgProp.Get(String.Format("0x{0:X8}", this.lastErrorCode));
			}
		}

		public Encoding Encoder
		{
			get { return this.encoder; }
		}

		public bool IsSucc
		{
			get { return this.lastErrorCode == SUCC; }
		}

		public int SelectedIndex
		{
			get { return this.selectedIndex; }
		}

		#endregion

		protected bool IsActive()
		{
			bool ret = this.selectedIndex != -1 && this.hDongle > 0 ;
			if (!ret)
				this.lastErrorCode = 0xF0000001;
			return ret;
		}

		protected bool IsValidSeq(int seq)
		{
			bool ret =  seq >= 0 && seq < this.dongleInfo.Length;
			if (!ret)
				this.lastErrorCode = 0xF0000002;
			return ret;
		}

		private uint ToUint32(String str, int fromBase = 16)
		{
			uint v = 0;

			try
			{
				v = Convert.ToUInt32(str, fromBase);
			}
			catch (Exception)
			{
			}
			return v;
		}



		//public static RT CopyToStru<RT>(byte[] buffer)
		//{
		//	if (buffer.IsNullOrEmpty())
		//		throw new Exception("Can't copy empty byte array to structure !");

		//	int size = IntPtrUtil.SizeOf(typeof(RT));
		//	IntPtr ptr = IntPtr.Zero;
		//	RT stru;

		//	try
		//	{
		//		ptr = IntPtrUtil.Create(size);
		//		Marshal.Copy(buffer, 0, ptr, size);
		//		stru = (RT)Marshal.PtrToStructure(ptr, typeof(RT));
		//	}
		//	finally
		//	{
		//		if (ptr != IntPtr.Zero)
		//			IntPtrUtil.Free(ref ptr);
		//	}

		//	return stru;
		//}


		#region "--- device operation ---"

		/// <summary>
		/// open specified dongle by sequence number
		/// and close dongle which has been opened
		/// </summary>
		public bool Open(int seq)
		{
			// invliad sequence number
			if (!IsValidSeq(seq))
				return false;

			// opened already
			if (seq == this.selectedIndex && this.hDongle > 0)
				return true;

			// turn off old
			if ( this.hDongle > 0 )
			{
				Dongle_LEDControl(this.hDongle, (int)RockeyArmLedFlag.OFF);
				Close();
			}

			// open new dongle
			this.lastErrorCode = Dongle_Open(ref this.hDongle, seq);

			// blink
			if (this.hDongle > 0)
			{
				Dongle_LEDControl(this.hDongle, (int)RockeyArmLedFlag.BLINK);
				this.selectedIndex = seq;
			}

			return IsSucc;
		}

		/// <summary>
		/// close current active dongle
		/// </summary>
		public void Close()
		{
			if (this.hDongle > 0 )
			{
				try
				{
					Dongle_LEDControl(this.hDongle, (int)RockeyArmLedFlag.OFF);
					this.lastErrorCode = Dongle_Close(this.hDongle);
					this.hDongle = -1;
				}
				catch (Exception)
				{ }
			}
			this.selectedIndex = -1;
		}

		/// <summary>
		/// reset dongle status to anonymous
		/// </summary>
		public bool Reset()
		{
			if (!IsActive())
				return false;

			this.lastErrorCode = Dongle_ResetState(this.hDongle);

			return IsSucc;
		}

		/// <summary>
		/// restore current key, should use admin pin
        /// after restore,should re-enumerate dongle again
		/// </summary>
		public bool Restore(byte[] adminPin)
		{
			if (!IsActive())
				return false;

			if (!Authen(this.hDongle, DongleAuthenMode.ADMIN, adminPin))
				return false;

			this.lastErrorCode = Dongle_RFS(this.hDongle);

			Close();
			return IsSucc;
		}
		
		private bool Authen(DONGLE_HANDLER hDongle, DongleAuthenMode authenMode, byte[] pin)
		{
			uint flag = (authenMode == DongleAuthenMode.ADMIN) ? (uint)1 : (uint)0;

			int remainCount;
			this.lastErrorCode = Dongle_VerifyPIN(hDongle, flag, pin, out remainCount);

			return IsSucc;
		}

		#endregion

		#region "--- enumerate dongle ---"
		/// <summary>
		/// enumerate dongle device
		/// </summary>
		public bool Enumerate()
		{
			Close();

			long count = 0;
			this.lastErrorCode = Dongle_Enum(IntPtr.Zero, out count);
			if ( !this.IsSucc || count <= 0)
				return false;

			logger.Debug(String.Format("found {0} Dongles !", count));

			List<DongleInfo> keyInfo = new List<DongleInfo>();
			IntPtr pDongleInfo = IntPtr.Zero;
			try
			{
				int size = IntPtrUtil.SizeOf(typeof(DONGLE_INFO));
				pDongleInfo = IntPtrUtil.Create(size * (int)count);
				this.lastErrorCode = Dongle_Enum(pDongleInfo, out count);

				for (int i = 0; i < count; i++)
				{
					IntPtr ptr = IntPtrUtil.Create(pDongleInfo, i * size);
					DONGLE_INFO devInfo = IntPtrUtil.ToStru<DONGLE_INFO>(ptr);
					keyInfo.Add(ParseDongleInfo((short)i, devInfo));
				}
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
				return false;
			}
			finally
			{
				IntPtrUtil.Free(ref pDongleInfo);
			}

			this.dongleInfo = keyInfo.ToArray();

			// read key information for each dongle
			GetDongleKeyInfo();

			this.selectedIndex = -1;
			this.hDongle = -1;

			return IsSucc;
		}


		private DongleInfo ParseDongleInfo(short seq, DONGLE_INFO devInfo)
		{
			DongleInfo dongleInfo = new DongleInfo()
			{
				Seq = seq,
				Version = String.Format("v{0}.{1:d2}",
								devInfo.m_Ver >> 8 & 0xff, devInfo.m_Ver & 0xff),
								//BitConverter.ToString(devInfo.m_BirthDay)),
				UserId = devInfo.m_UserID.ToString("X08"),
				AppId = devInfo.m_PID.ToString("X08"),
				KeyId = BitConverter.ToString(devInfo.m_HID),
				Description = GetDongleModel((byte)(devInfo.m_Type & 0x0ff))
			};

			logger.Debug(dongleInfo.GetInfo());
			return dongleInfo;
		}

		/// <summary>
		/// check current key is empty or not
		/// </summary>
		/// <returns></returns>
		private bool IsEmptyKey(DongleInfo dongleInfo)
		{
			return dongleInfo.AppId.Equals("FFFFFFFF") || dongleInfo.UserId.Equals("FFFFFFFF");
		}

		private void GetDongleKeyInfo()
		{
			if (this.dongleInfo.IsNullOrEmpty())
				return;

			int size = IntPtrUtil.SizeOf(typeof(DongleKeyInfoStru));
			byte[] buffer = new byte[size];

			for (int seq = 0; seq < this.dongleInfo.Length; seq++)
			{
				if (IsEmptyKey(this.dongleInfo[seq]) )
					continue;

				DONGLE_HANDLER __hDongle=-1;

				if( Dongle_Open(ref __hDongle, seq) == SUCC)
				{
					if (Dongle_ReadFile(__hDongle, DongleConst.KEY_INFO_DESCRIPTOR, 0, buffer, buffer.Length) == SUCC)
					{
						this.dongleInfo[seq].KeyInfo = ParseDongleKeyInfo(buffer);
					}
					Dongle_Close(__hDongle);
				}
			}
		}

		private DongleKeyInfo ParseDongleKeyInfo(byte[] buffer)
		{
			DongleKeyInfoStru keyInfoStru = ByteUtil.CopyToStru<DongleKeyInfoStru>(buffer);
			DongleKeyInfo keyInfo = new DongleKeyInfo();

			keyInfo.DongleType = (DongleType)keyInfoStru.DongleType;
			keyInfo.UserId = encoder.GetString(keyInfoStru.UserId);
			keyInfo.UserName = encoder.GetString(keyInfoStru.UserName).TrimEnd('\0');
			keyInfo.Operator = encoder.GetString(keyInfoStru.Operator);
			keyInfo.CreateDate = encoder.GetString(keyInfoStru.CreateDate);

			return keyInfo;
		}

		private String GetDongleModel(byte model)
		{
			RockeyArmModel dongleModel;

			switch (model)
			{
				case (byte)RockeyArmModel.STANDARD:
					dongleModel = RockeyArmModel.STANDARD;
					break;

				case (byte)RockeyArmModel.TIMER:
					dongleModel = RockeyArmModel.TIMER;
					break;

				case (byte)RockeyArmModel.UDISK:
					dongleModel = RockeyArmModel.UDISK;
					break;

				default:
					dongleModel = RockeyArmModel.UNKNOWN;
					break;
			}

			return dongleModel.GetAliasName();
		}

		#endregion

		public void Dispose()
		{
			Close();
		}
	}
}
