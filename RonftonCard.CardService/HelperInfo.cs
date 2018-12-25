using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.CardService
{
	public class HelperInfo
	{
		private static IDictionary<String, String> helperInfo = new Dictionary<String, String>()
		{
			{"http://localhost:9001/reader/open   ",  "[GET]: 打开读卡器"},
			{"http://localhost:9001/reader/info   ",  "[GET]: 获取读卡器信息" },
			{"http://localhost:9001/reader/select ",  "[GET]: 寻卡" },
			{"http://localhost:9001/reader/read/id",  "[GET]: 读卡信息,id代表扇区号" },
		};
		
		public static void DbgHelperInfo()
		{
			Console.WriteLine("localhost service list below:");
			Console.WriteLine("---------------------------------------------------------------------");
			foreach( String key in helperInfo.Keys )
			{
				Console.WriteLine(key + " --> " + helperInfo[key]);
			}

			DbgReadme();
		}

		public static void DbgReadme()
		{
			Console.WriteLine("---------------------------------------------------------------------");
		}
	}
}