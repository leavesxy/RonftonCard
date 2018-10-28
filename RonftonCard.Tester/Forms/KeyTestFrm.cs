using BlueMoon;
using RonftonCard.AuthenKey.RockeyArm;
using RonftonCard.Common.AuthenKey;
using System;
using System.Text;
using System.Windows.Forms;

namespace RonftonCard.Tester.Forms
{
	public partial class KeyTestFrm : Form
	{
		private DongleKey key;
		private byte[] userPwd;
		private byte[] adminPwd;
		private byte[] seed;

		public KeyTestFrm()
		{
			InitializeComponent();
		}

		private void KeyTestFrm_Load(object sender, EventArgs e)
		{
			this.key = new DongleKey();
			this.key.Open();
			this.TxtUserPwd.Text = "12345678";
			this.TxtAdminPwd.Text = "FFFFFFFFFFFFFFFF";
			this.TxtSeed.Text = "11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11";
			this.TxtTdesKey.Text = "0123456789abcdef";
			this.TxtUserID.Text = "0x30303031";
		}

		#region "--- Event handle ---"
		private void TxtUserPwd_TextChanged(object sender, EventArgs e)
		{
			this.userPwd = Encoding.Default.GetBytes(this.TxtUserPwd.Text.Trim());
		}

		private void TxtAdminPwd_TextChanged(object sender, EventArgs e)
		{
			this.adminPwd = Encoding.Default.GetBytes(this.TxtAdminPwd.Text.Trim());
		}

		private void KeyTestFrm_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.key.Close();
		}

		#endregion

		private void BtnEnumKey_Click(object sender, EventArgs e)
		{
			this.TxtDbg.Trace("Default CharSet : " + Encoding.Default.EncodingName, true);
			AuthenKeyInfo[] keyInfo = key.Enumerate();
			this.TxtDbg.Trace(String.Format( "Found {0} keys" , keyInfo.Length) );
			Array.ForEach(keyInfo, key => this.TxtDbg.Trace(key.ToString()));
		}

		private void BtnUniqueKey_Click(object sender, EventArgs e)
		{
			byte[] seed = StringConverterManager.ConvertTo<byte[]>(this.TxtSeed.Text.Trim());
			byte[] pid;
			byte[] newAdminPwd;

			this.TxtDbg.Trace("UniqueKey with key " + BitConverter.ToString(adminPwd), true);
			if (key.Initialize(seed, out newAdminPwd, out pid))
			{
				this.TxtDbg.Trace("UniqueKey OK !");
				this.TxtDbg.Trace("seed : " + BitConverter.ToString(seed));
				this.TxtDbg.Trace("new admin pwd : " + BitConverter.ToString(newAdminPwd));
				this.TxtDbg.Trace("  --- " + Encoding.ASCII.GetString(newAdminPwd));
				this.TxtDbg.Trace("pid : " + BitConverter.ToString(pid));
				this.TxtDbg.Trace("  --- " + Encoding.ASCII.GetString(pid));
			}
			else
				this.TxtDbg.Trace("UniqueKey Failed !");
		}

		private void BtnAdminAuthen_Click(object sender, EventArgs e)
		{
			this.TxtDbg.Trace("begin to authen by Admin : " + Encoding.ASCII.GetString(adminPwd), true);
			if( key.Authen(AuthenMode.ADMIN, adminPwd))
			{
				this.TxtDbg.Trace("Admin authen OK!");
			}
			else
				this.TxtDbg.Trace("Admin authen Failed!");
		}

		private void BtnUserAuthen_Click(object sender, EventArgs e)
		{
			this.TxtDbg.Trace("begin to authen by User: " + Encoding.ASCII.GetString(userPwd), true);
			if (key.Authen(AuthenMode.USER, userPwd))
			{
				this.TxtDbg.Trace("User authen OK!");
			}
			else
				this.TxtDbg.Trace("User authen Failed!");
		}

		private void BtnCreateCompanyKeyFile_Click(object sender, EventArgs e)
		{
			this.TxtDbg.Trace("Create Company seed key ...", true);

			byte[] deskey = Encoding.Default.GetBytes(this.TxtTdesKey.Text.Trim());

			if( this.key.Create( AuthenKeyType.COMPANY_SEED, deskey) )
			{
				this.TxtDbg.Trace("Create Company seed key OK! " + BitConverter.ToString(deskey));
			}
			else
				this.TxtDbg.Trace("Create 3Des Key failed!" + this.key.LastErrorMessage);
		}

		private void BtnTdesEncrypt_Click(object sender, EventArgs e)
		{
			byte[] cipher;
			this.TxtDbg.Trace("Encrypt with 3Des ...", true);
			this.TxtDbg.Trace("plain text = " + this.TxtPlain.Text);

			//if (this.key.TDesEncrypt(this.TxtPlain.Text.Trim(), out cipher))
			//{
			//	this.TxtDbg.Trace("cipher length = " + cipher.Length);
			//	this.TxtDbg.Trace("cipher data = " + BitConverter.ToString(cipher));
			//}
			//else
			//	this.TxtDbg.Trace("Encrypt failed !" + this.key.GetLastErrMsg());
		}

		private void BtnRestore_Click(object sender, EventArgs e)
		{
			this.TxtDbg.Trace("Restore ...", true);
			if (this.key.Restore())
				this.TxtDbg.Trace("Restore OK...");
			else
				this.TxtDbg.Trace("Restore failed ! " + this.key.LastErrorMessage);
		}

		private void BtnSetUserID_Click(object sender, EventArgs e)
		{
			//this.TxtDbg.Trace("Set User ID ...", true);

			//uint userid = StringConverterManager.ConvertTo<uint>(this.TxtUserID.Text.Trim());
			//if (this.key.SetUserID(userid))
			//	this.TxtDbg.Trace("Set User ID OK..." + userid.ToString("x8"));
			//else
			//	this.TxtDbg.Trace("Set User ID failed ! " + this.key.GetLastErrMsg());
		}

		private void BtnCreateRsaKeyFile_Click(object sender, EventArgs e)
		{
			//this.TxtDbg.Trace("CreateRsaKeyFile ...", true);
			//if( this.key.CreateRSAKeyFile())
			//	this.TxtDbg.Trace("CreateRsaKeyFile OK..." );
			//else
			//	this.TxtDbg.Trace("CreateRsaKeyFile failed ! " + this.key.GetLastErrMsg());

		}

		private void BtnRsaPriEncrypt_Click(object sender, EventArgs e)
		{

		}

		private void BtnDbgErrorMsg_Click(object sender, EventArgs e)
		{
			this.TxtDbg.Trace("Show Error Message ...", true);
			this.TxtDbg.Trace(this.key.GetAllErrorMessage());
		}
	}
}
