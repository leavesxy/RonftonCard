using Bluemoon.WinForm;
using RonftonCard.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using System.IO;

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
				new TabPageDescriptor { PageIndex=2, PageName="授权KEY", TabPageForm = new DongleForm() },
			};
		}
		private void MainFrm_Load(object sender, EventArgs e)
		{
			this.Text = this.rm.GetFormTextName();
			this.Icon = (Icon)this.rm.GetObject("Main");

			InitTabPage();
			this.TabMainControl.SelectedIndex = 0;

			LoadConfiguration();
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

		private void LoadConfiguration()
		{
			if (!ConfigManager.Init())
			{
				MessageBox.Show("初始化配置错误！", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			this.CbCardTemplete.Items.AddRange(ConfigManager.TempleteNames);
			this.CbCardTemplete.SelectedIndex = 0;

			this.CbCardReader.Items.AddRange(ConfigManager.ReaderNames);
			this.CbCardReader.SelectedIndex = 0;

			this.CbDongle.Items.AddRange(ConfigManager.DongleNames);
			this.CbDongle.SelectedIndex = 0;
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
			ConfigManager.ReaderSelected = (String)CbCardReader.Items[CbCardReader.SelectedIndex];
		}

		private void CbCardTemplete_SelectedIndexChanged(object sender, EventArgs e)
		{
			ConfigManager.TempleteSelected = (String)CbCardTemplete.Items[CbCardTemplete.SelectedIndex];
		}

		private void CbDongle_SelectedIndexChanged(object sender, EventArgs e)
		{
			ConfigManager.DongleSelected = (String)CbDongle.Items[CbDongle.SelectedIndex];
		}

		#endregion

		#region "--- button click ---"

		/// <summary>
		/// reload configuration
		/// </summary>
		private void BtnRefresh_Click(object sender, EventArgs e)
		{
			LoadConfiguration();
		}

		/// <summary>
		/// Exit
		/// </summary>
		private void BtnExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
		#endregion

		const int WM_DEVICECHANGE = 0x2190;
		const int DBT_DEVICEARRIVAL = 0x8000;
		const int DBT_DEVICEREMOVECOMPLETE = 0x8004;
		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case DBT_DEVICEARRIVAL:  // U盘插入
					MessageBox.Show("U盘插入");
					//DriveInfo[] s = DriveInfo.GetDrives();
					//foreach (DriveInfo drive in s)
					//{
					//	if (drive.DriveType == DriveType.Removable)
					//	{
					//		MessageBox.Show("USB插入");
					//		break;
					//	}
					//}
					break;
				case DBT_DEVICEREMOVECOMPLETE: //U盘卸载
					MessageBox.Show("USB卸载");
					break;

				default:
					break;
			}

			base.WndProc(ref m);
		}
	}
}
