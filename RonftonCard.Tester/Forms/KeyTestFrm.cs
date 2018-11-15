using Bluemoon;
using Bluemoon.Converter;
using log4net;
using RonftonCard.AuthenKey.RockeyArm;
using RonftonCard.Common.AuthenKey;
using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace RonftonCard.Tester.Forms
{
	public partial class KeyTestFrm : Form
	{
		protected static ILog logger = LogManager.GetLogger("RonftonCardLog");

		private DongleKey key;
		private byte[] userPwd;
		private byte[] adminPwd;
		private byte[] seed;
		AuthenKeyInfo[] keyInfo;

		public KeyTestFrm()
		{
			InitializeComponent();
		}

		private void KeyTestFrm_Load(object sender, EventArgs e)
		{
			this.key = new DongleKey();
			this.TxtUserPwd.Text = "12345678";
			this.TxtAdminPwd.Text = "FFFFFFFFFFFFFFFF";
			this.TxtSeed.Text = "11,11,11,11,11,11,11,11,11,11,11,11,11,11,11,11";
			this.TxtTdesKey.Text = "0123456789abcdef";
			this.TxtUserID.Text = "0x30303031";

			//flesh UI,and enumerate Key
			Update();
			new Thread(() => EnumerateKey()).Start();
		}

		private void EnumerateKey()
		{
			Action action = delegate()
			{
				this.CbCurrentKey.Items.Clear();
				keyInfo = key.GetAuthenKeys().ToArray();
				if (!keyInfo.IsNullOrEmpty())
				{
					Array.ForEach(keyInfo,
						 k =>
						 {
							 this.CbCurrentKey.Items.Add(k.GetName());
							 this.TxtDbg.Trace(k.ToString());
						 });
				}
				this.CbCurrentKey.SelectedIndex = 0;
			};
			this.BeginInvoke(action);
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

		private void CbCurrentKey_SelectedIndexChanged(object sender, EventArgs e)
		{
			int selected = this.CbCurrentKey.SelectedIndex;

			if (this.key.Open(selected))
				this.TxtDbg.Trace(String.Format("Open {0} key OK, {1}" , selected, keyInfo[selected].ToString()));
			else
				this.TxtDbg.Trace(String.Format("Open {0} key Failed, {1}", selected, this.key.LastErrorMessage));
		}

		#endregion

		#region "--- button handle ---"
		private void BtnEnumKey_Click(object sender, EventArgs e)
		{
			this.TxtDbg.Trace("Default CharSet : " + Encoding.Default.EncodingName, true);
			EnumerateKey();
		}

		private void BtnUniqueKey_Click(object sender, EventArgs e)
		{
			//byte[] seed = StringConverterManager.ConvertTo<byte[]>(this.TxtSeed.Text.Trim());
			//byte[] pid;
			//byte[] newAdminPwd;

			//this.TxtDbg.Trace("UniqueKey with key " + BitConverter.ToString(adminPwd), true);
			//if (key.Initialize(seed, out newAdminPwd, out pid))
			//{
			//	this.TxtDbg.Trace("UniqueKey OK !");
			//	this.TxtDbg.Trace("seed : " + BitConverter.ToString(seed));
			//	this.TxtDbg.Trace("new admin pwd : " + BitConverter.ToString(newAdminPwd));
			//	this.TxtDbg.Trace("  --- " + Encoding.ASCII.GetString(newAdminPwd));
			//	this.TxtDbg.Trace("pid : " + BitConverter.ToString(pid));
			//	this.TxtDbg.Trace("  --- " + Encoding.ASCII.GetString(pid));
			//}
			//else
			//	this.TxtDbg.Trace("UniqueKey Failed !");
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

		private void BtnCreateCompanySeed_Click(object sender, EventArgs e)
		{
			//this.TxtDbg.Trace("Create Company seed key ...", true);

			//byte[] compKey = Encoding.Default.GetBytes(this.TxtTdesKey.Text.Trim());
			//byte[] outData;

			//if( this.key.Create( AuthenKeyType.COMPANY_SEED, compKey, out outData) )
			//{
			//	this.TxtDbg.Trace("Create Company seed key OK! " + BitConverter.ToString(compKey));
			//}
			//else
			//	this.TxtDbg.Trace("Create 3Des Key failed!" + this.key.LastErrorMessage);
		}

		private void BtnCreateUserRootKey_Click(object sender, EventArgs e)
		{
			//this.TxtDbg.Trace("Create Company seed key ...", true);

			//// to test, use reverse of company
			//char[] ch = this.TxtTdesKey.Text.Trim().ToCharArray();
			//Array.Reverse(ch);
			//String keyString = new String(ch);

			//byte[] userKey = Encoding.Default.GetBytes(keyString);
			//byte[] outData;

			//if (this.key.Create(AuthenKeyType.USER_ROOT, userKey, out outData))
			//{
			//	this.TxtDbg.Trace("Create User key OK! " + BitConverter.ToString(userKey));
			//}
			//else
			//	this.TxtDbg.Trace("Create User Key failed!" + this.key.LastErrorMessage);
		}

		private void BtnEncryptByCompanySeed_Click(object sender, EventArgs e)
		{
			//byte[] cipher;
			//this.TxtDbg.Trace("Encrypt By Company seed ...", true);
			//this.TxtDbg.Trace("plain text = " + this.TxtPlain.Text);
			//byte[] plain = Encoding.Default.GetBytes(this.TxtPlain.Text.Trim());

			//if (this.key.Encrypt( AuthenKeyType.COMPANY_SEED, plain, out cipher))
			//{
			//	this.TxtDbg.Trace(String.Format("cipher length [{0}] , {1}" ,cipher.Length, BitConverter.ToString(cipher)));
			//}
			//else
			//	this.TxtDbg.Trace("Encrypt failed !" + this.key.LastErrorMessage);
		}

		private void BtnEncryptByUserRoot_Click(object sender, EventArgs e)
		{
			//byte[] cipher;
			//this.TxtDbg.Trace("Encrypt By User root ...", true);
			//this.TxtDbg.Trace("plain text = " + this.TxtPlain.Text);
			//byte[] plain = Encoding.Default.GetBytes(this.TxtPlain.Text.Trim());

			//if (this.key.Encrypt(AuthenKeyType.USER_ROOT, plain, out cipher))
			//{
			//	this.TxtDbg.Trace(String.Format("cipher length [{0}] , {1}" + cipher.Length, BitConverter.ToString(cipher)));
			//}
			//else
			//	this.TxtDbg.Trace("Encrypt failed !" + this.key.LastErrorMessage);
		}

		private void BtnRestore_Click(object sender, EventArgs e)
		{
			//this.TxtDbg.Trace("Restore ...", true);
			//if (this.key.Restore())
			//	this.TxtDbg.Trace("Restore OK...");
			//else
			//	this.TxtDbg.Trace("Restore failed ! " + this.key.LastErrorMessage);
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
			//byte[] rsaPubKey;

			//if (this.key.Create(AuthenKeyType.AUTHEN, null, out rsaPubKey))
			//{
			//	this.TxtDbg.Trace("CreateRsaKeyFile OK...pub key :");
			//	this.TxtDbg.Trace("RSA-PUB byte : " + BitConverter.ToString(rsaPubKey));
			//	this.TxtDbg.Trace("RSA-PUB Base64 : " + Convert.ToBase64String(rsaPubKey));
			//}
			//else
			//	this.TxtDbg.Trace("CreateRsaKeyFile failed ! " + this.key.LastErrorMessage);
		}

		private void BtnRsaPriEncrypt_Click(object sender, EventArgs e)
		{
			//byte[] cipher;
			//this.TxtDbg.Trace("Encrypt By RSA-PRI ...", true);
			//this.TxtDbg.Trace("plain text = " + this.TxtPlain.Text);
			//byte[] plain = Encoding.Default.GetBytes(this.TxtPlain.Text.Trim());

			//if (this.key.Encrypt(AuthenKeyType.AUTHEN, plain, out cipher))
			//{
			//	this.TxtDbg.Trace(String.Format("cipher length [{0}] ", cipher.Length));
			//	this.TxtDbg.Trace("hex    : " + HexString.ToHexString(cipher));
			//	this.TxtDbg.Trace("base64 : " + Convert.ToBase64String(cipher));

			//	logger.Debug("RSA-PRI encrypt [" + this.TxtPlain.Text.Trim() + "]");
			//	logger.Debug("   hex String  : " + HexString.ToHexString(cipher));
			//	logger.Debug("   base64      : " + Convert.ToBase64String(cipher));
			//}
			//else
			//	this.TxtDbg.Trace("Encrypt failed !" + this.key.LastErrorMessage);
		}

		private void BtnRsaPubDecrypt_Click(object sender, EventArgs e)
		{
			KeyRsaPubDecryptFrm frm = new KeyRsaPubDecryptFrm(this.key);
			frm.ShowDialog();
		}

		private void BtnDbgErrorMsg_Click(object sender, EventArgs e)
		{
			this.TxtDbg.Trace("Show Error Message ...", true);
			this.TxtDbg.Trace(this.key.GetAllErrorMessage());
		}



		#endregion


	}
}
