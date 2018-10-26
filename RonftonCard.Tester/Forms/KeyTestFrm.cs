using BlueMoon;
using RonftonCard.AuthenKey.RockeyArm;
using RonftonCard.Common.Authen;
using System;
using System.Text;
using System.Windows.Forms;

namespace RonftonCard.Tester.Forms
{
	public partial class KeyTestFrm : Form
	{
		private IAuthenKey key;
		private byte[] userPwd;
		private byte[] devPwd;

		public KeyTestFrm()
		{
			InitializeComponent();
		}

		private void KeyTestFrm_Load(object sender, EventArgs e)
		{
			key = new DongleKey();
		}

		#region "--- Event handle ---"
		private void TxtUserPwd_TextChanged(object sender, EventArgs e)
		{
			this.userPwd = Encoding.Default.GetBytes(this.TxtUserPwd.Text.Trim());
		}

		private void TxtDevPwd_TextChanged(object sender, EventArgs e)
		{
			this.devPwd = Encoding.Default.GetBytes(this.TxtAdminPwd.Text.Trim());
		}
		#endregion

		private void BtnGetInfo_Click(object sender, EventArgs e)
		{
			this.TxtDbg.Trace("Default CharSet : " + Encoding.Default.EncodingName, true);
			this.TxtDbg.Trace(key.GetKeyInfo().ToString());
		}

		private void BtnUniqueKey_Click(object sender, EventArgs e)
		{

		}


	}
}
