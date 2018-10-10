using RonftonCard.Common;
using RonftonCard.Common.Config;
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
	public partial class PageDbg : Form
	{
		public PageDbg()
		{
			InitializeComponent();
		}

		private void PageDbg_Load(object sender, EventArgs e)
		{
		}
		
		#region "--- button command ---"

		private void BtnDbgAddrTemplete_Click(object sender, EventArgs e)
		{
			this.TxtDbg.Clear();
			CardAddrItem[] sortedAddrTable = TesterContext.GetCardAddrTempleteSelected().OrderBy(addr => addr.Offset).ToArray();
			StringBuilder errMsg = new StringBuilder();

			int size=0;
			for (int i = 0; i < sortedAddrTable.Length; i++)
			{
				this.TxtDbg.Text += sortedAddrTable[i].ToString();
				size += sortedAddrTable[i].Length;

				if ( i>0 && 
					(sortedAddrTable[i - 1].Offset + sortedAddrTable[i - 1].Length != sortedAddrTable[i].Offset))
				{
					errMsg.Append ("Address conflict! ->").Append(sortedAddrTable[i].ToString());
				}
			}
			this.TxtDbg.Text += "------------------------------------------------------------------------------------" + Environment.NewLine;
			this.TxtDbg.Text += "Card Virtual Address, Total Size : " + size.ToString() + Environment.NewLine;
			this.TxtDbg.Text += errMsg.ToString();
		}

		private void BtnDbgStruTemplete_Click(object sender, EventArgs e)
		{
			CardStruItem[] sortedStruItem = TesterContext.GetCardDataTempleteSelected().OrderBy(item => item.Offset).ToArray();

			this.TxtDbg.Clear();
			this.TxtDbg.Text  = "  Card data structure :" + Environment.NewLine;
			this.TxtDbg.Text += "------------------------------------------------------------------------------------" + Environment.NewLine;

			StringBuilder errMsg = new StringBuilder();
			int size = 0;

			for (int i = 0; i < sortedStruItem.Length; i++)
			{
				this.TxtDbg.Text += sortedStruItem[i].ToString();
				size += sortedStruItem[i].Length;
				if ( i >0 &&
					(sortedStruItem[i].Offset < sortedStruItem[i-1].Offset + sortedStruItem[i - 1].Length) )
				{
					errMsg.Append("Address conflict! ->")
						.Append(sortedStruItem[i].Name)
						.Append(", Offset=").Append(sortedStruItem[i].Offset.ToString("x4"))
						.Append(Environment.NewLine);
				}
			}
			this.TxtDbg.Text += "------------------------------------------------------------------------------------" + Environment.NewLine;
			this.TxtDbg.Text += "Card Structure total Size : " + size.ToString() + Environment.NewLine;

			this.TxtDbg.Text += errMsg.ToString();
		}

		private void BtnChkStruTemplete_Click(object sender, EventArgs e)
		{
			VirtualCard vcard = new VirtualCard(TesterContext.GetCardAddrTempleteSelected());
			String errMsg;

			this.TxtDbg.Text = String.Format("Total Size = {0}{1}", vcard.Size, Environment.NewLine);

			if (vcard.CheckCardStru(TesterContext.GetCardDataTempleteSelected(), out errMsg))
				errMsg = "Current Card Stru is OK!";

			this.TxtDbg.Text += errMsg;
		}

		#endregion
	}
}
