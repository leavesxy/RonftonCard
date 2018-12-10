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

			this.TraceMsg.Trace("Card Storage information :", true);
			this.TraceMsg.Line(50);
			this.TraceMsg.Trace( desc.GetStorageInfo());
			this.TraceMsg.Line(50);
			this.TraceMsg.Trace("Card data information :");
			this.TraceMsg.Line(50);
			this.TraceMsg.Trace(desc.GetDataItemInfo());
		}

		private void BtnCardDataTest_Click(object sender, EventArgs e)
		{

		}

		private void BtnVirtualCardTest_Click(object sender, EventArgs e)
		{

		}
	}
}
