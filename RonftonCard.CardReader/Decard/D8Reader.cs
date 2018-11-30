using Bluemoon;
using RonftonCard.Core;
using RonftonCard.Core.CardReader;
using System;
using System.Linq;
using System.Text;

namespace RonftonCard.CardReader.Decard
{
	using log4net;
	using DEV_HANDLER = System.Int32;

	public partial class D8Reader : ICardReader
	{
		private static ILog logger = LogManager.GetLogger("RonftonCardLog");

		private const short SUCC = 0;
		protected DEV_HANDLER hReader;
		protected int port;
		protected int baud;

		///Com: 0~99,USB:100~199,PCSC:200~299,Bluetooth:300~399
		public D8Reader()
			: this((int)ReaderPortType.USB, 0)
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

		public void Reset()
		{
			if(this.hReader  != -1 )
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

			return BitConverter.ToString(version);
		}

		public void Light(bool flag)
		{
			if( this.hReader != -1 )
				dc_light(this.hReader, (ushort)(flag ? 1 : 0));
		}

		#endregion


		#region "--- Card operation ---"

		/// <summary>
		/// atqa : Answer To Request, Type A
		///	sak : Select Acknowledge
		/// </summary>
		public bool Select2(out byte[] cardId)
		{
			cardId = null;

			if (this.hReader == -1)
				return false;

			ushort atqa = 0;
			if (dc_request(this.hReader, 0x00, ref atqa) != SUCC)
				return false;

			byte[] __cardId = ByteUtil.Malloc(16);
			if (dc_anticoll(this.hReader, 0x00, __cardId) != SUCC)
				return false;

			//M1: sak = 0x08
			//CPU simulate M1: sak=0x28
			byte sak = 0;
			if (dc_select(this.hReader, __cardId, ref sak) != SUCC)
				return false;

			cardId = new byte[__cardId.Length];
			Array.Copy(__cardId, 0, cardId, 0, __cardId.Length);
			return true;
		}


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

		public bool Authen(M1KeyMode keyMode, int descriptor, byte[] pwd)
		{
			byte mode = (keyMode == M1KeyMode.KEY_A) ? (byte)0x00 : (byte)0x04;
			return dc_authentication_passaddr(this.hReader, mode, (byte)(descriptor & 0xff), pwd) == SUCC;
		}

		/// <summary>
		/// M1_S50: 16 sectors,  64 blocks
		/// M1_S70: 40 sectors, 256 blocks
		/// in the first 32 sectors, 4 data blocks per sector. 
		/// 16 blocks per sector in the last 8 sectors
		/// </summary>
		private const int M1_BLOCK_LEN = 16;
		private const int M1_SECTOR_SPLIT = 32;
		private const int M1_MAX_SECTOR = 40;
		private const int M1_MAX_BLOCK = 256;

		private bool IsValidSector(int sector)
		{
			return sector >= 0 && sector < M1_MAX_SECTOR;
		}
		private bool IsValidBlock(int block)
		{
			return block >= 0 && block < M1_MAX_BLOCK;
		}

		public bool ReadBlock(int block, out byte[] outData)
		{
			outData = new byte[M1_BLOCK_LEN];

			if (!IsValidBlock(block))
				return false;
			short ret = dc_read(this.hReader, (byte)(block & 0xff), outData);
			logger.Debug("Read Block " + block.ToString() + " : " + BitConverter.ToString(outData) );

			return ret == SUCC;
		}

		public bool ReadSector(int sector, out byte[] outData, out int len)
		{
			if (!IsValidSector(sector))
			{
				outData = null;
				len = 0;
				return false;
			}

			// compute block number for this sector
			int blockNum = (sector < M1_SECTOR_SPLIT) ? 4 : 16;
			len = M1_BLOCK_LEN * blockNum;
			outData = new byte[len];

			// compute start block sequence number
			int startBlock = (sector < M1_SECTOR_SPLIT)
				? sector * 4 
				: M1_SECTOR_SPLIT * 4 + (sector - M1_SECTOR_SPLIT) * 16;

			logger.Debug(String.Format("Read Sector {0}, and from Block {1}", sector, startBlock));

			byte[] buffer;
			for (int i = 0; i < blockNum; i++)
			{
				if (ReadBlock(startBlock + i, out buffer))
				{
					Array.Copy(buffer, 0, outData, i * M1_BLOCK_LEN, M1_BLOCK_LEN);
				}
				else
				{
					logger.Debug(String.Format("Read Block {0} Failed", startBlock+i));
					return false;
				}
			}

			return true;
		}

		public bool WriteBlock(int block, byte[] inData)
		{
			if (!IsValidBlock(block))
				return false;

			short ret = dc_write(this.hReader, (byte)(block & 0xff), inData);
			
			return ret == SUCC;
		}

		public bool WriteSector(int sector, byte[] inData, int len)
		{
			if (!IsValidSector(sector) || len <=0 || len % M1_BLOCK_LEN != 0 )
				return false;

			// compute block number for this sector
			int blockNum = (sector < M1_SECTOR_SPLIT) ? 4 : 16;
			len = M1_BLOCK_LEN * blockNum;

			// compute start block sequence number
			int startBlock = (sector < M1_SECTOR_SPLIT)
				? sector * 4
				: M1_SECTOR_SPLIT * 4 + (sector - M1_SECTOR_SPLIT) * 16;

			byte[] buffer = ByteUtil.Malloc(16);

			for (int i = 0; i < blockNum; i++)
			{
				Array.Copy(inData, i * M1_BLOCK_LEN, buffer, 0, M1_BLOCK_LEN);
				if (!WriteBlock(startBlock + i, buffer))
				{
					logger.Debug(String.Format("Write block {0} Failed !", startBlock+i));
					return false;
				}
			}

			return true;
		}

		#endregion
	}
}
