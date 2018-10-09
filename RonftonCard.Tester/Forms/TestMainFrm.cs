using System;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using BlueMoon.Form;
using RonftonCard.Common.Config;
using System.Collections.Generic;

namespace RonftonCard.Tester.Forms
{
	public partial class TestMainFrm : Form
	{
		private ResourceManager rm;

		public TestMainFrm(ResourceManager rm)
		{
			this.rm = rm;
			InitializeComponent();
		}

		private void TestMainFrm_Load(object sender, EventArgs e)
		{
			this.Text = this.rm.GetFormTextName();
			this.Icon = (Icon)this.rm.GetObject("TestMain");
			TesterContext.Init();

			this.CbAddrTemplete.Items.AddRange(TesterContext.addrTemplete.GetTempleteName().ToArray());
			this.CbStruTemplete.Items.AddRange(TesterContext.struTemplete.GetTempleteName().ToArray());

			this.CbStruTemplete.SelectedIndex = 0;
			this.CbAddrTemplete.SelectedIndex = 0;
		}

		#region "--- Button command ---"

		/// <summary>
		/// exit 
		/// </summary>
		private void BtnExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		/// <summary>
		/// clear TxtDbg
		/// </summary>
		private void BtnClear_Click(object sender, EventArgs e)
		{
			this.TxtDbg.Clear();
		}

		#endregion

		#region "--- Event handle ---"
		private void CbAddrTemplete_SelectedIndexChanged(object sender, EventArgs e)
		{
			String selected = (String)CbAddrTemplete.SelectedItem;
			List<CardAddrItem> items = TesterContext.addrTemplete.GetTempleteItem(selected);

			foreach (CardAddrItem item in items)
				this.TxtDbg.Text += item.ToString();
		}

		private void CbStruTemplete_SelectedIndexChanged(object sender, EventArgs e)
		{
			String selected = (String)CbStruTemplete.SelectedItem;
			List<CardStruItem> items = TesterContext.struTemplete.GetTempleteItem(selected);

			foreach (CardStruItem item in items)
				this.TxtDbg.Text += item.ToString();
		}

		#endregion
	}
}