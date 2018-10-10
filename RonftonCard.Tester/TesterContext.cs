using RonftonCard.Common.Config;
using RonftonCard.Tester.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RonftonCard.Tester
{
	public class TesterContext
	{
		private const String STRU_TEMPLETE_FILE = "CardStru.xml";
		private const String ADDR_TEMPLETE_FILE = "CardAddr.xml";

		public static TestMainFrm mainForm { get; private set;}
		public static CardAddrTemplete addrTemplete { get; private set; }
		public static CardStruTemplete struTemplete { get; private set; }

		public static bool Init(TestMainFrm mainFrm)
		{
			try
			{
				addrTemplete = new CardAddrTemplete(ADDR_TEMPLETE_FILE);
				struTemplete = new CardStruTemplete(STRU_TEMPLETE_FILE);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			mainForm = mainFrm;
			return true;
		}

		public static List<CardAddrItem> GetCardAddrTempleteSelected()
		{
			String selected = mainForm.CardAddrTempleteSelected;
			return addrTemplete.GetTempleteItem(selected);
		}

		public static List<CardStruItem> GetCardDataTempleteSelected()
		{
			String selected = mainForm.CardStruTempleteSelected;
			return struTemplete.GetTempleteItem(selected);
		}
	}
}