using RonftonCard.Common;
using RonftonCard.Common.Reader;
using RonftonCard.Tester.Entity;
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
	public partial class ConfigTestFrm : Form
	{
		public ConfigTestFrm()
		{
			InitializeComponent();
		}

		private void ConfigTestFrm_Load(object sender, EventArgs e)
		{

		}

		private void BtnDbgCardTemplete_Click(object sender, EventArgs e)
		{
			CardContext ctx = ContextManager.CreateContext();
			this.TxtDbg.Trace(ctx.ConfigTemplete.DbgTempleteDataDescriptor(), true);
			this.TxtDbg.Trace(ctx.ConfigTemplete.DbgTempleteStorageDescriptor());
		}

		private void BtnDbgCardEntity_Click(object sender, EventArgs e)
		{
			CardEntity entity = CardEntity.CreateTestEntity();
			this.TxtDbg.Trace("Data written to Card ...", true);
			this.TxtDbg.Trace("---------------------------------------------");
			this.TxtDbg.Trace(entity.ToString());
		}

		private void BtnWriteVirtualCard_Click(object sender, EventArgs e)
		{
			CardContext ctx = ContextManager.CreateContext();

			AbstractVirtualCard vc = new MifareVirtualCard(ctx);
			CardEntity entity = CardEntity.CreateTestEntity();
			vc.WriteEntity<CardEntity>(entity);
			this.TxtDbg.Trace(vc.DbgBuffer(),true);
		}

		private void BtnReaderInit_Click(object sender, EventArgs e)
		{
			CardContext ctx = ContextManager.CreateContext();
			this.TxtDbg.Trace(ctx.ReaderDescriptor.ToString(),true);

			using (ICardReader reader = ctx.GetCardReader())
			{
				if (reader != null)
				{
					this.TxtDbg.Trace("reader version = " + reader.GetVersion());
					reader.Beep(2);
					reader.Close();
				}
			}
		}
	}
}
