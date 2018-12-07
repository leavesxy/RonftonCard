using System;
using System.Windows.Forms;
using RonftonCard.Core;
using RonftonCard.Core.Config;

namespace RonftonCard.Main.Forms
{
	using Bluemoon.WinForm;

	public partial class ConfigForm : Form
	{
		public ConfigForm()
		{
			InitializeComponent();
		}

		private void ConfigForm_Load(object sender, EventArgs e)
		{

		}

		private void BtnCardTemplete_Click(object sender, EventArgs e)
		{
			CardTempleteDescriptor desc = ConfigManager.GetCardTemplete();

			this.TxtTrace.Trace("Card Storage information :", true);
			this.TxtTrace.Line(50);
			this.TxtTrace.Trace( desc.GetStorageInfo());
			this.TxtTrace.Line(50);
			this.TxtTrace.Trace("Card data information :");
			this.TxtTrace.Line(50);
			this.TxtTrace.Trace(desc.GetDataItemInfo());
		}

		private void BtnCardDataTest_Click(object sender, EventArgs e)
		{

		}

		private void BtnVirtualCardTest_Click(object sender, EventArgs e)
		{

		}
	}
}
