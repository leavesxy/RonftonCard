using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Bluemoon;
using RonftonCard.Core.Temp.Dongle;

namespace RonftonCard.Dongle.Temp.RockeyArm
{
	using HDONGLE = Int64;

	public partial class RockeyArmDongle  : AbstractDongle
	{
		private const String defaultErrMsgFileName = "RockeyArmErrorMessage.properties";

		#region "--- Constructor ---"

		public RockeyArmDongle()
			: this( Charset.UTF8.GetAliasName(),DongleConst.DEFAULT_SEED_KEY,defaultErrMsgFileName)
		{
		}

		public RockeyArmDongle(String encoding, String seed, String errMsgFileName )
			: this(encoding, seed, errMsgFileName,DongleConst.DEFAULT_ADMIN_PIN_DONGLE,DongleConst.DEFAULT_USER_PIN_DONGLE)
		{
		}

		public RockeyArmDongle(String encoding, String seed, String errMsgFileName, String defaultAdminPin, String defaultUserPin)
			: base(encoding, seed, errMsgFileName, defaultAdminPin, defaultUserPin)
		{
			//Enumerate();
		}

		#endregion

		#region "--- util ---"

		/// <summary>
		/// check current sequence is valid or not
		/// </summary>
		protected bool IsValidSeq(int seq)
		{
			return !(this.dongleInfo.IsNullOrEmpty() || seq > this.dongleInfo.Length - 1);
		}

		/// <summary>
		/// convert String to int,default base 16
		/// </summary>
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
		#endregion

		#region "--- device interface implements ---"

		/// <summary>
		/// convert lastErrorCode to ErrorMsg key
		/// </summary>
		protected override String GetErrorMsgKey()
		{
			return String.Format("0x{0:X8}", this.LastErrorCode);
		}

		/// <summary>
		/// open specified key by seq , and first is default
		/// </summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override bool Open(int seq = 0)
		{
			if (!IsValidSeq(seq))
				return false;

			//avoid to re-open again
			if (this.dongleInfo[seq].hDongle > 0)
				return true;

			this.LastErrorCode = Dongle_Open(ref this.dongleInfo[seq].hDongle, seq);
			return IsSucc;
		}

		/// <summary>
		/// Close all dongle
		/// </summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override void Close()
		{
			if (this.Dongles.IsNullOrEmpty())
				return;

			for (int i = 0; i < this.dongleInfo.Length; i++)
			{
				if (this.dongleInfo[i].hDongle != -1)
				{
					Dongle_Close(this.dongleInfo[i].hDongle);
					this.dongleInfo[i].hDongle = -1;
				}
			}
			// set dongleInfo null
			this.dongleInfo = null;
		}

		/// <summary>
		/// close specified dongle
		/// </summary>
		[MethodImpl(MethodImplOptions.Synchronized)]
		public override void Close(int seq)
		{
			if (!IsValidSeq(seq))
				return;

			if (this.dongleInfo[seq].hDongle != -1)
			{
				Dongle_Close(this.dongleInfo[seq].hDongle);
				this.dongleInfo[seq].hDongle = -1;
			}
		}


		/// <summary>
		/// enumerate dongle device
		/// </summary>
		public override bool Enumerate()
		{
			// close all device if have opened!!!
			if (!this.dongleInfo.IsNullOrEmpty())
			{
				Close();
			}

			long count = 0;
			this.LastErrorCode = Dongle_Enum(IntPtr.Zero, out count);

			//no key found
			if (!IsSucc || count <= 0)
				return false;

			logger.Debug(String.Format("found {0} Dogs !", count));

			List<DongleInfo> keyInfo = new List<DongleInfo>();
			IntPtr pDongleInfo = IntPtr.Zero;

			try
			{
				int size = IntPtrUtil.SizeOf(typeof(DONGLE_INFO));
				pDongleInfo = IntPtrUtil.Create(size * (int)count);
				this.LastErrorCode = Dongle_Enum(pDongleInfo, out count);

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
			}
			finally
			{
				IntPtrUtil.Free(ref pDongleInfo);
			}

			this.dongleInfo = keyInfo.ToArray();

			return IsSucc;
		}

		private DongleInfo ParseDongleInfo(short seq, DONGLE_INFO devInfo)
		{
			DongleInfo dongleInfo = new DongleInfo()
			{
				Seq = seq,
				Version = String.Format("v{0}.{1:d2}-({2})",
								devInfo.m_Ver >> 8 & 0xff,devInfo.m_Ver & 0xff,
								BitConverter.ToString(devInfo.m_BirthDay)),
				UserId = devInfo.m_UserID.ToString("X08"),
				AppId = devInfo.m_PID.ToString("X08"),
				KeyId = BitConverter.ToString(devInfo.m_HID),
				Description = GetDongleModel( (byte)(devInfo.m_Type & 0x0ff ))
			};
			AbstractDongle.logger.Debug(dongleInfo.ToString());
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

		/// <summary>
		/// restore current key, should use admin pin
		/// </summary>
		public override bool Restore(byte[] adminPin, int seq = 0)
		{
			if (!Open(seq))
				return false;

			if (!Authen(this.dongleInfo[seq].hDongle, AuthenMode.ADMIN, adminPin))
				return false;

			this.LastErrorCode = Dongle_RFS(this.dongleInfo[seq].hDongle);

			Close(seq);
			return IsSucc;
		}
		
		/// <summary>
		/// reset dongle status to anonymous
		/// </summary>
		/// <param name="seq"></param>
		/// <returns></returns>
		public override bool Reset(int seq = 0)
		{
			if (!IsValidSeq(seq))
				return false;

			this.LastErrorCode = Dongle_ResetState(this.dongleInfo[seq].hDongle);
			return IsSucc;
		}

		private bool Authen(HDONGLE hDongle, AuthenMode authenMode, byte[] pin)
		{
			uint flag = (authenMode == AuthenMode.ADMIN) ? (uint)1 : (uint)0;
			int pRemainCount;
			this.LastErrorCode = Dongle_VerifyPIN(hDongle, flag, pin, out pRemainCount);

			return IsSucc;
		}


		#endregion
	}
}