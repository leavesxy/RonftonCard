﻿using System;
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

		#region "--- form Initialize ---"
		public CardForm()
		{
			InitializeComponent();
		}

		private void CardForm_Load(object sender, EventArgs e)
		{
			this.KeyA.Text = DEFAULT_KEY_A;
			this.KeyB.Text = DEFAULT_KEY_B;
			this.BlockNo.Text = "0";
			this.AuthenKeyType.Items.AddRange(AUTHEN_KEY_MODE);
			this.AuthenKeyType.SelectedIndex = 1;

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

		#endregion

		#region "--- KEY authen test ---"

		/// <summary>
		/// Verify KEY_A
		/// </summary>
		private void BtnTestKeyA_Click(object sender, EventArgs e)
		{
			byte[] keyA = HexString.FromHexString(this.KeyA.Text.Trim(), "-");

			this.Dbg.Trace("Test KeyA : " + BitConverter.ToString(keyA), true);
			TestKey(M1KeyMode.KEY_A, keyA);
		}

		/// <summary>
		/// verify KEY_B
		/// </summary>
		private void BtnTestKeyB_Click(object sender, EventArgs e)
		{
			byte[] keyB = HexString.FromHexString(this.KeyB.Text.Trim(), "-");

			this.Dbg.Trace("Test KeyB : " + BitConverter.ToString(keyB), true);
			TestKey(M1KeyMode.KEY_B, keyB);
		}

		private void TestKey(M1KeyMode keyMode, byte[] key)
		{
			ResultArgs ret = this.reader.Select();
			if (!ret.Succ)
			{
				this.Dbg.Trace("Select Card Error!");
				return;
			}

			CardSelectInfo info = (CardSelectInfo)ret.Result;
			this.Dbg.Trace( String.Format("Select card_id={0}, ATQA=0x{1}, SAK={2}",
					BitConverter.ToString(info.SN),
					info.ATQA.ToString("X4"),
					info.SAK.ToString("X2")));

			foreach (CheckBox cb in this.cardSectorSelected)
			{
				if (!cb.Checked)
					continue;

				int sector = int.Parse(cb.Text.Trim());
				if (!this.reader.Authen(keyMode, sector * 4, key))
					this.Dbg.Trace("Authen sector {0} failed !", sector);
				else
					this.Dbg.Trace("Authen sector {0} OK !", sector);
			}
		}
		#endregion

		#region "--- Read sector test ---"
		/// <summary>
		/// Read Sector with KEY_A
		/// </summary>
		private void BtnReadSectorA_Click(object sender, EventArgs e)
		{
			this.Dbg.Trace("Read Card Sector With Key_A ..." + this.KeyA.Text.Trim(), true);
			byte[] keyA = HexString.FromHexString(this.KeyA.Text.Trim(), "-");
			ReadSector(M1KeyMode.KEY_A, keyA);
		}

		/// <summary>
		/// Read Sector with KEY_B
		/// </summary>
		private void BtnReadSectorB_Click(object sender, EventArgs e)
		{
			this.Dbg.Trace("Read Card by KeyB..." + this.KeyB.Text.Trim(), true);
			byte[] keyB = HexString.FromHexString(this.KeyB.Text.Trim(), "-");
			ReadSector(M1KeyMode.KEY_B, keyB);
		}

		private void ReadSector(M1KeyMode keyMode, byte[] key)
		{
			ResultArgs ret = this.reader.Select();
			if (!ret.Succ)
				this.Dbg.Trace("Select Card Error!");

			CardSelectInfo info = (CardSelectInfo)ret.Result;
			this.Dbg.Trace(String.Format("Select card_id={0}, ATQA=0x{1}, SAK={2}",
					BitConverter.ToString(info.SN),
					info.ATQA.ToString("X4"),
					info.SAK.ToString("X2")));

			foreach (CheckBox cb in this.cardSectorSelected)
			{
				if (!cb.Checked)
					continue;

				int sector = int.Parse(cb.Text.Trim());

				if (!this.reader.Authen(keyMode, sector * 4, key))
				{
					this.Dbg.Trace("Auth sector {0} failed !", sector);
					return;
				}

				byte[] buffer;
				int len = 0;
				if (this.reader.ReadSector(sector, out buffer, out len))
				{
					this.Dbg.Trace("Sector {0}", sector);
					this.Dbg.Trace(BitConverter.ToString(buffer, 0, 16));
					this.Dbg.Trace(BitConverter.ToString(buffer, 16, 16));
					this.Dbg.Trace(BitConverter.ToString(buffer, 32, 16));
					this.Dbg.Trace(BitConverter.ToString(buffer, 48, 16));
				}
			}
		}

		#endregion

		#region "--- reset card sector ---"
		private void BntReset_Click(object sender, EventArgs e)
		{
			ResetCardBlock();
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

		/// <summary>
		/// close reader before form close
		/// </summary>
		private void CardForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.reader != null)
				this.reader.Close();
		}

		private void CbAuthenKey_SelectedIndexChanged(object sender, EventArgs e)
		{
		}

		#endregion

		#region "--- test function ---"

		/// <summary>
		/// select test
		/// only return card_id
		/// </summary>
		private void BtnSelectCard_Click(object sender, EventArgs e)
		{
			byte[] card_id;

			if (this.reader.Select(out card_id))
			{
				this.Dbg.Trace("Select card id =" + BitConverter.ToString(card_id), true);
			}
		}

		/// <summary>
		/// select test 2
		/// return card_id, atqa, sak
		/// </summary>
		private void BtnSelectCard2_Click(object sender, EventArgs e)
		{
			byte[] cardId;
			UInt16 atqa;
			byte sak;

			if (this.reader.Select2(out cardId, out atqa, out sak))
			{
				this.Dbg.Trace("Select2 ", true);
				this.Dbg.Trace(String.Format("card_id={0}, atqa={1}, sak={2}",
						BitConverter.ToString(cardId),
						atqa.ToString("X4"),
						sak));
			}
		}

		/// <summary>
		/// test control block
		/// </summary>
		private void BtnTest3_Click(object sender, EventArgs e)
		{
			this.Dbg.Trace("Write sector Control block", true);

			byte[] keyA = HexString.FromHexString(this.KeyA.Text.Trim(), "-");
			byte[] keyB = HexString.FromHexString(this.KeyB.Text.Trim(), "-");

			M1KeyMode mode = (M1KeyMode)Enum.Parse(typeof(M1KeyMode), AuthenKeyType.Items[AuthenKeyType.SelectedIndex] as String, true);

			foreach (CheckBox cb in this.cardSectorSelected)
			{
				if (!cb.Checked)
					continue;

				int sector = int.Parse(cb.Text.Trim());
				if (this.reader.ChangeControlBlock(sector, mode, keyA, keyB))
				{
					this.Dbg.Trace(String.Format("Write sector {0} block ok!", sector));
				}
			}
		}

		/// <summary>
		/// show test data for writting card
		/// </summary>
		private void BtnTest4_Click(object sender, EventArgs e)
		{

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


	}
}
