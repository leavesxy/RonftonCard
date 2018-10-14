using System;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using BlueMoon.Form;
using RonftonCard.Common;
using RonftonCard.Tester.Entity;
using RonftonCard.Common.Reader;
using System.Reflection;
using BlueMoon.Util;

namespace RonftonCard.Tester.Forms
{
	public partial class TestMainFrm : Form
	{
		private ResourceManager rm;

		public TestMainFrm(ResourceManager rm)
		{
			InitializeComponent();
			this.rm = rm;
		}

		private void TestMainFrm_Load(object sender, EventArgs e)
		{
			this.Text = this.rm.GetFormTextName();
			this.Icon = (Icon)this.rm.GetObject("TestMain");

			try
			{
				CardContextManager.LoadCardTemplete("CardTemplete.xml");
				CardContextManager.LoadCardReader("CardReader.xml");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			this.CbCardTemplete.Items.AddRange(CardContextManager.GetCardTempleteNames());
			this.CbCardTemplete.SelectedIndex = 0;

			this.CbCardReader.Items.AddRange(CardContextManager.GetCardReaderNames());
			this.CbCardReader.SelectedIndex = 0;

			this.TxtControlBlock.Text = "{1 0 0}, {0 1 1}";
			Update();
		}

		#region "--- Event handle ---"

		private void CbCardTemplete_SelectedIndexChanged(object sender, EventArgs e)
		{
			BtnDbgCardTemplete_Click(this, null);
		}

		private void CbCardReader_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		#endregion

		/// <summary>
		/// exit 
		/// </summary>
		private void BtnExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		#region "--- Config Test ---"

		private void BtnDbgCardTemplete_Click(object sender, EventArgs e)
		{
			String cardTempleteName = CbCardTemplete.SelectedItem as String;
			Dbg(CardContextManager.GetCardTemplete(cardTempleteName).DbgStorageItems(), true);
			Dbg(CardContextManager.GetCardTemplete(cardTempleteName).DbgDataItems());
		}

		private void BtnDbgCardEntity_Click(object sender, EventArgs e)
		{
			CardEntity entity = CardEntity.CreateTestEntity();
			Dbg("Data written to Card ...", true);
			Dbg("---------------------------------------------");
			Dbg(entity.ToString());
		}

		private void BtnWriteVirtualCard_Click(object sender, EventArgs e)
		{
			String cardTempleteName = CbCardTemplete.SelectedItem as String;
			M1VirtualCard mvc = new M1VirtualCard(this.TxtControlBlock.Text, CardContextManager.GetCardTemplete(cardTempleteName), null);
			Dbg("User data control String : " + this.TxtControlBlock.Text, true);
			Dbg("Key control block    : " + mvc.DbgControlBlock());
			Dbg("size of virtual card : " + mvc.CardTemplete.CardSize.ToString());
			Dbg("physical address : " + mvc.DbgSectors());
		}
		#endregion

		#region "--- Card reader Test ---"
		private void BtnReaderInit_Click(object sender, EventArgs e)
		{
			String readerName = this.CbCardReader.SelectedItem as String;
			Dbg("Current activated CardReader : " + readerName, true);
			Dbg(CardContextManager.GetCardReaderDesc(readerName));

			using (ICardReader reader = CardContextManager.GetCardReader(readerName))
			{
				if (reader != null)
				{
					Dbg("reader version = " + reader.GetVersion());
					reader.Beep(2);
					reader.Close();
				}
			}

			// show load assemblies
			DbgAllAssemblies();
		}
		private void BtnSelectCard_Click(object sender, EventArgs e)
		{
			Dbg("Begin to Select Card ...", true);

			String readerName = this.CbCardReader.SelectedItem as String;
			using (ICardReader reader = CardContextManager.GetCardReader(readerName))
			{
				if (reader != null)
				{
					byte[] cardId;
					if (reader.Select(out cardId))
					{
						Dbg("Select Card OK! id = " + BitConverter.ToString(cardId));
					}
					reader.Close();
				}
			}
		}
		private void BtnReadBlock_A_Click(object sender, EventArgs e)
		{
			byte[] keyA = ByteUtil.FromHexString(this.TxtKeyA.Text.Trim());
			ReadBlock(KeyMode.KeyA, keyA);

		}

		private void BtnReadBlock_B_Click(object sender, EventArgs e)
		{
			byte[] keyB = ByteUtil.FromHexString(this.TxtKeyB.Text.Trim());
			ReadBlock(KeyMode.KeyB, keyB);
		}

		private void ReadBlock(KeyMode keyMode, byte[] key)
		{
			Dbg("ReadBlock with " + keyMode.ToString(), true);
			int blockNum = int.Parse(this.TxtBlockNum.Text);

			String readerName = this.CbCardReader.SelectedItem as String;
			using (ICardReader reader = CardContextManager.GetCardReader(readerName))
			{
				if (reader != null)
				{
					byte[] cardId;
					if (reader.Select(out cardId))
					{
						Dbg("Select Card OK! id = " + BitConverter.ToString(cardId));

						if (!reader.Authen(keyMode, blockNum, key))
						{
							Dbg("Authen failed !");
							return;
						}

						Dbg("Auth OK , key = " + BitConverter.ToString(key));

						byte[] block;
						if (reader.Read(blockNum, out block))
						{
							Dbg(String.Format("Read block #{0} ok ! --> {1}",
								blockNum,
								BitConverter.ToString(block)));
						}
					}
					reader.Close();
				}
			}
		}

		private void BtnWriteBlock_Click(object sender, EventArgs e)
		{
			Dbg("WriteBlock::Begin to Select Card ...", true);
			int blockNum = int.Parse(this.TxtBlockNum.Text);

			String readerName = this.CbCardReader.SelectedItem as String;
			using (ICardReader reader = CardContextManager.GetCardReader(readerName))
			{
				if (reader != null)
				{
					byte[] cardId;
					if (reader.Select(out cardId))
					{
						Dbg("Select Card OK! id = " + BitConverter.ToString(cardId));

						//byte[] pwdA = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
						byte[] pwdB = new byte[] { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff };

						if (!reader.Authen(KeyMode.KeyB, blockNum, pwdB))
						{
							Dbg("Authen failed !");
							return;
						}

						byte[] block = new byte[] { 0x10, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f };
						if (reader.Write(blockNum, block))
						{
							Dbg(String.Format("Write block #{0} ok ! --> {1}",
								blockNum,
								BitConverter.ToString(block)));
						}
					}
					reader.Close();
				}
			}
		}
		#endregion

		#region "--- debug util ---"
		private void Dbg(String msg, bool isClear = false)
		{
			if (isClear)
				this.TxtDbg.Clear();

			this.TxtDbg.Text += msg + Environment.NewLine;
		}

		private void Dbg(String format, params Object[] args)
		{
			this.TxtDbg.Text += String.Format(format, args) + Environment.NewLine;
		}

		private void DbgCr()
		{
			this.TxtDbg.Text += Environment.NewLine;
		}

		private void Dbg(byte[] msg, bool isClear = false)
		{
			if (isClear)
				this.TxtDbg.Clear();

			this.TxtDbg.Text += BitConverter.ToString(msg) + Environment.NewLine;
		}

		private void DbgAllAssemblies()
		{
			Assembly[] assembies = AppDomain.CurrentDomain.GetAssemblies();
			Dbg("Current assemblies loaded : ");

			foreach (Assembly assembly in assembies)
			{
				// filter mircosoft, system
				String name = assembly.GetName().Name;
				if (!name.StartsWith("System", StringComparison.CurrentCultureIgnoreCase) &&
					!name.StartsWith("Microsoft", StringComparison.CurrentCultureIgnoreCase))
				{
					Dbg(" --> name = {0}", name);
				}
			}
		}



		#endregion

		private void BtnUpdateKeyA_Click(object sender, EventArgs e)
		{
			byte[] keyA = ByteUtil.FromHexString(this.TxtKeyA.Text.Trim());
			UpdateKey(KeyMode.KeyA, keyA);
		}
		private void BtnUpdateKeyB_Click(object sender, EventArgs e)
		{
			byte[] keyB = ByteUtil.FromHexString(this.TxtKeyB.Text.Trim());
			UpdateKey(KeyMode.KeyB, keyB);
		}

		private void UpdateKey(KeyMode keyMode, byte[] key)
		{
			Dbg("UpdateKey with " + keyMode.ToString() , true);
			int blockNum = int.Parse(this.TxtBlockNum.Text);

			String readerName = this.CbCardReader.SelectedItem as String;
			using (ICardReader reader = CardContextManager.GetCardReader(readerName))
			{
				if (reader != null)
				{
					byte[] cardId;
					if (reader.Select(out cardId))
					{
						Dbg("Select Card OK! id = " + BitConverter.ToString(cardId));
						if (!reader.Authen(keyMode, blockNum, key))
						{
							Dbg("Authen failed !");
							return;
						}

						Dbg("Auth OK , key = " + BitConverter.ToString(key));

						byte[] block = new byte[] { 0x01, 0x01, 0x01, 0x01, 0x01, 0x01, 0x78, 0x77, 0x88, 0x69, 0x0f, 0x0f, 0x0f, 0x0f, 0x0f, 0x0f };
						if (!reader.Write(blockNum, block))
						{
							Dbg("Update failed !");
							return;
						}
						Dbg(String.Format("Write block #{0} ok ! --> {1}",
								blockNum,
								BitConverter.ToString(block)));
					}
					reader.Close();
				}
			}
		}
	}
}