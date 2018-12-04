using Bluemoon;
using RonftonCard.Core;
using RonftonCard.Core.CardReader;
using System;
using System.Linq;

namespace RonftonCard.CardReader.Decard
{
	using Core.DTO;
	using log4net;
	using DEV_HANDLER = System.Int32;

	public partial class D8Reader : ICardReader
	{
		private static ILog logger = LogManager.GetLogger("RonftonCardLog");

		private const short SUCC = 0;
		protected DEV_HANDLER hReader;
		protected int port;
		protected int baud;
		protected CardType cardType;

		///Com: 0~99,USB:100~199,PCSC:200~299,Bluetooth:300~399
		public D8Reader()
			: this((int)ReaderPortType.USB, 0)
		{
		}

		public D8Reader(int port, int baud)
			: this(port,baud, CardType.TYPE_A)
		{
		}

		public D8Reader(int port, int baud, CardType cardType)
		{
			this.port = port;
			this.baud = baud;
			this.hReader = -1;
			this.cardType = cardType;
		}

		#region "--- device operation ---"

		public bool Open()
		{
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
			//if (this.hReader != -1)
			//{
			//	while (times-- > 0)
			//	{
			//		dc_beep(this.hReader, (ushort)duration);
			//	}
			//}
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
		public bool Select(out byte[] cardId)
		{
			uint cardIdLen = 0;
			byte[] buffer = ByteUtil.Malloc(16);

			//Reset();
			if (dc_card_n(this.hReader, 0x00, ref cardIdLen, buffer) != SUCC)
			{
				cardId = null;
				return false;
			}
			cardId = ArrayUtil.CopyFrom(buffer, (int)cardIdLen);
			return true;
		}

		/// <summary>
		/// low level invoke,include request,anticoll,select
		/// </summary>
		/// <returns></returns>
		public bool Select2(out byte[] cardId,  out UInt16 atqa,  out byte sak)
		{
			atqa = 0;
			cardId = null;
			sak = 0x00;

			if (this.hReader == -1)
				return false;

			//atqa :: M1_S50: 0x0004; M1_S70: 0x0002
			if (dc_request(this.hReader, 0x00, ref atqa) != SUCC)
				return false;

			logger.Debug(String.Format("dc_request atqa = 0x{0}", atqa.ToString("X4")));

			byte[] __cardId = ByteUtil.Malloc(16);
			if (dc_anticoll(this.hReader, 0x00, __cardId) != SUCC)
				return false;

			// remove zero at the end
			cardId = ByteUtil.TrimEnd(__cardId);

			//M1: sak = 0x08
			//CPU simulate M1: sak=0x28
			if (dc_select(this.hReader, __cardId, ref sak) != SUCC)
				return false;
			
			logger.Debug(String.Format("dc_select sak = 0x{0}", sak.ToString("X2")));

			return true;
		}

		public ResultArgs Select()
		{
			ResultArgs ret = new ResultArgs(false);
			CardInfo cardInfo = new CardInfo();

			if (this.hReader == -1)
				return ret;

			//atqa :: M1_S50: 0x0004; M1_S70: 0x0002
			UInt16 atqa = 0;
			if (dc_request(this.hReader, 0x00, ref atqa) != SUCC)
			{
				ret.Msg = "Can't request atqa !";
				return ret;
			}

			cardInfo.Atqa = atqa;

			byte[] cardId = ByteUtil.Malloc(16);
			if (dc_anticoll(this.hReader, 0x00, cardId) != SUCC)
			{
				ret.Msg = "AntiCollision Failed !";
				return ret;
			}

			// remove zero at the end
			cardInfo.CardId = ByteUtil.TrimEnd(cardId);

			//M1: sak = 0x08
			//CPU simulate M1: sak=0x28
			byte sak = 0x00;
			if (dc_select(this.hReader, cardId, ref sak) != SUCC)
			{
				ret.Msg = "Can't get SAK!";
				return ret;
			}
			cardInfo.Sak = sak;
			ret.Succ = true;
			ret.Result = cardInfo;
			return ret;
		}

		/// <summary>
		/// block logic number
		/// </summary>
		public bool Authen(M1KeyMode keyMode, int blockNo, byte[] pwd)
		{
			byte mode = (keyMode == M1KeyMode.KEY_A) ? (byte)0x00 : (byte)0x04;
			return dc_authentication_passaddr(this.hReader, mode, (byte)(blockNo & 0xff), pwd) == SUCC;
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

		public bool ReadBlock(int blockNo, out byte[] outData)
		{
			outData = new byte[M1_BLOCK_LEN];

			if (!IsValidBlock(blockNo))
				return false;

			short ret = dc_read(this.hReader, (byte)(blockNo & 0xff), outData);
			logger.Debug("Read Block " + blockNo.ToString() + " : " + BitConverter.ToString(outData) );

			return ret == SUCC;
		}

		public bool WriteBlock(int blockNo, byte[] inData)
		{
			if (!IsValidBlock(blockNo))
				return false;

			return dc_write(this.hReader, (byte)(blockNo & 0xff), inData) == SUCC;
		}

		private int ComputeBlockNum(int sector)
		{
			return (sector < M1_SECTOR_SPLIT) ? 4 : 16;
		}

		private int ComputeStartBlockNo(int sector)
		{
			return (sector < M1_SECTOR_SPLIT) 
				? sector * 4
				: M1_SECTOR_SPLIT * 4 + (sector - M1_SECTOR_SPLIT) * 16;
		}

		public bool ReadSector(int sector, out byte[] outData, out int len)
		{
			outData = null;
			len = 0;

			if (!IsValidSector(sector))
				return false;

			// compute block number for this sector
			int blockNum = ComputeBlockNum(sector);
			int startBlockNo = ComputeStartBlockNo(sector);
			outData = new byte[M1_BLOCK_LEN * blockNum];

			logger.Debug(String.Format("Read Sector {0}, and from Block {1}", sector, startBlockNo));

			byte[] buffer;
			for (int i = 0; i < blockNum; i++)
			{
				if (ReadBlock(startBlockNo + i, out buffer))
				{
					Array.Copy(buffer, 0, outData, i * M1_BLOCK_LEN, M1_BLOCK_LEN);
				}
				else
				{
					logger.Debug(String.Format("Read Block {0} Failed", startBlockNo + i));
					return false;
				}
			}

			return true;
		}

		public bool WriteSector(int sector, byte[] inData, int len)
		{
			if (!IsValidSector(sector) || len <=0 || len % M1_BLOCK_LEN != 0 )
				return false;

			// compute block number for this sector
			int blockNum = ComputeBlockNum(sector);
			int startBlockNo = ComputeStartBlockNo(sector);
			//len = M1_BLOCK_LEN * blockNum;
			byte[] buffer = ByteUtil.Malloc(16);

			for (int i = 0; i < blockNum; i++)
			{
				Array.Copy(inData, i * M1_BLOCK_LEN, buffer, 0, M1_BLOCK_LEN);
				if (!WriteBlock(startBlockNo + i, buffer))
				{
					logger.Debug(String.Format("Write block {0} Failed !", startBlockNo + i));
					return false;
				}
			}
			return true;
		}


		private static byte[] controlBlock = new byte[] { 0x78, 0x77, 0x88, 0x69 };

		/// <summary>
		/// change control block of sector
		/// </summary>
		public bool ChangeControlBlock(int sector, M1KeyMode keyMode, byte[] keyA, byte[] keyB)
		{
			if (!IsValidSector(sector))
				return false;

			byte[] key = (keyMode == M1KeyMode.KEY_A) ? keyA : keyB;
			int startBlockNo = ComputeStartBlockNo(sector);
			//int blockNum = ComputeBlockNum(sector);

			if (!Authen(keyMode, startBlockNo, key))
			{
				logger.Debug("Can't authen with KeyMode=" + keyMode.ToString());
				return false;
			}

			return dc_changeb3(this.hReader, (byte)(sector & 0xff), keyA, 0x04, 0x04, 0x04, 0x03, 0x00, keyB)==SUCC;

			//byte[] block3;
			//if( !ReadBlock(startBlockNo + blockNum - 1, out block3))
			//{
			//	logger.Debug(String.Format("Read Sector {0}, block3 Failed !", sector));
			//	return false;
			//}

			//Array.Copy(keyA, 0, block3, 0, 6);
			//Array.Copy(controlBlock, 0, block3, 6, 4);
			//Array.Copy(keyB, 0, block3, 10, 6);

			//return WriteBlock(startBlockNo + blockNum - 1, block3);
		}

		public bool ChangeControlBlock2(int sector, M1KeyMode keyMode, byte[] keyA, byte[] keyB)
		{
			if (!IsValidSector(sector))
				return false;

			byte[] key = (keyMode == M1KeyMode.KEY_A) ? keyA : keyB;
			int startBlockNo = ComputeStartBlockNo(sector);
			int blockNum = ComputeBlockNum(sector);

			if (!Authen(keyMode, startBlockNo, key))
			{
				logger.Debug("Can't authen with KeyMode=" + keyMode.ToString());
				return false;
			}

			byte[] cb = ByteUtil.Malloc(M1_BLOCK_LEN);
			Array.Copy(keyA, 0, cb, 0, 6);
			Array.Copy(controlBlock, 0, cb, 6, 4);
			Array.Copy(keyB, 0, cb, 10, 6);

			return WriteBlock(startBlockNo + blockNum - 1, cb);
		}
		#endregion
	}
}
