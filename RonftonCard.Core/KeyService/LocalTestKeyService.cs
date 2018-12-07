using System;
using System.Collections.Generic;
using System.Text;
using Bluemoon;
using log4net;

namespace RonftonCard.Core.KeyService
{
	using Dongle;
	using DTO;

	public class LocalTestKeyService : IKeyService
	{
		private ILog logger;
		private IDongle dongle;

		private byte[] userId;
		private byte[] controlBlock;	// 0x78, 0x77, 0x88, 0x69

		private const String KEY_SALT = "RFT";
		
		public LocalTestKeyService(ILog logger, IDongle dongle, String userId, byte[] controlBlock)
		{
			this.logger = logger;
			this.userId = ArrayUtil.CopyFrom<byte>( Encoding.ASCII.GetBytes(userId), 6 );
			this.controlBlock = controlBlock;
			this.dongle = dongle;
		}

		public bool IsOK()
		{
			return true;
		}

		/// <summary>
		/// 钱包扇区(文件) : 'RFT'(3) + sn(4) + userId(6) + 'P' + '00'
		/// 身份扇区(文件) : 'rft'(3) + sn(4) + userId(6) + 'i' + '00'
		/// </summary>
		public ResultArgs ComputeKey(byte[] sn)
		{
			byte[] key_i = ComputeIdKey(sn);
			byte[] key_w = ComputeWalletKey(sn);

			ResultArgs ret = new ResultArgs(true);
			ret.Msg = "OK";

			// 若要改成按每个扇区给密钥
			// key为扇区号、文件号，value为读密钥和写密钥的组合
			ret.Result = new Dictionary<ushort, byte[]>()
			{
				{ 0, key_i },
				{ 1, key_w }
			};
			return ret;
		}

		private byte[] ComputeIdKey(byte[] sn)
		{
			List<byte> buffer = new List<byte>();
			buffer.AddRange(Encoding.ASCII.GetBytes("RFT"));
			buffer.AddRange(sn);
			buffer.AddRange(this.userId);
			buffer.Add((byte)'i');
			buffer.AddRange( new byte[] { 0x00, 0x00 } );

			// 使用dongle根密钥加密上述内容
			// 一定用logger把密钥记录下来
			List<byte> key = new List<byte>();
			key.AddRange(new byte[] { 0x01, 0x01, 0x01, 0x01, 0x01, 0x01 });    //key_a
			key.AddRange(controlBlock);
			key.AddRange(new byte[] { 0x02, 0x02, 0x02, 0x02, 0x02, 0x02 });    //key_b
			return key.ToArray();
		}

		private byte[] ComputeWalletKey(byte[] sn)
		{
			List<byte> key = new List<byte>();
			key.AddRange(new byte[] { 0x01, 0x01, 0x01, 0x01, 0x01, 0x01 });    //key_a
			key.AddRange(controlBlock);
			key.AddRange(new byte[] { 0x03, 0x03, 0x03, 0x03, 0x03, 0x03 });    //key_b
			return key.ToArray();
		}
	}
}