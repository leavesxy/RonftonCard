using System;
using Bluemoon;

namespace RonftonCard.UnitTest
{
	public class DbgUtil
	{
		public static void Dbg(byte[] buffer, int line = -1)
		{
			if (buffer.IsNullOrEmpty())
				return;

			for (int i = 0; i < buffer.Length; i++)
			{
				if (i != 0 && line > 0 && i % line == 0)
					Console.WriteLine();

				if (line < 0 || i % line != 0)
					Console.Write(" ");
				 
				Console.Write(buffer[i].ToString("X2"));
			}
			Console.WriteLine();
		}
	}
}