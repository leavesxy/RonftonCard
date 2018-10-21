using BlueMoon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Common
{
	public class DataTypeHandler
	{
		public static byte[] ToByte(Object obj, CardDataType dataType)
		{
			byte[] ret;

			switch(dataType)
			{
				case CardDataType.BIN:
					if (obj.GetType().Equals(typeof(String)))
						ret = Encoding.Default.GetBytes(((String)obj));
					else
						ret = (byte[])obj;
					break;
				case CardDataType.STR:
					ret = WriteSTR((String)obj);
					break;
				case CardDataType.DATE_B:
					ret = WriteDateB((DateTime)obj);
					break;
				case CardDataType.CHAR:
					ret = WriteChar((char)obj);
					break;
				case CardDataType.BOOL:
					ret = WriteBool((bool)obj);
					break;
				case CardDataType.BYTE:
					ret = new byte[] { (byte)obj };
					break;
				case CardDataType.INT16:
					ret = BitConverter.GetBytes((Int16)obj);
					break;
				case CardDataType.INT32:
					ret = BitConverter.GetBytes((Int32)obj);
					break;
				case CardDataType.INT64:
					ret = BitConverter.GetBytes((Int64)obj);
					break;
				case CardDataType.BCD:
					ret = HexString.FromString((String)obj);
					break;
				default:
					ret = new byte[] { };
					break;
			}
			return ret;
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
			return HexString.FromString(ss.Substring(2));
		}
	}
}
