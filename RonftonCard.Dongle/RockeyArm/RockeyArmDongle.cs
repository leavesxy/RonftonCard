using System;
using System.Collections.Generic;
using System.Text;

namespace RonftonCard.Dongle.RockeyArm
{
	using Bluemoon;
	using log4net;
	using Core.Dongle;
	using DONGLE_HANDLER = Int64;

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
			bool ret =  (seq >= 0) && (seq < this.dongleInfo.Length);

			if (!ret)
				this.lastErrorCode = 0xF0000002;

			return ret;
		}

		private uint ToUint32(String str, int fromBase = 16)
		{
			uint val = 0;

			try
			{
				val = Convert.ToUInt32(str, fromBase);
			}
			catch (Exception)
			{
			}
			return val;
		}

		public void Dispose()
		{
			Close();
		}
	}
}
