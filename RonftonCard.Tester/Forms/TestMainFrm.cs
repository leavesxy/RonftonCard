using System;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using BlueMoon.Form;
using System.Collections.Generic;
using RonftonCard.Common;
using RonftonCard.Tester.Entity;
using System.Text.RegularExpressions;
using RonftonCard.Common.Utils;
using RonftonCard.Common.Reader;
using System.Reflection;

namespace RonftonCard.Tester.Forms
{
	public partial class TestMainFrm : Form
	{
		private ResourceManager rm;

		public TestMainFrm(ResourceManager rm)
		{
			InitializeComponent();
			this.rm = rm;
		}

		private void TestMainFrm_Load(object sender, EventArgs e)
		{
			this.Text = this.rm.GetFormTextName();
			this.Icon = (Icon)this.rm.GetObject("TestMain");

			try
			{
				RFTCardContext.LoadCardTemplete("CardTemplete.xml");
				RFTCardContext.LoadCardReader("CardReader.xml");
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return ;
			}

			this.CbCardTemplete.Items.AddRange(RFTCardContext.GetCardTempleteNames());
			this.CbCardTemplete.SelectedIndex = 0;

			this.CbCardReader.Items.AddRange(RFTCardContext.GetCardReaderNames());
			this.CbCardReader.SelectedIndex = 0;

			this.TxtControlBlock.Text = "{1 0 0}, {0 1 1}";
			Update();
		}
		
		#region "--- Event handle ---"

		private void CbCardTemplete_SelectedIndexChanged(object sender, EventArgs e)
		{
			BtnDbgCardTemplete_Click(this, null);
		}

		private void CbCardReader_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		#endregion

		/// <summary>
		/// exit 
		/// </summary>
		private void BtnExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		#region "--- Config Test ---"
		
		private void BtnDbgCardTemplete_Click(object sender, EventArgs e)
		{
			String cardTempleteName = CbCardTemplete.SelectedItem as String;
			Dbg(RFTCardContext.GetCardTemplete(cardTempleteName).DbgStorageItems(), true);
			Dbg(RFTCardContext.GetCardTemplete(cardTempleteName).DbgDataItems());
		}

		private void BtnDbgCardEntity_Click(object sender, EventArgs e)
		{
			CardEntity entity = CardEntity.CreateTestEntity();
			Dbg("Data written to Card ...",true);
			Dbg("---------------------------------------------");
			Dbg(entity.ToString());
		}

		private void BtnWriteVirtualCard_Click(object sender, EventArgs e)
		{
			String cardTempleteName = CbCardTemplete.SelectedItem as String;
			M1VirtualCard mvc = new M1VirtualCard(this.TxtControlBlock.Text, RFTCardContext.GetCardTemplete(cardTempleteName), null);
			Dbg("User data control String : " + this.TxtControlBlock.Text, true);
			Dbg("Key control block    : " + mvc.DbgControlBlock());
			Dbg("size of virtual card : " + mvc.CardTemplete.CardSize.ToString());
			Dbg("physical address : " + mvc.DbgSectors());
		}
		#endregion


		#region "--- Card reader Test ---"
		private void BtnReaderInit_Click(object sender, EventArgs e)
		{
			String readerName = this.CbCardReader.SelectedItem as String;
			Dbg("Current activated CardReader : " + readerName, true);
			Dbg(RFTCardContext.GetCardReaderDesc(readerName));

			using (ICardReader reader = RFTCardContext.GetCardReader(readerName))
			{
				if (reader != null)
				{
					Dbg( "reader version = " + reader.GetVersion());
					reader.Beep(2);
					reader.Close();
				}
			}

			// show load assemblies
			DbgAllAssemblies();
		}

		private void DbgAllAssemblies()
		{
			Assembly[] assembies = AppDomain.CurrentDomain.GetAssemblies();
			Dbg("Current assemblies loaded : ");

			foreach(Assembly assembly in assembies)
			{
				// filter mircosoft, system
				String name = assembly.GetName().Name;
				if (!name.StartsWith("System", StringComparison.CurrentCultureIgnoreCase) &&
					!name.StartsWith("Microsoft", StringComparison.CurrentCultureIgnoreCase))
				{
					Dbg(" --> name = {0}", name);
				}
			}
		}

		private void BtnReaderBeep_Click(object sender, EventArgs e)
		{

		}
		#endregion

		private void Dbg(String msg, bool isClear=false)
		{
			if (isClear)
				this.TxtDbg.Clear();

			this.TxtDbg.Text += msg + Environment.NewLine;
		}

		private void Dbg(String format, params Object[] args )
		{
			this.TxtDbg.Text += String.Format(format, args) + Environment.NewLine;
		}

		private void DbgCr()
		{
			this.TxtDbg.Text += Environment.NewLine;
		}

		private void Dbg(byte[] msg, bool isClear=false)
		{
			if (isClear)
				this.TxtDbg.Clear();

			this.TxtDbg.Text += BitConverter.ToString(msg) + Environment.NewLine;
		}

	}
}