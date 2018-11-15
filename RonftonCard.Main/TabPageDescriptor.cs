using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RonftonCard.Main
{
	public class TabPageDescriptor
	{
		/// <summary>
		/// index of tab page
		/// </summary>
		public int PageIndex { get; set; }

		/// <summary>
		/// text of tab page
		/// </summary>
		public String PageName { get;set; }

		/// <summary>
		/// form which load to current page
		/// </summary>
		public Form TabPageForm { get;set; }

	}
}
