using System;
using System.Linq;


namespace RonftonCard.CardReader.Decard
{
	using Bluemoon;

	/// <summary>
	/// implements Device operation
	/// </summary>
	public partial class D8Reader
	{
		public bool Open()
		{
			if (this.hReader > 0)
			{
				// opened already
				return true;
			}

			this.hReader = dc_init((short)this.port, this.baud);
			if (this.hReader < 0)
			{
				return false;
			}

			Reset();
			char type = (char)this.cardType.GetAliasName().ElementAt(0);
			dc_config_card(this.hReader, type);

			// if init ok, beep to prompt
			Beep();
			return true;
		}

		public void Close()
		{
			if (this.hReader >0 )
			{
				dc_exit(this.hReader);
				this.hReader = -1;
			}
		}

		public void Reset()
		{
			if (this.hReader != -1)
			{
				dc_reset(this.hReader, 10);
			}
		}

		public void Beep(int times = 1, int duration = 10)
		{
			if (this.hReader != -1)
			{
				while (times-- > 0)
				{
					dc_beep(this.hReader, (ushort)duration);
				}
			}
		}

		public void Dispose()
		{
			Close();
		}

		public String GetVersion()
		{
			byte[] version = new byte[128];

			if (this.hReader == -1 || dc_getver(this.hReader, version) != SUCC)
				return String.Empty;

			return BitConverter.ToString( ByteUtil.TrimEnd(version) );
		}

		public void Light(bool flag)
		{
			if (this.hReader != -1)
				dc_light(this.hReader, (ushort)(flag ? 1 : 0));
		}
	}
}
