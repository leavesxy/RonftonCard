using BlueMoon;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Common.UnitTest
{
	[TestClass]
	public class RsaTest
	{
		[TestMethod]
		public void PubDecrypt()
		{
			byte[] cipher = Convert.FromBase64String(@"gkYjfD9dCWyPaM2tvUMEej+wd7xR+bXLhjAa96A2u2dHHK4r8+Vu8wAr3R8vjSKJNUsi9P/fXdlqvJtajd+Yd+4O9eyb0pPbtWncfmyWdRPz6sbXBHWXlSO8LcnATEnH2T+qE/ELy2tqd8hdh85HWyvyp+ZHTXwKYT3mz86vt2Y=");

			try
			{
				RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

				byte[] bytes_Public_Key = Convert.FromBase64String(@"yyBvi7wh+/QMZ1WIsgewSrCmDl9EBap5jO+0JZRhD1v0xbzWp1FqzdQiUEEUBG6t8J59XmV3wQH3uCnBKRAyzFdBCmrcJJHNdqJCeXI3ol1hEiJhnFc+tfH/2uWN6FEYYq4z+0p6TPSstiQmG4mEqEuBupdwT0tvv5+6yJoPAmUAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAgAAAAAKADAA==");
				rsa.ImportCspBlob(bytes_Public_Key);

				//OAEP padding is only available on Microsoft Windows XP or later. 
				byte[] plain = rsa.Decrypt(cipher, false);

				Console.Out.WriteLine("plain byte = " + BitConverter.ToString(plain));
				Console.Out.WriteLine("plain String = " + Encoding.Default.GetString(plain));
			}
			catch (CryptographicException e)
			{
				Console.WriteLine(e.ToString());
			}
		}

		[TestMethod]
		public void Test2()
		{
			String publickey = @"<RSAKeyValue><Modulus>uXli7kiAS2M3OMPHJN2ExUJceko+Zk3lcZYrJBs3j8aXlp+J2olG9TTSsm98hyZ0g2+20c4M/mL67Ysd9JAR9+UaRLwmGO2PcBsw1MWxDKp4vClgFHtdQP9LwrQj/8nhPXElkNBNNcvSwpcvheCR72dOWo5iyo4P8kXxsE+7UDel16CrUppR2DK4Csq5C0UKF4IkNvegFfoo9dMMvP80kO7zmIwW5hqhRRGPft9fRRxPtFdSnbx1u3HtXif4uj1UJsTFHZkp9Bx/ZOsNiAZPRsfjIU6ijjbUVj4oFXFpKwoejPRf9VGjHMeSKV4c/KcYf6EPXs+NGIaM06RF63fhbQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
			//String publickey = @"<RSAKeyValue><Modulus>rmxra4yAdvkkTHmBZS+9c9C3rij4YEiZCIIObjbcVehj2qIKnG+ZRe0E6J2+rvFohA32Awa0DCt2VTuIh3TFEUh7ecEiIGbUDxL3EjztcQ2vSKrMwhgh2zHiSMbHrRl5zxtTBbe1a4HCVjUFja/qYYdhBXnuPCAH2HeQSCzmPzk=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
			byte[] plainByte = Convert.FromBase64String(@"EK9xirdSvVGPsuHS2Je38Slbwnse3BxivZlWZ1K7SgXZEh+YLB+A7wxIB9PpQkZSWW3GsBsOmn4ieNy+cO5ECHIei7awyn8RfmFWrNfSCwt3MonDpxsHZfPJXoMANv1zQb+wB6DWvsHZrOAq4JWhzbPObgziAENedZTn6STeni4NKhU7aAihhQDwcvKyPrZcQv4MKYmkO7zyxnC1MjAiUytcJN5ZzkCLvw4/7Nygf8V+WWwmi+sDjyRtlw1GVh5UvMS9yjDHuHpawa8TChnAvNFxSPAswAksVl01DXT/gAutBv4+OFTLef4Kh9nbEulNiDTao09163CZGZX/XtPKxA==");

			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);
			byte[] cipherbytes;
			rsa.FromXmlString(publickey);
			cipherbytes = rsa.Decrypt(plainByte, false);

			Console.WriteLine("Hex    = " + BitConverter.ToString(cipherbytes));
			Console.WriteLine("String = " + Encoding.Default.GetString(cipherbytes));
		}

		[TestMethod]
		public void Test3()
		{
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);

			String plain = "hubin";

			byte[] cipher = rsa.Encrypt(Encoding.Default.GetBytes(plain), false);
			Console.WriteLine("cipher = " + Convert.ToBase64String(cipher));
			String xmlPublicKey = rsa.ToXmlString(true);
			Console.WriteLine("public key = " + xmlPublicKey);

			//byte[] plainBytes = rsa.Decrypt(cipher, false);
			//Console.WriteLine("plain = " + Encoding.Default.GetString(plainBytes));

			//RSACryptoServiceProvider rsa2 = new RSACryptoServiceProvider(2048);
			//rsa2.FromXmlString(xmlPublicKey);
			//byte[] cipher2 = rsa2.Decrypt(cipher, false);
			//Console.WriteLine("cipher2 = " + Convert.ToBase64String(cipher2));
		}

		//private string DecryptProcess(string source, string e, string n)
		//{
		//	byte[] N = Convert.FromBase64String(n);
		//	byte[] E = Convert.FromBase64String(e);
		//	BigInteger biN = new BigInteger(N);
		//	BigInteger biE = new BigInteger(E);
		//	return DecryptString(source, biE, biN);
		//}


		//private string DecryptString(string source, BigInteger e, BigInteger n)
		//{
		//	int len = source.Length;
		//	int len1 = 0;
		//	int blockLen = 0;
		//	if ((len % 256) == 0)
		//		len1 = len / 256;
		//	else
		//		len1 = len / 256 + 1;
		//	string block = "";
		//	string temp = "";
		//	for (int i = 0; i < len1; i++)
		//	{
		//		if (len >= 256)
		//			blockLen = 256;
		//		else
		//			blockLen = len;
		//		block = source.Substring(i * 256, blockLen);
		//		BigInteger biText = new BigInteger(block, 16);
		//		BigInteger biEnText = biText.modPow(e, n);
		//		string temp1 = System.Text.Encoding.Default.GetString(biEnText.getBytes());
		//		temp += temp1;
		//		len -= blockLen;
		//	}
		//	return temp;
		//}
	}
}
