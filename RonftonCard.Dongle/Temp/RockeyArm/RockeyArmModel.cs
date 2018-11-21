using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluemoon;

namespace RonftonCard.Dongle.Temp.RockeyArm
{
	/// <summary>
	/// model of rockey arm dongle
	/// </summary>
	public enum RockeyArmModel : byte
	{
		[Alias("Stardard Locker")]
		STANDARD = 0xff,

		[Alias("Timer Locker")]
		TIMER = 0x00,

		[Alias("Usb Disk Locker")]
		UDISK=0x02,

		[Alias("Unknown model")]
		UNKNOWN = 0xee
	}
}