using System;
using System.Collections.Generic;
using System.Text;
using Bluemoon;
using RonftonCard.Core.Dongle;

namespace RonftonCard.Dongle.RockeyArm
{
	using Bluemoon.Config;
	using log4net;
	using DONGLE_HANDLER = Int64;

	public partial class RockeyArmDongle : IDongle
	{
		private static ILog logger = LogManager.GetLogger("RonftonCardLog");
		private const String defaultErrMsgFileName = "RockeyArmErrorMessage.properties";
		private readonly String defaultAdminPin = "FFFFFFFFFFFFFFFF";
		private readonly String defaultUserPin = "12345678";
		private const uint SUCC = 0x00000000;

		private DongleInfo[] dongleInfo;
		private DONGLE_HANDLER[] hDongles;
		private Properties errorMsgProp;
		private readonly byte[] seed;
		private Encoding encoder;
		private uint lastErrorCode;
		private int selectedIndex;

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

			//Enumerate();
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


		public DONGLE_HANDLER[] GetDongleHandler()
		{
			return this.hDongles;
		}

		protected bool IsValidSeq(int seq)
		{
			return !(this.hDongles.IsNullOrEmpty() || seq > this.hDongles.Length - 1 || seq < 0 );
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


		#region "--- device operation ---"

		/// <summary>
		/// open specified key by seq , and first is default
		/// </summary>
		public bool Open(int seq)
		{
			if (!IsValidSeq(seq))
				return false;

			if (seq != this.selectedIndex)
			{
				// turn off old
				if (this.selectedIndex != -1 && this.hDongles[this.selectedIndex] > 0)
				{
					Dongle_LEDControl(this.hDongles[selectedIndex], (int)RockeyArmLedFlag.OFF);
				}
				this.selectedIndex = seq;
			}

			// avoid to re-open again
			// and blink selected
			if (this.hDongles[this.selectedIndex] > 0)
			{
				Dongle_LEDControl(this.hDongles[this.selectedIndex], (int)RockeyArmLedFlag.BLINK);
				return true;
			}

			this.lastErrorCode = Dongle_Open(ref this.hDongles[this.selectedIndex], seq);

			if(this.hDongles[this.selectedIndex] > 0)
				Dongle_LEDControl(this.hDongles[this.selectedIndex], (int)RockeyArmLedFlag.BLINK);

			return IsSucc;
		}

		public void Close(int seq)
		{
			if (!IsValidSeq(seq))
				return;

			if (this.hDongles[seq] > 0 )
			{
				Dongle_LEDControl(this.hDongles[seq], (int)RockeyArmLedFlag.OFF);
				Dongle_Close(this.hDongles[seq]);
				this.hDongles[seq] = -1;
			}
		}

		public void CloseAll()
		{
			if (this.hDongles.IsNullOrEmpty())
				return;

			for (int i = 0; i < this.hDongles.Length; i++)
			{
				Close(i);
			}
			// set dongleInfo null
			this.dongleInfo = null;
		}

		/// <summary>
		/// reset dongle status to anonymous
		/// </summary>
		public bool Reset()
		{
			if (!IsValidSeq(this.selectedIndex))
				return false;

			if (this.hDongles[this.selectedIndex] > 0)
			{
				this.lastErrorCode = Dongle_ResetState(this.hDongles[this.selectedIndex]);
			}
			return IsSucc;
		}

		/// <summary>
		/// restore current key, should use admin pin
		/// </summary>
		public bool Restore(byte[] adminPin)
		{
			if (!Open(this.selectedIndex))
				return false;

			if (!Authen(this.hDongles[this.selectedIndex], DongleAuthenMode.ADMIN, adminPin))
				return false;

			this.lastErrorCode = Dongle_RFS(this.hDongles[this.selectedIndex]);

			Close(this.selectedIndex);
			return IsSucc;
		}
		
		private bool Authen(DONGLE_HANDLER hDongle, DongleAuthenMode authenMode, byte[] pin)
		{
			uint flag = (authenMode == DongleAuthenMode.ADMIN) ? (uint)1 : (uint)0;

			int pRemainCount;
			this.lastErrorCode = Dongle_VerifyPIN(hDongle, flag, pin, out pRemainCount);

			return IsSucc;
		}

		#endregion

		#region "--- enumerate dongle ---"
		/// <summary>
		/// enumerate dongle device
		/// </summary>
		public bool Enumerate()
		{
			// close all device if have opened!!!
			CloseAll();

			//!!! set selectIndex=-1 !!!
			this.selectedIndex = -1;
			long count = 0;

			if (Dongle_Enum(IntPtr.Zero, out count) != SUCC || count <= 0)
				return false;

			logger.Debug(String.Format("found {0} Dogs !", count));

			List<DongleInfo> keyInfo = new List<DongleInfo>();
			IntPtr pDongleInfo = IntPtr.Zero;

			try
			{
				int size = IntPtrUtil.SizeOf(typeof(DONGLE_INFO));
				pDongleInfo = IntPtrUtil.Create(size * (int)count);
				Dongle_Enum(pDongleInfo, out count);

				for (int i = 0; i < count; i++)
				{
					IntPtr ptr = IntPtrUtil.Create(pDongleInfo, i * size);
					DONGLE_INFO devInfo = IntPtrUtil.ToStructure<DONGLE_INFO>(ptr);
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
			this.hDongles = new DONGLE_HANDLER[this.dongleInfo.Length];

			//open first as default
			this.selectedIndex = 0;
			return Open(this.selectedIndex);
		}

		private DongleInfo ParseDongleInfo(short seq, DONGLE_INFO devInfo)
		{
			DongleInfo dongleInfo = new DongleInfo()
			{
				Seq = seq,
				Version = String.Format("v{0}.{1:d2}-({2})",
								devInfo.m_Ver >> 8 & 0xff, devInfo.m_Ver & 0xff,
								BitConverter.ToString(devInfo.m_BirthDay)),
				UserId = devInfo.m_UserID.ToString("X08"),
				AppId = devInfo.m_PID.ToString("X08"),
				KeyId = BitConverter.ToString(devInfo.m_HID),
				Description = GetDongleModel((byte)(devInfo.m_Type & 0x0ff))
			};
			logger.Debug(dongleInfo.ToString());
			return dongleInfo;
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
			CloseAll();
		}
	}
}
