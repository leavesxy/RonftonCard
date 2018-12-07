using log4net;
using System;
using System.Windows.Forms;
using RonftonCard.Core.Dongle;
using RonftonCard.Core;
using Bluemoon;
using System.Runtime.CompilerServices;
using System.Threading;

namespace RonftonCard.Main.Forms
{
	using Bluemoon.WinForm;
    using Core.DTO;

	public partial class DongleForm : Form
	{
		private ILog logger;
		private IDongle dongle;

		public DongleForm()
		{
			InitializeComponent();
			this.logger = ConfigManager.GetLogger();
		}

		private void DongleForm_Load(object sender, EventArgs e)
		{
			this.TxtUserRootKey.Text = BitConverter.ToString(ByteUtil.Malloc(16, 0x0a));
			this.TxtPlain.Text = "01234567";

			Update();

			RefreshDongle(true);
		}

		#region "--- event handler ---"

		private void CbDongleSelected_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.dongle.Open(this.CbDongle.SelectedIndex);
		}

		/// <summary>
		/// close dongle before form closing
		/// </summary>
		private void DongleForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.dongle != null)
			{
				this.dongle.Dispose();
				this.dongle = null;
			}
		}
		
		#endregion


		[MethodImpl(MethodImplOptions.Synchronized)]
		private void RefreshDongle(bool clear=false)
		{
			Action action = delegate ()
			{
				// clear trace
				this.CbDongle.Items.Clear();

				// create instance when form load
				if (this.dongle == null)
					this.dongle = ConfigManager.GetDongle();

				if (this.dongle != null)
				{
					// refresh dongle
					this.dongle.Enumerate();

					if (!this.dongle.Dongles.IsNullOrEmpty())
					{
						this.CbDongle.Items.AddRange(this.dongle.Dongles);
						this.CbDongle.SelectedIndex = 0;
						ShowDongleInfo(clear);
					}
				}
			};
			this.BeginInvoke(action);
		}

		private void ShowDongleInfo(bool clear)
		{
			this.Trace.Trace("Dongles information : ", clear);

			foreach( DongleInfo info in this.dongle.Dongles )
			{
				this.Trace.Trace(info.GetInfo());
			}
		}

		private void BtnEnumerate_Click(object sender, EventArgs e)
		{
			RefreshDongle(true);
		}

		private void BtnRestore_Click(object sender, EventArgs e)
		{
			byte[] adminPin = this.dongle.Encoder.GetBytes(this.TxtAdminPin.Text.Trim());
			int selected = this.CbDongle.SelectedIndex;

			if (this.dongle.Restore(adminPin))
			{
				logger.Debug(String.Format("restore {0} ok! dongle info = {1}", selected, this.dongle.Dongles[selected]));
				this.Trace.Trace(String.Format("restore {0} ok, old dongle info = {1}", selected, this.dongle.Dongles[selected]), true);
			}
			else
			{
				this.Trace.Trace(String.Format("restore {0} Error! error msg = {1}", selected, this.dongle.LastErrorMessage), true);
			}

			Update();
			Thread.Sleep(5000);

			// refresh dongle
			RefreshDongle(false);
		}

		private void BtnUtcTime_Click(object sender, EventArgs e)
		{
			this.Trace.Trace("Current Dongle 's Timer is : " + this.dongle.GetDevTimer().ToString("yyyy-MM-dd HH:mm:ss"), true);
		}

		#region "--- User root Key  ---"
		private void BtnCreateUserRootKey_Click(object sender, EventArgs e)
		{
			int selected = this.CbDongle.SelectedIndex;
			byte[] userRootKey = HexString.FromHexString(this.TxtUserRootKey.Text.Trim(), "-");
			String userId = this.TxtUserID.Text.Trim();

			DongleUserInfo keyInfo = DongleUserInfo.CreateTestDongleKeyInfo(DongleType.USER_ROOT, userId);

			ResultArgs arg = this.dongle.CreateUserRootDongle(userId, userRootKey, keyInfo);

			if (arg.Succ)
			{
				this.Trace.Trace(String.Format("Create user root key ok! "), true);
				UserRootDongleResult res = (UserRootDongleResult)arg.Result;

				this.Trace.Trace(res.ToString());
				logger.Debug(res.ToString());
			}
			else
			{
				this.Trace.Trace(String.Format("Create user root key Error! err msg = {0}", this.dongle.LastErrorMessage), true);
			}

			// refresh dongle
			RefreshDongle(false);
		}

		private void BntEncryptByUserRoot_Click(object sender, EventArgs e)
		{
			byte[] cipher;
			this.Trace.Trace("Encrypt By User root ...", true);
			this.Trace.Trace("plain text = " + this.TxtPlain.Text);

			byte[] plain = this.dongle.Encoder.GetBytes(this.TxtPlain.Text.Trim());
			int selected = this.CbDongle.SelectedIndex;

			if (this.dongle.Encrypt(DongleType.USER_ROOT, plain, out cipher))
			{
				this.Trace.Trace(String.Format("cipher length [{0}] , {1}",cipher.Length, BitConverter.ToString(cipher)));
			}
			else
				this.Trace.Trace("Encrypt failed !" + this.dongle.LastErrorMessage);
		}
		#endregion

		#region "--- Authen Key ---"
		private void BtnCreateAuthenKey_Click(object sender, EventArgs e)
		{
			int selected = this.CbDongle.SelectedIndex;
			String userId = this.TxtUserID.Text.Trim();

			DongleUserInfo keyInfo = DongleUserInfo.CreateTestDongleKeyInfo(DongleType.AUTHEN, userId);
			ResultArgs arg = this.dongle.CreateAppAuthenDongle(userId, keyInfo);

			if (arg.Succ)
			{
				this.Trace.Trace(String.Format("Create Authen key ok! "), true);
				AppAuthenDongleResult res = (AppAuthenDongleResult)arg.Result;

				this.Trace.Trace(res.ToString());
				logger.Debug(res.ToString());
			}
			else
			{
				this.Trace.Trace(String.Format("Create Authen key Error! err msg = {0}", this.dongle.LastErrorMessage), true);
			}

			// refresh dongle
			RefreshDongle(false);
		}


		private void BtnEncryptByPri_Click(object sender, EventArgs e)
		{
			byte[] cipher;
			this.Trace.Trace("Encrypt By Authen private key ...", true);
			this.Trace.Trace("plain text = " + this.TxtPlain.Text);

			byte[] plain = this.dongle.Encoder.GetBytes(this.TxtPlain.Text.Trim());
			int selected = this.CbDongle.SelectedIndex;

			if (this.dongle.Encrypt(DongleType.AUTHEN, plain, out cipher))
			{
				this.Trace.Trace(String.Format("cipher length [{0}] , {1}", cipher.Length, HexString.ToHexString(cipher)));
			}
			else
				this.Trace.Trace("Encrypt failed !" + this.dongle.LastErrorMessage);
		}

		#endregion

		


	}
}
