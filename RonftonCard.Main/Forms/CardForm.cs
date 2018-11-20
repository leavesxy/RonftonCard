using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RonftonCard.Core;

namespace RonftonCard.Main.Forms
{
	public partial class CardForm : Form
	{
		private List<CheckBox> cardSectorSelected;

		private const String DEFAULT_KEY_A = "01-01-01-01-01-01";
		private const String DEFAULT_KEY_B = "0f-0f-0f-0f-0f-0f";

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
		}

		private void ResetCardBlock()
		{
			int[] sectors = ConfigManager.GetCardTemplete().SegmentAddr;

			this.cardSectorSelected.ForEach(m =>
			{
				if (sectors.Contains(int.Parse(m.Text)))
					m.Checked = true;
			});
		}


		#region "--- button handler ---"
		private void BtnSelectCard_Click(object sender, EventArgs e)
		{

		}

		private void BtnReadBlockA_Click(object sender, EventArgs e)
		{

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


		#endregion


	}
}
