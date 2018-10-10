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
			List<CardAddrItem> items = TesterContext.GetCardAddrTempleteSelected();

			this.TxtDbg.Text = "  Card Virtual Address, Total Size : " + Environment.NewLine;
			this.TxtDbg.Text += "------------------------------------------" + Environment.NewLine;

			foreach (CardAddrItem item in items)
				this.TxtDbg.Text += item.ToString();
		}
		private void BtnChkAddrTemplete_Click(object sender, EventArgs e)
		{
			CardAddrItem[] sortedAddrTable = TesterContext.GetCardAddrTempleteSelected().OrderBy(addr => addr.Offset).ToArray();
			int errors = 0;
			this.TxtDbg.Clear();

			for (int i = 1; i < sortedAddrTable.Length; i++)
			{
				if (sortedAddrTable[i - 1].Offset + sortedAddrTable[i - 1].Length != sortedAddrTable[i].Offset)
				{
					this.TxtDbg.Text += "Error : ->" + sortedAddrTable[i].ToString() + Environment.NewLine;
					errors++;
				}
			}

			this.TxtDbg.Text += String.Format("there are {0} errors in current configuration!", errors);
		}

		private void BtnDbgStruTemplete_Click(object sender, EventArgs e)
		{
			List<CardStruItem> items = TesterContext.GetCardDataTempleteSelected();

			this.TxtDbg.Text = "  Card data structure :" + Environment.NewLine;
			this.TxtDbg.Text += "------------------------------------------" + Environment.NewLine;

			foreach (CardStruItem item in items)
				this.TxtDbg.Text += item.ToString();
		}

		private void BtnChkStruTemplete_Click(object sender, EventArgs e)
		{
			VirtualCard vcard = new VirtualCard(TesterContext.GetCardAddrTempleteSelected());
			String errMsg;

			this.TxtDbg.Text = String.Format("Total Size = {0}", vcard.Size);

			if (vcard.CheckCardStru(TesterContext.GetCardDataTempleteSelected(), out errMsg))
				errMsg = "Current Card Stru is OK!";

			this.TxtDbg.Text += errMsg;
		}

		#endregion
	}
}
