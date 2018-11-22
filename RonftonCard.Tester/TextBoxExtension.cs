using System;
using System.Linq;
using System.Windows.Forms;

namespace RonftonCard.Tester
{
	/// <summary>
	/// extent text box to debug
	/// </summary>
	public static class TextBoxExtension
	{
		public static void Trace(this TextBox @this, String msg, bool clear = false)
		{
			if (clear)
				@this.Clear();

			@this.Text += msg;
			@this.Cr();
		}

		public static void Trace(this TextBox @this, String format, params Object[] args)
		{
			@this.Text += String.Format(format, args);
			@this.Cr();
		}

		public static void Trace(this TextBox @this, byte[] msg, bool clear = false)
		{
			if (clear)
				@this.Clear();

			@this.Text += BitConverter.ToString(msg);
			@this.Cr();
		}

		public static void Cr(this TextBox @this)
		{
			@this.Text += Environment.NewLine;
		}

		public static void Line(this TextBox @this, int num, bool clear = false)
		{
			if (clear)
				@this.Clear();

			String line = new String(Enumerable.Repeat('-', num).ToArray());
			@this.Text += line;
			@this.Cr();
		}
	}
}