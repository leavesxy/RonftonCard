using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Common.UnitTest
{
	[TestClass]
	public class Base64Test
	{
		[TestMethod]
		public void Test()
		{
			String plain = "01234567";
			Console.WriteLine("plain = " + plain);
			Console.WriteLine("Base64 : = " + Convert.ToBase64String(Encoding.Default.GetBytes(plain)));
		}
	}
}