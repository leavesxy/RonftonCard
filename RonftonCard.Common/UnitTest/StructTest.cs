using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Common.UnitTest
{
	[TestClass]
	public class StructTest
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct RSA_PUBLIC_KEY
		{
			public uint bits;                   // length in bits of modulus        	
			public uint modulus;                // modulus
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
			public byte[] exponent;             // public exponent
		}

		[TestMethod]
		public void Test1()
		{
			RSA_PUBLIC_KEY pub = new RSA_PUBLIC_KEY();
			pub.exponent = new byte[256];

			byte[] temp = new byte[10];
			Array.Copy(pub.exponent, temp, 10);
			Console.WriteLine(BitConverter.ToString(temp));
		}

	}
}
