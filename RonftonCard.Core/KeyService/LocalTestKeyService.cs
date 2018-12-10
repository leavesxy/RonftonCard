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
		//private byte[] controlBlock;	// 0x78, 0x77, 0x88, 0x69

		private const String KEY_SALT = "RFT";
		
		public LocalTestKeyService(ILog logger, IDongle dongle, String userId)
		{
			this.logger = logger;
			this.userId = ArrayUtil.CopyFrom<byte>( Encoding.ASCII.GetBytes(userId), 6 );
			this.dongle = dongle;
		}

		public bool IsOK()
		{
			return true;
		}

		/// <summary>
		/// Key : 'RFT'(3) + sn(4) + userId(6) + sectorType + '00'
		/// </summary>
		public CardInitResponse ComputeKey(CardInitRequest req)
		{
			byte[] plain = PrepareKeyData(req);

			CardInitResponse response = new CardInitResponse()
			{
				Sector = req.Sector,
				KeyA = ComputeKeyA( plain ),
				KeyB = ComputeKeyB( plain )
			};

			return response;
		}

		public List<CardInitResponse> ComputeKey(List<CardInitRequest> req)
		{
			List<CardInitResponse> response = new List<CardInitResponse>();

			foreach (CardInitRequest __req in req)
			{
				response.Add(ComputeKey(__req));
			}

			return response;
		}


		private byte[] ComputeKeyA(byte[] plain)
		{
			return new byte[] { 0x0a, 0x0a, 0x0a, 0x0a, 0x0a, 0x0a };
		}

		private byte[] ComputeKeyB(byte[] plain)
		{
			return new byte[] { 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b };
		}

		private byte[] PrepareKeyData(CardInitRequest req)
		{
			List<byte> buffer = new List<byte>();
			buffer.AddRange(Encoding.ASCII.GetBytes("RFT"));	// 3
			buffer.AddRange(req.SN);							// 4
			buffer.AddRange(this.userId);						// 6
			buffer.Add( req.SectorType );						// 1
			buffer.AddRange( new byte[] { 0x00, 0x00 } );       // 2

			logger.Debug("Prepare Key data : " + BitConverter.ToString(buffer.ToArray()));
			return buffer.ToArray();
		}
	}
}