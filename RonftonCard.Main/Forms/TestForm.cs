﻿using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Newtonsoft.Json;
using RonftonCard.Core.Dongle;
using RonftonCard.Dongle.RockeyArm;

namespace RonftonCard.Main.Forms
{
	using Bluemoon.WinForm.Toolbox;

	public partial class TestForm : Form
	{
		private RockeyArmDongle dongle;
		private int ledFlag;

		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_GetUTCTime(Int64 hDongle, ref uint pdwUTCTime);
		[DllImport(@"Plugin\Dongle_d.dll")]
		static extern uint Dongle_LEDControl(Int64 hDongle, uint nFlag);

		public TestForm()
		{
			InitializeComponent();
		}

		private void TestForm_Load(object sender, EventArgs e)
		{

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

		private void BtnConvert2Json_Click(object sender, EventArgs e)
		{
			DongleUserInfo keyInfo = DongleUserInfo.CreateTestDongleKeyInfo(DongleType.USER_ROOT, "01234567");

			String jsonData = JsonConvert.SerializeObject(keyInfo);
			this.Trace.Trace(jsonData, true);
		}

		private void BtnGetUTCTime_Click(object sender, EventArgs e)
		{

		}
	}
}
