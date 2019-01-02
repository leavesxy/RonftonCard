using System;
using System.Collections.Generic;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace RonftonCard.Main.Forms
{
	using Bluemoon.Config;
	using Bluemoon.WinForm;
	using Bluemoon.WinForm.Toolbox;
	using Core;

	public partial class MainFrm : Form
	{
		private ResourceManager rm;
		private List<TabPageDescriptor> tabPageDescriptor;

		#region "--- init ---"
		public MainFrm(ResourceManager rm)
		{
			InitializeComponent();
			this.rm = rm;
			this.tabPageDescriptor = new List<TabPageDescriptor>()
			{
				new TabPageDescriptor { PageIndex=0, PageName="Config", TabPageForm = new ConfigForm()},
				new TabPageDescriptor { PageIndex=1, PageName="Card", TabPageForm = new CardForm() },
				new TabPageDescriptor { PageIndex=2, PageName="Dongle", TabPageForm = new DongleForm() },
				new TabPageDescriptor { PageIndex=3, PageName="Test", TabPageForm = new TestForm() },
			};
		}

		private void MainFrm_Load(object sender, EventArgs e)
		{
			this.Text = this.rm.GetFormTextName();
			this.Icon = (Icon)this.rm.GetObject("Main");

			InitTabPage();
			this.TabMainControl.SelectedIndex = 0;
			InitConfiguration();

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

		private const String configFileName = @"etc\RonftonConfig.xml";
		private const String ReaderSectionPath = "reader";
		private const String DongleSectionPath = "dongle";
		private const String CardSectionPath = "card";

		private void InitConfiguration()
		{
			IEntityResolver resolver = new XmlDefaultResolver(configFileName);

			this.ReaderModel.BindDisplayItem(resolver,ReaderSectionPath);
			this.DongleModel.BindDisplayItem(resolver,DongleSectionPath);
			this.CardType.BindDisplayItem(resolver,CardSectionPath);

			ContextManager.Init();
		}

		#endregion

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
			//ConfigManager.ReaderSelected = (String)CardReader.Items[CardReader.SelectedIndex];
		}

		private void CbCardTemplete_SelectedIndexChanged(object sender, EventArgs e)
		{
			//ConfigManager.TempleteSelected = (String)CardTemplete.Items[CardTemplete.SelectedIndex];
		}

		private void CbDongle_SelectedIndexChanged(object sender, EventArgs e)
		{
			//ConfigManager.DongleSelected = (String)DongleModel.Items[DongleModel.SelectedIndex];
		}

		private void CbCardType_SelectedIndexChanged(object sender, EventArgs e)
		{
			//ConfigManager.CardType = (CardType)Enum.Parse(typeof(CardType), (String)CbCardType.Items[CbCardType.SelectedIndex], true);
		}

		#endregion

		#region "--- button click ---"

		/// <summary>
		/// reload configuration
		/// </summary>
		private void BtnRefresh_Click(object sender, EventArgs e)
		{
			//LoadConfiguration();
		}

		/// <summary>
		/// Exit
		/// </summary>
		private void BtnExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}
		#endregion


		#region "--- intercept USB event ---"
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

		#endregion

		private void BtnInitConfig_Click(object sender, EventArgs e)
		{
			ContextManager.ReaderSelected = ((ComboBoxBindingItem)this.ReaderModel.SelectedItem).Name;
			ContextManager.DongleSelected = ((ComboBoxBindingItem)this.DongleModel.SelectedItem).Name;
			ContextManager.CardTypeSelected = ((ComboBoxBindingItem)this.CardType.SelectedItem).Name;
		}
	}
}
