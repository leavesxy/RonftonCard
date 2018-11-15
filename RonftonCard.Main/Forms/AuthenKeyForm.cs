using log4net;
using RonftonCard.AuthenKey.RockeyArm;
using RonftonCard.Common.AuthenKey;
using System;
using System.Collections.Generic;
using System.Threading;
using Bluemoon;
using System.Windows.Forms;
using System.Text;
using RonftonCard.Common;

namespace RonftonCard.Main.Forms
{
	public partial class AuthenKeyForm : Form
	{
		private static ILog logger = LogManager.GetLogger("RonftonCardLog");
		private IAuthenKey[] authenKeys;
		private AuthenKeyInfo[] keyInfo;

		public AuthenKeyForm()
		{
			InitializeComponent();
		}

		private void AuthenKeyForm_Load(object sender, EventArgs e)
		{
			//this.authenKey = new DongleKey();
			this.CbKeyModel.Items.AddRange( ContextManager.AuthenKeyModels );
			this.CbKeyModel.SelectedIndex = 0;



		}

		private void EnumerateAuthenKey()
		{
			Action action = delegate ()
			{
				this.CbKeySelected.Items.Clear();
				this.keyInfo = this.authenKey.GetAuthenKeys();

				if (this.keyInfo != null)
				{
					this.keyInfo.ForEach(
							 key =>
							 {
								 this.CbKeySelected.Items.Add(key.GetName());
								 this.TxtTrace.Trace(key.ToString());
							 });
					this.CbKeySelected.SelectedIndex = 0;
				}
			};

			this.BeginInvoke(action);
		}


		#region "--- button handler ---"
		private void BtnEnumKey_Click(object sender, EventArgs e)
		{

		}

		private void BtnRestore_Click(object sender, EventArgs e)
		{
			this.TxtTrace.Trace("Restore ...", true);

			byte[] adminPin = Encoding.UTF8.GetBytes(this.TxtAdminPin.Text.Trim());

			if (this.authenKey.Restore(adminPin))
				this.TxtTrace.Trace("Restore OK, Key = " + BitConverter.ToString(adminPin));
			else
				this.TxtTrace.Trace("Restore failed ! " + this.authenKey.LastErrorMessage);
		}

		private void BtnCreateUserRootKey_Click(object sender, EventArgs e)
		{

		}

		private void BtnCreateAuthenKey_Click(object sender, EventArgs e)
		{

		}
		#endregion

		#region "--- event handler ---"

		private void CbKeySelected_SelectedIndexChanged(object sender, EventArgs e)
		{
			int selected = this.CbKeySelected.SelectedIndex;

			if (this.authenKey.Open(selected))
				this.TxtTrace.Trace(String.Format("Open {0} key OK, {1}", selected, keyInfo[selected].ToString()));
			else
				this.TxtTrace.Trace(String.Format("Open {0} key Failed, {1}", selected, this.authenKey.LastErrorMessage));
		}

		private void CbKeyModel_SelectedIndexChanged(object sender, EventArgs e)
		{
			Update();
			int sel = this.CbKeyModel.SelectedIndex;

			new Thread(() => EnumerateAuthenKey()).Start();
		}

		#endregion

	}
}
