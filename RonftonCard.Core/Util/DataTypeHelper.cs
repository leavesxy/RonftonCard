using Bluemoon;
using System;
using System.Text;

namespace RonftonCard.Common.Util
{
	public class DataTypeHelper
	{
		public static byte[] ToByte(Object obj, CardDataType dataType)
		{
			byte[] result;

			switch(dataType)
			{
				case CardDataType.BIN:
					if (obj.GetType().Equals(typeof(String)))
						result = Encoding.Default.GetBytes(((String)obj));
					else
						result = (byte[])obj;
					break;
				case CardDataType.STR:
					result = WriteSTR((String)obj);
					break;
				case CardDataType.DATE_B:
					result = WriteDateB((DateTime)obj);
					break;
				case CardDataType.CHAR:
					result = WriteChar((char)obj);
					break;
				case CardDataType.BOOL:
					result = WriteBool((bool)obj);
					break;
				case CardDataType.BYTE:
					result = new byte[] { (byte)obj };
					break;
				case CardDataType.INT16:
					result = BitConverter.GetBytes((Int16)obj);
					break;
				case CardDataType.INT32:
					result = BitConverter.GetBytes((Int32)obj);
					break;
				case CardDataType.INT64:
					result = BitConverter.GetBytes((Int64)obj);
					break;
				case CardDataType.BCD:
					result = HexString.FromHexString((String)obj);
					break;
				default:
					result = new byte[] { };
					break;
			}
			return result;
		}

		private static byte[] WriteBool(bool ch)
		{
			return new byte[] { ch ? (byte)0x01:(byte)0x00 };
		}

		private static byte[] WriteChar(char ch)
		{
			return new byte[] { (byte)ch };
		}

		private static byte[] WriteSTR(String str)
		{
			return Encoding.Default.GetBytes(str);
		}

		private static byte[] WriteDateB(DateTime obj)
		{
			String ss = obj.ToString("yyyyMMdd");
			return HexString.FromHexString(ss.Substring(2));
		}
	}
}
