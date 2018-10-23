using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RonftonCard.Tester
{
	/// <summary>
	/// extent text box to debug
	/// </summary>
	public static class TextBoxExtension
	{

		public static void Trace(this TextBox @this, String msg, bool isClear = false)
		{
			if (isClear)
				@this.Clear();

			@this.Text += msg + Environment.NewLine;
		}

		public static void Trace(this TextBox @this, String format, params Object[] args)
		{
			@this.Text += String.Format(format, args) + Environment.NewLine;
		}

		public static void NewLine(this TextBox @this)
		{
			@this.Text += Environment.NewLine;
		}

		public static void Trace(this TextBox @this, byte[] msg, bool isClear = false)
		{
			if (isClear)
				@this.Clear();

			@this.Text += BitConverter.ToString(msg) + Environment.NewLine;
		}
	}
}
