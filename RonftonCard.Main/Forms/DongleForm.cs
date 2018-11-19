using log4net;
using System;
using System.Windows.Forms;
using RonftonCard.Core.Dongle;
using RonftonCard.Core;
using Bluemoon;
using RonftonCard.Core.Entity;
using System.Runtime.CompilerServices;

namespace RonftonCard.Main.Forms
{
	public partial class DongleForm : Form
	{
		private static ILog logger = LogManager.GetLogger("RonftonCardLog");
		private IDongle dongle;

		public DongleForm()
		{
			InitializeComponent();
		}

		private void DongleForm_Load(object sender, EventArgs e)
		{
			this.TxtUserRootKey.Text = BitConverter.ToString(ByteUtil.Malloc(16, 0x0a));
			this.TxtPlain.Text = "01234567";
			Update();

			RefreshDongle(true);
		}

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

					this.CbDongle.Items.AddRange(this.dongle.Dongles);
					this.CbDongle.SelectedIndex = 0;
					ShowDongleInfo(clear);
				}
			};
			this.BeginInvoke(action);
		}

		private void ShowDongleInfo(bool clear)
		{
			if (this.dongle == null || this.dongle.Dongles == null || this.dongle.Dongles.Length == 0)
				return;

			this.TxtTrace.Trace("Dongles information : ", clear);

			foreach( DongleInfo info in this.dongle.Dongles )
			{
				this.TxtTrace.Trace(info.ToString());
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

			if (this.dongle.Restore(adminPin, selected))
			{
				logger.Debug(String.Format("restore {0} ok! dongle info = {1}", selected, this.dongle.Dongles[selected]));
				this.TxtTrace.Trace(String.Format("restore {0} ok! dongle info = {1}", selected, this.dongle.Dongles[selected]), true);
			}
			else
			{
				this.TxtTrace.Trace(String.Format("restore {0} Error! error msg = {1}", selected, this.dongle.LastErrorMessage), true);
			}

			// refresh dongle
			RefreshDongle(false);
		}

		#region "--- User root Key  ---"
		private void BtnCreateUserRootKey_Click(object sender, EventArgs e)
		{
			int selected = this.CbDongle.SelectedIndex;
			byte[] userRootKey = HexString.FromHexString(this.TxtUserRootKey.Text.Trim(), "-");
			ResultArgs arg = this.dongle.CreateUserRootKey(this.TxtUserID.Text.Trim(), userRootKey, selected);

			if (arg.Succ)
			{
				this.TxtTrace.Trace(String.Format("Create user root key ok! "), true);
				UserRootKeyResponse res = (UserRootKeyResponse)arg.Result;

				this.TxtTrace.Trace(res.ToString());
				logger.Debug(res.ToString());
			}
			else
			{
				this.TxtTrace.Trace(String.Format("Create user root key Error! err msg = {0}", this.dongle.LastErrorMessage), true);
			}

			// refresh dongle
			RefreshDongle(false);
		}

		private void BntEncryptByUserRoot_Click(object sender, EventArgs e)
		{
			byte[] cipher;
			this.TxtTrace.Trace("Encrypt By User root ...", true);
			this.TxtTrace.Trace("plain text = " + this.TxtPlain.Text);

			byte[] plain = this.dongle.Encoder.GetBytes(this.TxtPlain.Text.Trim());
			int selected = this.CbDongle.SelectedIndex;

			if (this.dongle.Encrypt(plain, out cipher, selected))
			{
				this.TxtTrace.Trace(String.Format("cipher length [{0}] , {1}",cipher.Length, BitConverter.ToString(cipher)));
			}
			else
				this.TxtTrace.Trace("Encrypt failed !" + this.dongle.LastErrorMessage);
		}
		#endregion

		#region "--- Authen Key ---"
		private void BtnCreateAuthenKey_Click(object sender, EventArgs e)
		{
			int selected = this.CbDongle.SelectedIndex;
			ResultArgs arg = this.dongle.CreateAuthenKey(this.TxtUserID.Text.Trim(), selected);

			if (arg.Succ)
			{
				this.TxtTrace.Trace(String.Format("Create Authen key ok! "), true);
				AuthenKeyResponse res = (AuthenKeyResponse)arg.Result;

				this.TxtTrace.Trace(res.ToString());
				logger.Debug(res.ToString());
			}
			else
			{
				this.TxtTrace.Trace(String.Format("Create Authen key Error! err msg = {0}", this.dongle.LastErrorMessage), true);
			}

			// refresh dongle
			RefreshDongle(false);
		}


		private void BtnEncryptByPri_Click(object sender, EventArgs e)
		{
			byte[] cipher;
			this.TxtTrace.Trace("Encrypt By Authen private key ...", true);
			this.TxtTrace.Trace("plain text = " + this.TxtPlain.Text);

			byte[] plain = this.dongle.Encoder.GetBytes(this.TxtPlain.Text.Trim());
			int selected = this.CbDongle.SelectedIndex;

			if (this.dongle.PriEncrypt(plain, out cipher, selected))
			{
				this.TxtTrace.Trace(String.Format("cipher length [{0}] , {1}", cipher.Length, HexString.ToHexString(cipher)));
			}
			else
				this.TxtTrace.Trace("Encrypt failed !" + this.dongle.LastErrorMessage);
		}

		#endregion



		#region "--- event handler ---"

		private void CbDongleSelected_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		/// <summary>
		/// close dongle
		/// </summary>
		private void DongleForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.dongle != null)
			{
				this.dongle.Close();
				this.dongle = null;
			}
		}



		#endregion


	}
}
