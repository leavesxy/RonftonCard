using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace RonftonCard.Main.Forms
{
	using Bluemoon;
	using Core;
	using Core.CardReader;
	using Core.DTO;
	using KeyService;
	using System.Text;

	public partial class CardForm : Form
	{
		private List<CheckBox> cardSectorSelected;

		private const String DEFAULT_KEY_A = "ff-ff-ff-ff-ff-ff";
		private const String DEFAULT_KEY_B = "ff-ff-ff-ff-ff-ff";
		private String[] AUTHEN_KEY_MODE = new String[] { "Key_A", "Key_B" };

		private ICardReader reader;

		public CardForm()
		{
			InitializeComponent();
		}

		private void CardForm_Load(object sender, EventArgs e)
		{
			this.TxtKeyA.Text = DEFAULT_KEY_A;
			this.TxtKeyB.Text = DEFAULT_KEY_B;

			this.CbAuthenKey.Items.AddRange(AUTHEN_KEY_MODE);
			this.CbAuthenKey.SelectedIndex = 1;

			this.cardSectorSelected = new List<CheckBox>()
			{
				this.Cb0,this.Cb1,this.Cb2,this.Cb3,this.Cb4,this.Cb5,this.Cb6,this.Cb7,
				this.Cb8,this.Cb9,this.Cb10,this.Cb11,this.Cb12,this.Cb13,this.Cb14,this.Cb15,
			};
			ResetCardBlock();

			this.reader = ConfigManager.GetCardReader();
			this.reader.Open();
			Update();
		}

		private void ResetCardBlock()
		{
			UInt16[] sectors = ConfigManager.GetCardTemplete().SegmentAddr;

			this.cardSectorSelected.ForEach(m =>
			{
				if (sectors.Contains(UInt16.Parse(m.Text)))
					m.Checked = true;
			});
		}


		#region "--- button handler ---"
		private void BtnSelectCard_Click(object sender, EventArgs e)
		{
			byte[] card_id;

			if (this.reader.Select(out card_id))
			{
				this.TxtDbg.Trace("Select card id =" + BitConverter.ToString(card_id), true);
			}
		}

		private void BtnSelectCard2_Click(object sender, EventArgs e)
		{
			byte[] cardId;
			UInt16 atqa;
			byte sak;

			if (this.reader.Select2(out cardId, out atqa, out sak))
			{
				this.TxtDbg.Trace("Select2 ", true);
				this.TxtDbg.Trace(String.Format("card_id={0}, atqa={1}, sak={2}",
						BitConverter.ToString(cardId),
						atqa.ToString("X4"),
						sak));
			}
		}

		private void BtnTestKeyA_Click(object sender, EventArgs e)
		{
			byte[] keyA = HexString.FromHexString(this.TxtKeyA.Text.Trim(), "-");

			this.TxtDbg.Trace("Test KeyA : " + BitConverter.ToString(keyA), true);
			TestKey(M1KeyMode.KEY_A, keyA);
		}

		private void BtnTestKeyB_Click(object sender, EventArgs e)
		{
			byte[] keyB = HexString.FromHexString(this.TxtKeyB.Text.Trim(), "-");

			this.TxtDbg.Trace("Test KeyB : " + BitConverter.ToString(keyB), true);
			TestKey(M1KeyMode.KEY_B, keyB);
		}

		private void TestKey(M1KeyMode keyMode, byte[] key)
		{
			foreach (CheckBox cb in this.cardSectorSelected)
			{
				if (!cb.Checked)
					continue;

				int sector = int.Parse(cb.Text.Trim());
				if (!this.reader.Authen(keyMode, sector * 4, key))
					this.TxtDbg.Trace("Authen sector {0} failed !", sector);
				else
					this.TxtDbg.Trace("Authen sector {0} OK !", sector);
			}
		}

		private void BtnReadBlockA_Click(object sender, EventArgs e)
		{
			this.TxtDbg.Trace("Read Card With Key_A ...", true);
			byte[] keyA = HexString.FromHexString(this.TxtKeyA.Text.Trim(), "-");
			foreach (CheckBox cb in this.cardSectorSelected)
			{
				if (!cb.Checked)
					continue;

				int sector = int.Parse(cb.Text.Trim());
				ReadSector(M1KeyMode.KEY_A, sector, keyA);
			}
		}


		private void BtnReadBlockB_Click(object sender, EventArgs e)
		{
			this.TxtDbg.Trace("Read Card by KeyB...", true);
			byte[] keyB = HexString.FromHexString(this.TxtKeyB.Text.Trim(), "-");

			foreach (CheckBox cb in this.cardSectorSelected)
			{
				if (!cb.Checked)
					continue;

				int sector = int.Parse(cb.Text.Trim());
				ReadSector(M1KeyMode.KEY_B, sector, keyB);
			}
		}

		private void ReadSector(M1KeyMode keyMode, int sector, byte[] key)
		{
			if (!this.reader.Authen(keyMode, sector * 4, key))
			{
				this.TxtDbg.Trace("Auth sector {0} failed !", sector);
				return;
			}

			byte[] buffer;
			int len = 0;
			if (this.reader.ReadSector(sector, out buffer, out len))
			{
				this.TxtDbg.Trace("Sector {0}", sector);
				this.TxtDbg.Trace(BitConverter.ToString(buffer, 0, 16));
				this.TxtDbg.Trace(BitConverter.ToString(buffer, 16, 16));
				this.TxtDbg.Trace(BitConverter.ToString(buffer, 32, 16));
				this.TxtDbg.Trace(BitConverter.ToString(buffer, 48, 16));
			}
		}



		private void BtnUpdateKeyA_Click(object sender, EventArgs e)
		{

		}

		private void BtnUpdateKeyB_Click(object sender, EventArgs e)
		{

		}

		private void BtnWriteBlock_Click(object sender, EventArgs e)
		{

		}

		private void BtnReadlBlock_Click(object sender, EventArgs e)
		{
			byte[] card_id;

			if (this.reader.Select(out card_id))
			{
				this.TxtDbg.Trace("Select card id =" + BitConverter.ToString(card_id), true);
			}

			byte[] keyB = HexString.FromHexString(this.TxtKeyB.Text.Trim(), "-");
			byte[] block0;

			if (!this.reader.Authen(M1KeyMode.KEY_A, 0, keyB))
				return;

			if (this.reader.ReadBlock(0, out block0))
			{
				this.TxtDbg.Trace(String.Format("ReadlBlock0 = {0}", BitConverter.ToString(block0)), false);
			}
		}

		private void BntReset_Click(object sender, EventArgs e)
		{
			ResetCardBlock();
		}

		#endregion

		#region "--- event hanlder ---"
		private void CbAll_CheckedChanged(object sender, EventArgs e)
		{
			if (this.CbAll.Checked)
			{
				this.cardSectorSelected.ForEach(c => c.Checked = true);
			}
			else
			{
				this.cardSectorSelected.ForEach(c => c.Checked = false);
			}
		}

		private void CardForm_Activated(object sender, EventArgs e)
		{

		}

		private void CardForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.reader != null)
				this.reader.Close();
		}

		private void CbAuthenKey_SelectedIndexChanged(object sender, EventArgs e)
		{
			MessageBox.Show(CbAuthenKey.SelectedIndex.ToString());
		}

		#endregion

		private void BtnInitialize_Click(object sender, EventArgs e)
		{
			//byte[] card_id;
			//if( !reader.Select( out card_id))
			//{
			//	this.TxtDbg.Trace("no card found !", true);
			//	return;
			//}

			//this.TxtDbg.Trace("Select Card" + BitConverter.ToString(card_id));

			//ResultArgs result = CardManager.MifareInitialize(card_id);

			//if (result.Succ)
			//{
			//	CardKeyResponse[] response = result.Result as CardKeyResponse[];

			//	for (int i = 0; i < response.Length; i++)
			//	{
			//		this.TxtDbg.Trace(String.Format("CardId = {0}, Sector = {1},KeyA={2}, KeyB={3}, Control={4}",
			//			BitConverter.ToString(response[i].CardId),
			//			response[i].Sector,
			//			BitConverter.ToString(response[i].ReadKey),
			//			BitConverter.ToString(response[i].WriteKey),
			//			BitConverter.ToString(response[i].ControlBlock)));
			//	}
			//}
		}

		private void BtnWriteCardTest_Click(object sender, EventArgs e)
		{
			this.TxtDbg.Trace("Write sector Control block", true);

			byte[] keyA = HexString.FromHexString(this.TxtKeyA.Text.Trim(), "-");
			byte[] keyB = HexString.FromHexString(this.TxtKeyB.Text.Trim(), "-");

			M1KeyMode mode = (M1KeyMode)Enum.Parse(typeof(M1KeyMode), CbAuthenKey.Items[CbAuthenKey.SelectedIndex] as String, true);

			foreach (CheckBox cb in this.cardSectorSelected)
			{
				if (!cb.Checked)
					continue;

				int sector = int.Parse(cb.Text.Trim());
				if( this.reader.ChangeControlBlock(sector, mode, keyA, keyB) )
				{
					this.TxtDbg.Trace(String.Format("Write sector {0} block ok!", sector));
				}
			}
		}


	}
}
