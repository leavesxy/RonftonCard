using RonftonCard.Common;
using RonftonCard.Common.Reader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bluemoon;

namespace RonftonCard.Tester.Forms
{
	public partial class MifareTestFrm : Form
	{
		private List<CheckBox> mifareCardBlocks;

		public MifareTestFrm()
		{
			InitializeComponent();
		}

		private void MifareTestFrm_Load(object sender, EventArgs e)
		{
			ContextManager.CurrentCardType = EntityCardType.M1;
			this.TxtControlBlock.Text = "{1 0 0},{0 1 1}";
			this.mifareCardBlocks = new List<CheckBox>()
			{
				this.Cb0,this.Cb1,this.Cb2,this.Cb3,this.Cb4,this.Cb5,this.Cb6,this.Cb7,
				this.Cb8,this.Cb9,this.Cb10,this.Cb11,this.Cb12,this.Cb13,this.Cb14,this.Cb15,
			};
			ResetCardBlock();
		}



		#region "--- button command ---"
		/// <summary>
		/// select card Test
		/// </summary>
		private void BtnSelectCard_Click(object sender, EventArgs e)
		{
			this.TxtDbg.Trace("Begin to Select Card ...", true);
			CardContext ctx = ContextManager.CreateContext();

			using (ICardReader reader = ctx.GetCardReader())
			{
				if (reader != null)
				{
					byte[] cardId;
					if (reader.Select(out cardId))
					{
						this.TxtDbg.Trace("Select Card OK! id = " + BitConverter.ToString(cardId));
					}
					reader.Close();
				}
			}
		}

		/// <summary>
		/// read block with KeyA
		/// </summary>
		private void BtnReadBlockA_Click(object sender, EventArgs e)
		{
			byte[] keyA = HexString.FromHexString(this.TxtKeyA.Text.Trim());
			ReadBlock(KeyMode.KeyA, keyA);
		}

		/// <summary>
		/// read block with KeyB
		/// </summary>
		private void BtnReadBlockB_Click(object sender, EventArgs e)
		{
			byte[] keyB = HexString.FromHexString(this.TxtKeyB.Text.Trim());
			ReadBlock(KeyMode.KeyB, keyB);
		}

		private void BtnUpdateKeyA_Click(object sender, EventArgs e)
		{

		}

		private void BtnUpdateKeyB_Click(object sender, EventArgs e)
		{

		}

		private void BtnWriteBlock_Click(object sender, EventArgs e)
		{

		}

		private void BntReset_Click(object sender, EventArgs e)
		{
			ResetCardBlock();
		}

		#endregion

		#region "--- util --"

		/// <summary>
		/// reset all card block check-box
		/// </summary>
		private void ResetCardBlock()
		{
			int[] sectors = ContextManager.AddrDescriptors;

			this.mifareCardBlocks.ForEach(m =>
			{
				if (sectors.Contains(int.Parse(m.Text)))
					m.Checked = true;
			});
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public int[] GetCardBlockSelected()
		{
			Update();
			List<int> blockSelected = new List<int>();
			this.mifareCardBlocks.ForEach(
				a =>
				{
					if (a.Checked)
						blockSelected.Add(int.Parse(a.Text));
				});
			return blockSelected.ToArray();
		}

		private void ReadBlock(KeyMode keyMode, byte[] key)
		{
			CardContext ctx = ContextManager.CreateContext();
			this.TxtDbg.Clear();

			this.TxtDbg.Trace("ReadBlock with mode : {0}, Key = {1}",
				keyMode.ToString(),
				BitConverter.ToString(key));

			int[] blockSelected = GetCardBlockSelected();
			if( blockSelected.IsNullOrEmpty())
			{
				this.TxtDbg.Trace("No block selected !");
				return;
			}

			ICardReader reader = ctx.GetCardReader();
			if (reader == null)
				return;

			byte[] cardId;
			if (!reader.Select(out cardId))
			{
				this.TxtDbg.Trace("Select card Failed !");
				reader.Close();
				return;
			}

			this.TxtDbg.Trace("Select Card OK! id = " + BitConverter.ToString(cardId));

			for (int i=0; i<blockSelected.Length;i++ )
			{
				if (!reader.Authen(keyMode, i, key))
				{
					this.TxtDbg.Trace("Authen {0} block failed !", i);
					continue;
				}

				byte[] blockData;
				if (reader.Read(i, out blockData))
				{
					this.TxtDbg.Trace(String.Format("Read block #{0} -> {1}",
						i,
						BitConverter.ToString(blockData)));
				}
			}
			reader.Close();
		}
		#endregion

		#region "--- Event Handle ---"

		private void CbAll_CheckedChanged(object sender, EventArgs e)
		{
			if( this.CbAll.Checked )
			{
				this.mifareCardBlocks.ForEach(c => c.Checked = true);
			}
			else
			{
				this.mifareCardBlocks.ForEach(c => c.Checked = false);
			}
		}

		#endregion

	}
}