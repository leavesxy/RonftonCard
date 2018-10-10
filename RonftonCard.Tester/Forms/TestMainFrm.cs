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
		private List<TabPageDescriptor> pageDescriptor;

		public TestMainFrm(ResourceManager rm)
		{
			InitializeComponent();
			this.rm = rm;
		}

		private void TestMainFrm_Load(object sender, EventArgs e)
		{
			this.Text = this.rm.GetFormTextName();
			this.Icon = (Icon)this.rm.GetObject("TestMain");

			if( !TesterContext.Init(this) )
			{
				Application.Exit();
				return;
			}

			this.CbAddrTemplete.Items.AddRange(TesterContext.addrTemplete.GetTempleteName().ToArray());
			this.CbStruTemplete.Items.AddRange(TesterContext.struTemplete.GetTempleteName().ToArray());

			this.CbStruTemplete.SelectedIndex = 0;
			this.CbAddrTemplete.SelectedIndex = 0;

			InitTabPage();
		}
		
		private void InitTabPage()
		{
			this.pageDescriptor = new List<TabPageDescriptor>()
			{
				new TabPageDescriptor { PageIndex=0, PageName="Dbg", PageForm = new PageDbg() },
				new TabPageDescriptor { PageIndex=1, PageName="卡片测试", PageForm = new PageCardTester()},
			};

			// Ascending order
			this.pageDescriptor.Sort((d1, d2) => d1.PageIndex.CompareTo(d2.PageIndex));

			this.pageDescriptor.ForEach(
				desc => 
				{
					// set style of form
					desc.PageForm.TopLevel = false;
					desc.PageForm.FormBorderStyle = FormBorderStyle.None;
					desc.PageForm.Dock = DockStyle.Fill;
					desc.PageForm.AutoScroll = true;
					//desc.PageForm.Height = TabMain.Height - 200;
					//desc.PageForm.Width = TabMain.Width;

					// add new tab page
					TabPage tp = new TabPage();
					tp.Name = desc.PageName;
					tp.Text = desc.PageName;
					tp.Controls.Add(desc.PageForm);
					this.TabMain.Controls.Add(tp);
				});
			this.TabMain.SelectedIndex = 0;
			this.pageDescriptor.Find(desc => desc.PageIndex == 0).PageForm.Show();
		}

		#region "--- Event handle ---"
		private void TabMain_SelectedIndexChanged(object sender, EventArgs e)
		{
			TabPageDescriptor pageDesc = this.pageDescriptor.Find(d => d.PageIndex == this.TabMain.SelectedIndex);
			if (pageDesc != null)
			{
				pageDesc.PageForm.Show();
			}
		}

		#endregion

		#region "--- Button command ---"

		/// <summary>
		/// exit 
		/// </summary>
		private void BtnExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		#endregion

		public String CardAddrTempleteSelected
		{
			get { return this.CbAddrTemplete.SelectedItem as String; }
		}

		public String CardStruTempleteSelected
		{
			get { return this.CbStruTemplete.SelectedItem as String; }
		}
	}
}