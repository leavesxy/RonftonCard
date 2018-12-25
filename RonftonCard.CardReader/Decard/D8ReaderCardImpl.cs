using System;


namespace RonftonCard.CardReader.Decard
{
	using Bluemoon;
	using Core.CardReader;
	using Core.DTO;

	/// <summary>
	/// implements Card operation
	/// </summary>
	public partial class D8Reader
	{
		/// <summary>
		/// atqa : Answer To Request, Type A
		///	sak : Select Acknowledge
		/// </summary>
		public bool Select(out byte[] sn)
		{
			uint snLen = 0;
			byte[] buffer = ByteUtil.Malloc(16);

			//Reset();
			if (dc_card_n(this.hReader, 0x00, ref snLen, buffer) != SUCC)
			{
				sn = null;
				return false;
			}
			sn = ArrayUtil.CopyFrom(buffer, (int)snLen);
			return true;
		}

		public ResultArgs Select()
		{
			ResultArgs ret = new ResultArgs(false);
			CardSelectResult cardInfo = new CardSelectResult();

			if (this.hReader == -1)
				return ret;

			//atqa :: M1_S50: 0x0004; M1_S70: 0x0002
			UInt16 atqa = 0;
			if (dc_request(this.hReader, 0x00, ref atqa) != SUCC)
			{
				ret.Msg = "Can't request atqa !";
				return ret;
			}

			cardInfo.ATQA = atqa;

			byte[] sn = ByteUtil.Malloc(16);
			if (dc_anticoll(this.hReader, 0x00, sn) != SUCC)
			{
				ret.Msg = "AntiCollision Failed !";
				return ret;
			}

			// remove zero at the end
			cardInfo.SN = ByteUtil.TrimEnd(sn);

			//M1: sak = 0x08
			//CPU simulate M1: sak=0x28
			byte sak = 0x00;
			if (dc_select(this.hReader, sn, ref sak) != SUCC)
			{
				ret.Msg = "Can't get SAK!";
				return ret;
			}
			cardInfo.SAK = sak;
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
		/// block length(bytes) of M1
		/// </summary>
		private const int M1_BLOCK_LEN = 16;

		/// <summary>
		/// M1_S50: 16 sectors,  64 blocks
		/// M1_S70: 40 sectors, 256 blocks
		///			in the first 32 sectors, 4 blocks per sector. 
		///			16 blocks per sector in the last 8 sectors
		/// </summary>
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
			logger.Debug("Read Block " + blockNo.ToString() + " : " + BitConverter.ToString(outData));

			return ret == SUCC;
		}

		public bool WriteBlock(int blockNo, byte[] inData)
		{
			if (!IsValidBlock(blockNo))
				return false;

			return dc_write(this.hReader, (byte)(blockNo & 0xff), inData) == SUCC;
		}

		/// <summary>
		/// compute how many blocks in specified sector
		/// </summary>
		private int ComputeBlockNum(int sector)
		{
			return (sector < M1_SECTOR_SPLIT) ? 4 : 16;
		}

		/// <summary>
		/// compute start block of specified sector
		/// </summary>
		private int ComputeStartBlockNo(int sector)
		{
			return (sector < M1_SECTOR_SPLIT)
				? sector * 4
				: M1_SECTOR_SPLIT * 4 + (sector - M1_SECTOR_SPLIT) * 16;
		}

		/// <summary>
		/// read all sector data
		/// </summary>
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

		/// <summary>
		/// write data to sector
		/// </summary>
		public bool WriteSector(int sector, byte[] inData, int len)
		{
			// len should be Multiple of M1_BLOCK_LEN
			if (!IsValidSector(sector) || len <= 0 || len % M1_BLOCK_LEN != 0)
				return false;

			// compute block number for this sector
			// if len is greater than block number of this sector, the excess part is discarded
			// if len is less than block number of this sector, keep the rest of sector
			int loop = Math.Min(ComputeBlockNum(sector), len / M1_BLOCK_LEN);

			int startBlockNo = ComputeStartBlockNo(sector);

			// len = M1_BLOCK_LEN * blockNum;
			byte[] buffer = ByteUtil.Malloc(16);

			for (int i = 0; i < loop; i++)
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
		public bool ChangeControlBlock(int sector, M1KeyMode keyMode, byte [] key, byte[] keyA, byte[] keyB)
		{
			if (!IsValidSector(sector))
				return false;

			int startBlockNo = ComputeStartBlockNo(sector);

			if (!Authen(keyMode, startBlockNo, key))
			{
				logger.Debug("Can't authen with KeyMode=" + keyMode.ToString());
				return false;
			}
			return dc_changeb3(this.hReader, (byte)(sector & 0xff), keyA, 0x04, 0x04, 0x04, 0x03, 0x00, keyB) == SUCC;
		}

		public bool ChangeControlBlock2(int sector, M1KeyMode keyMode, byte[] key, byte[] keyA, byte[] keyB)
		{
			if (!IsValidSector(sector))
				return false;

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
	}
}
