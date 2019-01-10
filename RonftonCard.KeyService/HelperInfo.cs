using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.KeyService
{
	public class HelperInfo
	{
		private static IDictionary<String, String> helperInfo = new Dictionary<String, String>()
		{
			{"http://localhost:9002/m1               ", "[GET] : 初始化M1卡" },
			{"http://localhost:9002/cpu              ", "[GET] : 初始化CPU卡" },
			{"http://localhost:9002/test             ",	"[GET] : 服务测试"},
		};
		
		public static void DbgHelperInfo()
		{
			Console.WriteLine("localhost service list below:");
			Console.WriteLine("---------------------------------------------------------------------");
			foreach( String key in helperInfo.Keys )
			{
				Console.WriteLine(key + " --> " + helperInfo[key]);
			}
		}
	}
}