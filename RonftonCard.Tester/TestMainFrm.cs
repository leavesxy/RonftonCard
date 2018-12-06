using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Newtonsoft.Json;
using RonftonCard.Core.Dongle;
using RonftonCard.Dongle.RockeyArm;

namespace RonftonCard.Tester
{
	public partial class TestMainFrm : Form
	{
		private RockeyArmDongle dongle;
		private int ledFlag;

		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_GetUTCTime(Int64 hDongle, ref uint pdwUTCTime);
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_LEDControl(Int64 hDongle, uint nFlag);

		public TestMainFrm()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			this.dongle = new RockeyArmDongle();
			//this.dongle.Enumerate();
		}
		
		private void BtnExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void BtnGetUTCTime_Click(object sender, EventArgs e)
		{
			dongle.Open(0);

			uint utcTime = 0;

			//if (dongle.Dongle_GetUTCTime(hDongle, ref utcTime) == 0)
			//{
			//	this.TxtTrace.Trace(String.Format("Read time ok ! utcTime = {0}", utcTime), true);

			//	// local time
			//	DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
			//	DateTime dt = startTime.AddSeconds(utcTime);

			//	this.TxtTrace.Trace("Date time = {0}", dt.ToString("yyyy-MM-dd HH:mm:ss"));
			//}
		}

		private void RbLedOff_CheckedChanged(object sender, EventArgs e)
		{
			this.ledFlag = 0;
		}

		private void RbLedOn_CheckedChanged(object sender, EventArgs e)
		{
			this.ledFlag = 1;
		}

		private void RbLedBlink_CheckedChanged(object sender, EventArgs e)
		{
			this.ledFlag = 2;
		}

		private void BtnLedControl_Click(object sender, EventArgs e)
		{
			//dongle.Open(0);
			//Int64 hDongle = dongle.GetDongleHanlder(0);

			//Dongle_LEDControl(hDongle, (uint)this.ledFlag);
		}

		private void BtnDongleInfo2Json_Click(object sender, EventArgs e)
		{
			DongleKeyInfo keyInfo = DongleKeyInfo.CreateTestDongleKeyInfo(DongleType.USER_ROOT, "01234567");

			String jsonData = JsonConvert.SerializeObject(keyInfo);
			this.TxtTrace.Trace(jsonData, true);
		}
	}
}
