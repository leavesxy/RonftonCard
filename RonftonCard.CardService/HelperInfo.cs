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
			{"http://localhost:9001/reader/open         ",  "[GET ]: 打开读卡器"},
			{"http://localhost:9001/reader/info         ",  "[GET ]: 获取读卡器信息" },
			{"http://localhost:9001/reader/select       ",  "[GET ]: 寻卡" },
			{"http://localhost:9001/reader/readSector   ",  "[POST]: 读卡扇区信息" },
			{"http://localhost:9001/reader/readBlock    ",  "[POST]: 读卡块信息" },
			{"http://localhost:9001/reader/init         ",  "[POST]: 卡片初始化" },
			{"http://localhost:9001/reader/personalize  ",  "[POST]: 卡片个人化" },
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