using System;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;
using BlueMoon.Form;
using System.Collections.Generic;
using RonftonCard.Common;
using RonftonCard.Tester.Entity;

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

		private const String CARD_TEMPLETE_FILE = "CardTemplete.xml";
		private void TestMainFrm_Load(object sender, EventArgs e)
		{
			this.Text = this.rm.GetFormTextName();
			this.Icon = (Icon)this.rm.GetObject("TestMain");

			try
			{
				if (!CardTempleteManager.LoadCardTemplete(CARD_TEMPLETE_FILE))
				{
					Application.Exit();
					return;
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
				return ;
			}

			this.CbCardTemplete.Items.AddRange( CardTempleteManager.GetTempleteNames().ToArray());
			this.CbCardTemplete.SelectedIndex = 0;
			Update();
		}
		
		#region "--- Event handle ---"

		private void CbCardTemplete_SelectedIndexChanged(object sender, EventArgs e)
		{

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

		private void BtnDbgCardTemplete_Click(object sender, EventArgs e)
		{
			String cardTempleteName = CbCardTemplete.SelectedItem as String;

			this.TxtDbg.Clear();
			this.TxtDbg.Text = CardTempleteManager.GetCardTemplete(cardTempleteName).DbgStorageItems();
			this.TxtDbg.Text += Environment.NewLine;
			this.TxtDbg.Text += CardTempleteManager.GetCardTemplete(cardTempleteName).DbgDataItems();
		}

		private void BtnDbgCardEntity_Click(object sender, EventArgs e)
		{
			CardEntity entity = CardEntity.CreateTestEntity();
			this.TxtDbg.Text = "写卡测试数据..." + Environment.NewLine;
			this.TxtDbg.Text += "---------------------------------------------" + Environment.NewLine;
			this.TxtDbg.Text += entity.ToString();
		}

		private void BtnWriteVirtualCard_Click(object sender, EventArgs e)
		{

		}

		#endregion


	}
}