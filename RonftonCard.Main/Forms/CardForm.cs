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

	public partial class CardForm : Form
	{
		private List<CheckBox> cardSectorSelected;

		private const String DEFAULT_KEY_A = "01-01-01-01-01-01";
		private const String DEFAULT_KEY_B = "0f-0f-0f-0f-0f-0f";

		private ICardReader reader;

		public CardForm()
		{
			InitializeComponent();
		}

		private void CardForm_Load(object sender, EventArgs e)
		{
			this.TxtControlBlock.Text = "{1 0 0},{0 1 1}";
			this.TxtKeyA.Text = DEFAULT_KEY_A;
			this.TxtKeyB.Text = DEFAULT_KEY_B;

			this.cardSectorSelected = new List<CheckBox>()
			{
				this.Cb0,this.Cb1,this.Cb2,this.Cb3,this.Cb4,this.Cb5,this.Cb6,this.Cb7,
				this.Cb8,this.Cb9,this.Cb10,this.Cb11,this.Cb12,this.Cb13,this.Cb14,this.Cb15,
			};
			ResetCardBlock();

			this.reader = ConfigManager.GetCardReader();
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

			if( this.reader.Select(out card_id))
			{
				this.TxtDbg.Trace("Select card id =" + BitConverter.ToString(card_id));
			}
		}

		private void BtnReadBlockA_Click(object sender, EventArgs e)
		{
			byte[] buffer;
			if (this.reader.Read(0, out buffer, 16))
			{
				this.TxtDbg.Trace("read byte =" + BitConverter.ToString(buffer));
			}
		}

		private void BtnReadBlockB_Click(object sender, EventArgs e)
		{

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

		#endregion

		private void BtnInitialize_Click(object sender, EventArgs e)
		{
			byte[] card_id;
			if( !reader.Select( out card_id))
			{
				this.TxtDbg.Trace("no card found !", true);
				return;
			}

			this.TxtDbg.Trace("Select Card" + BitConverter.ToString(card_id));

			ResultArgs result = CardManager.Initialize(card_id);

			if (result.Succ)
			{
				CardKeyResponse[] response = result.Result as CardKeyResponse[];

				for (int i = 0; i < response.Length; i++)
				{
					this.TxtDbg.Trace(String.Format("CardId = {0}, Sector = {1},KeyA={2}, KeyB={3}, Control={4}",
						BitConverter.ToString(response[i].CardId),
						response[i].Sector,
						BitConverter.ToString(response[i].ReadKey),
						BitConverter.ToString(response[i].WriteKey),
						BitConverter.ToString(response[i].ControlBlock)));
				}
			}
		}
	}
}
