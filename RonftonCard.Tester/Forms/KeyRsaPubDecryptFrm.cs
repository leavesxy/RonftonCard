using log4net;
using RonftonCard.AuthenKey.RockeyArm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RonftonCard.Tester.Forms
{
	public partial class KeyRsaPubDecryptFrm : Form
	{
		protected static ILog logger = LogManager.GetLogger("RonftonCardLog");

		private DongleKey key;

		public KeyRsaPubDecryptFrm(DongleKey key)
		{
			InitializeComponent();
			this.key = key;
		}

		private void KeyRsaPubDecryptFrm_Load(object sender, EventArgs e)
		{
			this.TxtPlain.Text = @"Wz8eQ8IqsvcGdtP+ttzLQp9bi+xtEJ1cSWelvFaYQ5Aj4dNaf1uHiRO6g6cGTceaJK5R8VvQTBaBw7I31rqrw5Vl0FikrPUKd26FbQRLQVA3VjDlLbRFDL3nq/CDFjrrI+CUU/vY1BFJVI4NzGoZdhwV3GaPPOg5vA64mlqYTRc=";
			this.TxtRsaPubKey.Text = @"uVu1P6VlVZA/GPzbO/uap5xq20OC4dVWq/jTb20Pta3jq7AFqThrUkqzKiCscoFIymeYgSMCazAVZTwMaMMc9OWz2v3lGQBdb0rLrrsxfO0DHZSJv/M7VifKLJWQc5qca+ST93vqD2PvpWmW/dN6wAui3rVRZRYFgGRvcQiKSn0AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA==";
		}

		private void BtnReturn_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void BtnRsaPubDecrypt_Click(object sender, EventArgs e)
		{
			//byte[] pubKey = Convert.FromBase64String(this.TxtRsaPubKey.Text.Trim());
			//byte[] plain = Convert.FromBase64String(this.TxtPlain.Text.Trim());
			//byte[] cipher;

			//this.key.RsaPubDecrypt(pubKey, plain, out cipher);
			//if (this.key.IsSucc())
			//{
			//	this.TxtDbg.Trace("Decrypt OK!", true);
			//	this.TxtDbg.Trace("cipher = " + BitConverter.ToString(cipher));
			//	this.TxtDbg.Trace("       = " + Encoding.Default.GetString(cipher));
			//}
			//else
			//	this.TxtDbg.Trace("Decrypt error!" + this.key.LastErrorMessage );
		}
	}
}
