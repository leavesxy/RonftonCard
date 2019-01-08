using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.DongleService
{
	public class HelperInfo
	{
		private static IDictionary<String, String> helperInfo = new Dictionary<String, String>()
		{
			{"http://localhost:9000/dongle                  ",  "[GET] : 枚举所有的加密锁" },
			{"http://localhost:9000/dongle/test             ",	"[GET] : 服务测试"},
			{"http://localhost:9000/dongle/open             ",  "[POST]: 打开指定的加密锁"},
			{"http://localhost:9000/dongle/userRoot/create  ",  "[POST]: 创建用户根密钥锁" },
			{"http://localhost:9000/dongle/userRoot/encrypt ",  "[POST]: -- 根密钥锁加密" },
			{"http://localhost:9000/dongle/userRoot/restore ",  "[POST]: -- 还原根密钥锁" },
			{"http://localhost:9000/dongle/authen/create    ",  "[POST]: 创建应用授权锁"},
			{"http://localhost:9000/dongle/authen/encrypt   ",  "[POST]: -- 应用授权锁加密"},
			{"http://localhost:9000/dongle/authen/restore   ",  "[POST]: -- 还原应用授权锁"},
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
			Console.WriteLine("1、首先通过GET方式获取所有的加密锁信息");
			Console.WriteLine("2、打开指定的加密锁，同时传递的序号为1中的顺序(新插入或拔出都会影响顺序)");
			Console.WriteLine("3、创建用户根密钥锁或者应用授权锁");
			Console.WriteLine("4、只有创建后，才能进行还原或者加密");
			Console.WriteLine("5、当打开选定的加密锁后，对应的锁会闪烁");
		}
	}
}