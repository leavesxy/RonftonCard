using System;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using RonftonCard.Common;

using System.Collections.Generic;
using Bluemoon.WinForm;

namespace RonftonCard.Tester.Forms
{
	public partial class TestMainFrm : Form
	{
		private ResourceManager rm;
		private List<TabPageDescriptor> tabPageDescriptor;

		public TestMainFrm(ResourceManager rm)
		{
			InitializeComponent();
			this.rm = rm;
			this.tabPageDescriptor = new List<TabPageDescriptor>()
			{
				new TabPageDescriptor { PageIndex=0, PageName="配置测试", TabPageForm = new ConfigTestFrm()},
				new TabPageDescriptor { PageIndex=1, PageName="卡测试", TabPageForm = new MifareTestFrm() },
				new TabPageDescriptor { PageIndex=2, PageName="Key测试", TabPageForm = new KeyTestFrm() },

			};
		}

		private void TestMainFrm_Load(object sender, EventArgs e)
		{
			this.Text = this.rm.GetFormTextName();
			this.Icon = (Icon)this.rm.GetObject("TestMain");

			try
			{
				ContextManager.LoadCardConfigTemplete("CardTemplete.xml");
				ContextManager.LoadCardReaderConfiguration("CardReader.xml");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			this.CbCardTemplete.Items.AddRange(ContextManager.TempleteNames);
			this.CbCardTemplete.SelectedIndex = 0;

			this.CbCardReader.Items.AddRange(ContextManager.CardReaderNames);
			this.CbCardReader.SelectedIndex = 0;

			InitTabPage();
			this.TabMainControl.SelectedIndex = 0;
		}

		private void InitTabPage()
		{
			// Ascending order
			this.tabPageDescriptor.Sort((d1, d2) => d1.PageIndex.CompareTo(d2.PageIndex));

			this.tabPageDescriptor.ForEach(desc => {
				// set style of form
				desc.TabPageForm.TopLevel = false;
				desc.TabPageForm.FormBorderStyle = FormBorderStyle.None;
				desc.TabPageForm.Dock = DockStyle.Fill;

				// add new tab page
				TabPage tp = new TabPage();
				tp.Name = desc.PageName;
				tp.Text = desc.PageName;
				tp.Controls.Add(desc.TabPageForm);

				this.TabMainControl.Controls.Add(tp);
			});

			this.tabPageDescriptor.Find(desc => desc.PageIndex == 0).TabPageForm.Show();
		}

		/// <summary>
		/// exit 
		/// </summary>
		private void BtnExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}


		#region "--- Event handle ---"

		private void CbCardTemplete_SelectedIndexChanged(object sender, EventArgs e)
		{
			ContextManager.CurrentTempleteName = CbCardTemplete.SelectedItem as String;
		}

		private void CbCardReader_SelectedIndexChanged(object sender, EventArgs e)
		{
			ContextManager.CurrentReaderDescriptor = CbCardReader.SelectedItem as String;
		}

		private void TabMainControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			TabPageDescriptor desc = this.tabPageDescriptor.Find(d => d.PageIndex == this.TabMainControl.SelectedIndex);
			if (desc != null)
			{
				desc.TabPageForm.Show();
			}
		}

		#endregion

		private void CompanySeedKeyMgr_Click(object sender, EventArgs e)
		{
			CompanySeedKeyFrm frm = new CompanySeedKeyFrm();
			frm.ShowDialog();
		}
	}
}