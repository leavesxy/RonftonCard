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
			{"http://localhost:9000/dongle                  ",	"[GET] : Enumerate all dongles" },
			{"http://localhost:9000/dongle/test             ",	"[GET] : Get test information"},
			{"http://localhost:9000/dongle/userRoot/create  ",	"[POST]: Create User root key" },
			{"http://localhost:9000/dongle/userRoot/encrypt ",  "[POST]: Encrypt by User root key " },
			{"http://localhost:9000/dongle/userRoot/restore ",  "[POST]: Erase User root key " },
			{"http://localhost:9000/dongle/authen/create    ",	"[POST]: Create Authen Key "},
			{"http://localhost:9000/dongle/authen/encrypt   ",	"[POST]: Encrypt by Private key "},
			{"http://localhost:9000/dongle/authen/restore   ",  "[POST]: Erase Authen Key "},
		};
		
		public static void DbgHelperInfo()
		{
			foreach( String key in helperInfo.Keys )
			{
				Console.WriteLine(key + " --> " + helperInfo[key]);
			}
		}
	}
}
