using Bluemoon.WinForm;
using RonftonCard.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace RonftonCard.Main.Forms
{
	public partial class MainFrm : Form
	{
		private ResourceManager rm;
		private List<TabPageDescriptor> tabPageDescriptor;

		public MainFrm(ResourceManager rm)
		{
			InitializeComponent();
			this.rm = rm;
			this.tabPageDescriptor = new List<TabPageDescriptor>()
			{
				new TabPageDescriptor { PageIndex=0, PageName="配置", TabPageForm = new ConfigForm()},
				new TabPageDescriptor { PageIndex=1, PageName="IC卡", TabPageForm = new CardForm() },
				new TabPageDescriptor { PageIndex=2, PageName="授权KEY", TabPageForm = new AuthenKeyForm() },
			};
		}
		private void MainFrm_Load(object sender, EventArgs e)
		{
			this.Text = this.rm.GetFormTextName();
			this.Icon = (Icon)this.rm.GetObject("Main");

			try
			{
				ContextManager.LoadCardConfigTemplete("CardTemplete.xml");
				ContextManager.LoadCardReaderConfiguration("CardReader.xml");
				ContextManager.LoadKeyConfiguration("AuthenKeyModel.xml");
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

		private void BtnExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		#region "--- event handler ---"

		private void TabMainControl_SelectedIndexChanged(object sender, EventArgs e)
		{
			TabPageDescriptor desc = this.tabPageDescriptor.Find(d => d.PageIndex == this.TabMainControl.SelectedIndex);
			if (desc != null)
			{
				desc.TabPageForm.Show();
			}
		}

		private void CbCardReader_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void CbCardTemplete_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		#endregion


	}
}
