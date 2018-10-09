
using RonftonCard.Common.Reader;
using System;
using System.Text;

namespace RonftonCard.CardReader
{
	public partial class D8Reader : AbstractCardReader
	{
		public D8Reader()
			: this((int)PortType.USB, 0)
		{
		}

		public D8Reader(int port, int baud)
		: base(port, baud)
		{
		}


		protected override void Open()
		{
			this.hDev = dc_init((short)this.port, this.baud);
			if (this.hDev < 0)
			{
				throw new Exception("Can't open Device !");
			}

			// if init ok, beep to prompt
			Beep();
		}

		public override void Close()
		{
			if (this.hDev != -1)
			{
				dc_exit(this.hDev);
				this.hDev = -1;
			}
		}

		public override void Beep(int times = 1, int duration = 10)
		{
			while(times>0)
			{
				dc_beep(this.hDev, (ushort)duration);
				times--;
			}
		}

		public override String GetVersion()
		{
			byte[] ver = new byte[128];
			if (dc_getver(this.hDev, ver) == 0)
			{
				return Encoding.Default.GetString(ver);
			}
			return "";
		}

		public override void Light(bool onOff)
		{
			dc_light(this.hDev, (ushort)(onOff ? 1 : 0));
		}

	}
}
