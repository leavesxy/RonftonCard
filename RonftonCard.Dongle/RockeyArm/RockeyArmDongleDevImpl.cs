using System;
using System.Collections.Generic;
using RonftonCard.Core.Dongle;

namespace RonftonCard.Dongle.RockeyArm
{
	using Bluemoon;
	using DONGLE_HANDLER = Int64;

	public partial class RockeyArmDongle
	{
		#region "--- Device interface ---"
		/// <summary>
		/// open dongle by sequence number
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
			if (this.hDongle > 0)
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
			if (this.hDongle > 0)
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

		/// <summary>
		/// get timer of dongle
		/// </summary>
		public DateTime GetDevTimer()
		{
			UInt32 utcTime = 0;
			this.lastErrorCode = Dongle_GetUTCTime(this.hDongle, ref utcTime);

			// local time zone
			DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
			return startTime.AddSeconds(utcTime);
		}

		public bool Enumerate()
		{
			Close();

			int count = LookupCount();
			if (count <= 0)
				return false;

			this.dongleInfo = GetDongleInfo(count);

			// read User information for each dongle
			GetDongleUserInfo();

			this.selectedIndex = -1;
			this.hDongle = -1;

			return IsSucc;
		}

		#endregion


	/// <summary>
		/// check current key is empty or not
		/// </summary>
		private bool IsEmptyKey(DongleInfo dongleInfo)
		{
			return dongleInfo.AppId.Equals("FFFFFFFF") || dongleInfo.UserId.Equals("FFFFFFFF");
		}


		#region "--- util for Enumerate ---"
		private int LookupCount()
		{
			long count = 0;
			this.lastErrorCode = Dongle_Enum(IntPtr.Zero, out count);
			logger.Debug(String.Format("found {0} Dongles !", count));

			return (int)count;
		}

		private DongleInfo[] GetDongleInfo(int count)
		{
			List<DongleInfo> keyInfo = new List<DongleInfo>();
			IntPtr ptr = IntPtr.Zero;
			
			try
			{
				int size = IntPtrUtil.SizeOf(typeof(DONGLE_INFO));
				ptr = IntPtrUtil.Create(size * (int)count);
				long __count;
				this.lastErrorCode = Dongle_Enum(ptr, out __count);

				//__count is indeed count of dongle
				for (int i = 0; i < __count; i++)
				{
					IntPtr __ptr = IntPtrUtil.Create(ptr, i * size);
					DONGLE_INFO devInfo = IntPtrUtil.ToStru<DONGLE_INFO>(__ptr);
					keyInfo.Add(ParseDongleInfo((short)i, devInfo));
				}
			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
				return null;
			}
			finally
			{
				IntPtrUtil.Free(ref ptr);
			}
			return keyInfo.ToArray();
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

		private void GetDongleUserInfo()
		{
			if (this.dongleInfo.IsNullOrEmpty())
				return;

			int size = IntPtrUtil.SizeOf(typeof(DongleUserInfoStru));
			byte[] buffer = new byte[size];

			for (int seq = 0; seq < this.dongleInfo.Length; seq++)
			{
				if (IsEmptyKey(this.dongleInfo[seq]))
					continue;

				DONGLE_HANDLER __hDongle = -1;

				if (Dongle_Open(ref __hDongle, seq) == SUCC)
				{
					if (Dongle_ReadFile(__hDongle, DongleConst.USER_INFO_DESCRIPTOR, 0, buffer, buffer.Length) == SUCC)
					{
						this.dongleInfo[seq].UserInfo = ParseDongleKeyInfo(buffer);
					}
					Dongle_Close(__hDongle);
				}
			}
		}

		private DongleUserInfo ParseDongleKeyInfo(byte[] buffer)
		{
			DongleUserInfoStru keyInfoStru = ByteUtil.CopyToStru<DongleUserInfoStru>(buffer);
			DongleUserInfo keyInfo = new DongleUserInfo();

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
	}
}
