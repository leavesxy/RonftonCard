using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Service
{
	public class HelperInfo
	{
		private static IDictionary<String, String> helperInfo = new Dictionary<String, String>()
		{
			{"http://localhost:9000/dongle                  ",	"[GET] : 枚举所有的加密狗" },
			{"http://localhost:9000/dongle/test             ",	"[GET] : 服务测试"},
			{"http://localhost:9000/dongle/userRoot/create  ",	"[POST]: 创建用户根密钥卡" },
			{"http://localhost:9000/dongle/userRoot/encrypt ",  "[POST]: -- 根密钥卡加密" },
			{"http://localhost:9000/dongle/userRoot/restore ",  "[POST]: -- 还原根密钥卡" },
			{"http://localhost:9000/dongle/authen/create    ",	"[POST]: 创建应用授权卡"},
			{"http://localhost:9000/dongle/authen/encrypt   ",	"[POST]: -- 应用授权卡加密"},
			{"http://localhost:9000/dongle/authen/restore   ",  "[POST]: -- 还原应用授权卡"},
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
			Console.WriteLine("操作步骤说明：");
			Console.WriteLine("1、只有创建用户根密钥后，才能进行还原或者加密");
			Console.WriteLine("2、只有创建授权密钥卡后，才能进行还原或者加密");
			Console.WriteLine("3、如果有多个加密狗，则在操作时，需要指定其序号，这个序号在枚举时获得");
		}
	}
}