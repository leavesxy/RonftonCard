using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.CardReader
{
	public enum CardDataType
	{
		STR,	//String
		CHAR,	//char
		BOOL,	//boolean
		BYTE,	//byte
		INT16,	//short
		INT32,	//int
		INT64,	//long
		UINT16,	//ushort
		UINT32,	//uint
		UINT64,	//ulong
		BCD,	//HexString
		BIN,	//byte[]
		DATE_B,	//yyMMdd, base on 2000
	}
}