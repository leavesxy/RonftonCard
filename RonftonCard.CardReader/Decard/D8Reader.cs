using Bluemoon;
using RonftonCard.Core;
using RonftonCard.Core.CardReader;
using System;
using System.Linq;
using System.Text;

namespace RonftonCard.CardReader.Decard
{
	public partial class D8Reader : AbstractCardReader
	{
		private const short SUCC = 0;

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
			byte[] version = new byte[128];

			if (this.hDev == -1 || dc_getver(this.hDev, version) != SUCC)
				return String.Empty;

			return BitConverter.ToString(version);
		}

		public override void Light(bool onOff)
		{
			dc_light(this.hDev, (ushort)(onOff ? 1 : 0));
		}

		#endregion

		#region "--- Card operation ---"
		public override bool Authen(KeyMode keyMode, int descriptor, byte[] pwd)
		{
			byte mode = (keyMode == KeyMode.KEY_A) ? (byte)0x00 : (byte)0x04;

			return dc_authentication_passaddr(this.hDev, mode, (byte)(descriptor & 0xff), pwd) == SUCC;
		}

		public override bool Read(int descriptor, out byte[] data, int len)
		{
			data = new byte[len];
			return dc_read(this.hDev, (byte)(descriptor & 0xff), data) == SUCC;
		}

		public override bool Write(int descriptor, byte[] data, int len)
		{
			return dc_write(this.hDev, (byte)(descriptor & 0xff), data) == SUCC;
		}

		public override bool Select(out byte[] cardId)
		{
			uint cardIdLen = 0;
			cardId = ByteUtil.Malloc(16);

			if (dc_card_n(this.hDev, 0x00, ref cardIdLen, cardId) != SUCC)
			{
				return false;
			}
			return true;
		}
		#endregion
	}
}
