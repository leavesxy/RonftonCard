using BlueMoon;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Common.UnitTest
{
	[TestClass]
	public class PwdTest
	{
		[TestMethod]
		public void ToAscii()
		{
			byte[] pid = StringConverterManager.ConvertTo<byte[]>("0x39,0x42,0x36,0x46,0x39,0x44,0x42,0x31,0x00");
			byte[] adminPwd = StringConverterManager.ConvertTo<byte[]>("0x43,0x37,0x37,0x33,0x30,0x38,0x39,0x39,0x30,0x31,0x34,0x39,0x38,0x41,0x46,0x36,0x00");
			Console.Out.WriteLine("pid = " + Encoding.ASCII.GetString(ArrayHelper.TrimEnd(pid)));
			Console.Out.WriteLine("adminPwd = " + Encoding.ASCII.GetString(ArrayHelper.TrimEnd(adminPwd)));
		}
	}
}
