
using RonftonCard.Common.Reader;
using System;
using System.Linq;
using System.Text;

namespace RonftonCard.CardReader
{
	public partial class D8Reader : AbstractCardReader
	{
		private const short RET_OK = 0;

		public D8Reader()
			: this((int)PortType.USB, 0)
		{
		}

		public D8Reader(int port, int baud)
		: base(port, baud)
		{
			Open();
		}

		#region "--- device operation ---"
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

		#endregion

		#region "--- Card operation ---"
		public override bool Authen(KeyMode keyMode, int descriptor, byte[] pwd)
		{
			byte mode = (keyMode == KeyMode.KeyA) ? (byte)0x00 : (byte)0x04;

			return dc_authentication_passaddr(this.hDev, mode, (byte)(descriptor & 0xff), pwd) == RET_OK;
		}

		public override bool Read(int descriptor, out byte[] data)
		{
			data = new byte[16];
			return dc_read(this.hDev, (byte)(descriptor & 0xff), data) == RET_OK;
		}

		public override bool Write(int descriptor, byte[] data)
		{
			return dc_write(this.hDev, (byte)(descriptor & 0xff), data) == RET_OK;
		}

		public override bool Select(out byte[] cardId)
		{
			uint cardIdLen = 0;
			cardId = Enumerable.Repeat((byte)0x00, 16).ToArray();

			if (dc_card_n(this.hDev, 0x00, ref cardIdLen, cardId) != RET_OK)
			{
				return false;
			}
			return true;
		}
		#endregion
	}
}
