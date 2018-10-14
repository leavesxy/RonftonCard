
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
			Open();
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
			if (this.hDev != -1)
			{
				while (times > 0)
				{
					dc_beep(this.hDev, (ushort)duration);
					times--;
				}
			}
		}

		public override String GetVersion()
		{
			if (this.hDev != -1)
			{
				byte[] ver = new byte[128];
				if (dc_getver(this.hDev, ver) == 0)
				{
					//return BitConverter.ToString(ver);
					StringBuilder sb = new StringBuilder();

					for(int i=0;i<ver.Length;i++)
					{
						if( ver[i] != 0x00)
						{
							sb.Append(ver[i].ToString("x2"));
							if (Char.IsLetterOrDigit((char)ver[i]))
								sb.Append("(" + (char)ver[i] +") ");
						}
					}
					return sb.ToString();
				}
			}
			return "";
		}

		public override void Light(bool onOff)
		{
			dc_light(this.hDev, (ushort)(onOff ? 1 : 0));
		}

	}
}
