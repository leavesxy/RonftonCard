using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using RonftonCard.Dongle.RockeyArm;

namespace RonftonCard.Tester
{
	public partial class TestMainFrm : Form
	{
		private RockeyArmDongle dongle;

		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_GetUTCTime(Int64 hDongle, ref uint pdwUTCTime);

		public TestMainFrm()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			this.dongle = new RockeyArmDongle();
		}


		private void BtnExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void BtnGetUTCTime_Click(object sender, EventArgs e)
		{
			dongle.Enumerate();
			dongle.Open(0);

			Int64 hDongle = dongle.GetDongleHanlder(0);

			uint utcTime = 0;

			if (Dongle_GetUTCTime(hDongle, ref utcTime) == 0)
			{
				this.TxtTrace.Trace("Read time ok ! utcTime = {0}", utcTime);
			}
		}
	}
}
