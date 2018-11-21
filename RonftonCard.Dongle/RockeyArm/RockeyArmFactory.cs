using System;
using System.Collections.Generic;
using RonftonCard.Core.Dongle;
using Bluemoon;
using log4net;

namespace RonftonCard.Dongle.RockeyArm
{
	using System.Linq;
	using static RockeyArmDongle;
	using DONGLE_HANDLER = Int64;

	public class RockeyArmFactory : IDongleFactory
	{
		protected static ILog logger = LogManager.GetLogger("RonftonCardLog");

		private DongleInfo[] dongleInfo;
		private DONGLE_HANDLER[] hDongle;
		private RockeyArmDongle dongle;

		private const uint SUCC = 0x00000000;

		public RockeyArmFactory()
		{
		}


		public DongleInfo[] Dongles
		{
			get	{ return this.dongleInfo; }
		}


		/// <summary>
		/// enumerate dongle device
		/// </summary>
		public void Enumerate()
		{
			// close all device if have opened!!!
			CloseAll();

			long count = 0;
			if (RockeyArmDongleInterface.Dongle_Enum(IntPtr.Zero, out count) != SUCC || count <= 0)
				return ;

			logger.Debug(String.Format("found {0} Dogs !", count));

			List<DongleInfo> keyInfo = new List<DongleInfo>();
			IntPtr pDongleInfo = IntPtr.Zero;

			try
			{
				int size = IntPtrUtil.SizeOf(typeof(DONGLE_INFO));
				pDongleInfo = IntPtrUtil.Create(size * (int)count);
				RockeyArmDongleInterface.Dongle_Enum(pDongleInfo, out count);

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
			this.hDongle = new DONGLE_HANDLER[this.dongleInfo.Length];
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


		private void CloseAll()
		{
			if (this.hDongle.IsNullOrEmpty())
				return;
			
			for(int i=0; i<this.hDongle.Length;i++)
			{
				dongle.Close(this.hDongle[i]);
			}
		}
		

		public IDongleService GetDongleService(string keyId)
		{
			throw new NotImplementedException();
		}

		public IDongleService GetDongleService(int seq)
		{
			throw new NotImplementedException();
		}
	}
}
