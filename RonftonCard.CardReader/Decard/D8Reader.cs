using Bluemoon;
using RonftonCard.Core;
using RonftonCard.Core.CardReader;
using System;
using System.Linq;
using System.Text;

namespace RonftonCard.CardReader.Decard
{
	using DEV_HANDLER = System.Int32;

	public partial class D8Reader : ICardReader
	{
		private const short SUCC = 0;
		protected DEV_HANDLER hReader;
		protected int port;
		protected int baud;

		///Com: 0~99,USB:100~199,PCSC:200~299,Bluetooth:300~399
		public D8Reader()
			: this((int)PortType.USB, 0)
		{
		}

		public D8Reader(int port, int baud)
		{
			this.port = port;
			this.baud = baud;
			this.hReader = -1;
		}

		#region "--- device operation ---"

		public bool Open()
		{
			this.hReader = dc_init((short)this.port, this.baud);
			if (this.hReader < 0)
			{
				return false;
			}

			// if init ok, beep to prompt
			Beep();
			return true;
		}

		public void Close()
		{
			if (this.hReader != -1)
			{
				dc_exit(this.hReader);
				this.hReader = -1;
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

			return BitConverter.ToString(version);
		}

		public void Light(bool onOff)
		{
			dc_light(this.hReader, (ushort)(onOff ? 1 : 0));
		}

		#endregion


		#region "--- Card operation ---"

		public bool Select(out byte[] cardId)
		{
			uint cardIdLen = 0;
			byte[] buffer = ByteUtil.Malloc(16);

			if (dc_card_n(this.hReader, 0x00, ref cardIdLen, buffer) != SUCC)
			{
				cardId = null;
				return false;
			}

			cardId = ArrayUtil.CopyFrom(buffer, (int)cardIdLen);
			return true;
		}

		public bool Authen(KeyMode keyMode, int descriptor, byte[] pwd)
		{
			byte mode = (keyMode == KeyMode.KEY_A) ? (byte)0x00 : (byte)0x04;

			return dc_authentication_passaddr(this.hReader, mode, (byte)(descriptor & 0xff), pwd) == SUCC;
		}

		/// <summary>
		/// M1_S50: 16 sectors,  64 blocks
		/// M1_S70: 40 sectors, 256 blocks
		/// in the first 32 sectors, 16 data blocks per sector. 16 blocks per sector in the last 8 sectors
		/// </summary>
		private const int M1_BLOCK_LEN = 16;
		private const int M1_SECTOR_SPLIT = 32;
		private const int M1_MAX_SECTOR = 39;
		private const int M1_MAX_BLOCK = 255;

		public bool ReadBlock(int block, out byte[] outData)
		{
			outData = new byte[M1_BLOCK_LEN];
			return dc_read(this.hReader, (byte)(block & 0xff), outData) == SUCC;
		}

		public bool ReadSector(int sector, out byte[] outData, out int len)
		{
			if( sector > M1_MAX_SECTOR )
			{
				outData = null;
				len = 0;
				return false;
			}

			// compute block number for this sector
			int blockNum = (sector < 32) ? 4 : 16;
			len = M1_BLOCK_LEN * blockNum;
			outData = new byte[len];

			// compute start block sequence number
			int startBlock = (sector < 32) ? sector * 4 : 32 * 4 + (sector - 32) * 16;

			byte[] buffer;
			for (int i = 0; i < blockNum; i++)
			{
				if (ReadBlock(startBlock + i, out buffer))
				{
					Array.Copy(buffer, 0, outData, i * 16, M1_BLOCK_LEN);
				}
				else
					return false;
			}
			return true;
		}

		public bool WriteBlock(int block, byte[] inData)
		{
			// block 0 can't be written
			if (block <= 0 || block > M1_MAX_BLOCK )
				return false;

			return dc_write(this.hReader, (byte)(block & 0xff), inData) == SUCC;
		}

		public bool WriteSector(int sector, byte[] inData, int len)
		{
			return true;
		}

		#endregion
	}
}
