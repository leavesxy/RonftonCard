using System;
using System.Collections.Generic;
using System.Text;
using Bluemoon;
using log4net;
using RonftonCard.Core;
using RonftonCard.Core.DTO;

namespace RonftonCard.KeyService
{
	public class LocalTestKeyService : IKeyService
	{
		private static ILog logger = LogManager.GetLogger("RonftonCardService");

		private const String KEY_SALT = "RF";

		// RootKey  TDes( CardID + UserId + Sector + SectorType + CardType + KEY_SALT )
		//                  4        6        2        1(I|W)     1(5|7|C)      2

		public ResultArgs ComputeKey(CardKeyRequest[] request)
		{
			if (request.IsNullOrEmpty())
				return new ResultArgs(false, null, "request is null !");

			List<CardKeyResponse> response = new List<CardKeyResponse>();

			List<byte> buffer = new List<byte>();
			Encoding encoder = Encoding.ASCII;

			for (int i = 0; i < request.Length; i++)
			{
				buffer.Clear();
				buffer.AddRange(request[i].CardId);
				buffer.AddRange(encoder.GetBytes(GetUserId()));
				buffer.AddRange(BitConverter.GetBytes(request[i].Sector));
				buffer.AddRange(BitConverter.GetBytes(request[i].SectorType));
				buffer.AddRange(BitConverter.GetBytes(request[i].CardType));
				buffer.AddRange(encoder.GetBytes(KEY_SALT));

				logger.Debug(BitConverter.ToString(buffer.ToArray()));

				CardKeyResponse res = new CardKeyResponse()
				{
					CardId = request[i].CardId,
					Sector = request[i].Sector,
					ReadKey = ComputeKeyA(buffer.ToArray()),
					WriteKey = ComputeKeyA(buffer.ToArray()),
					ControlBlock = ComputeControlBlock()
				};

				response.Add(res);
			}
			return new ResultArgs(true, response.ToArray(), "OK");
		}

		private byte[] ComputeKeyA( byte[] request )
		{
			return new byte[] { 0x01, 0x01, 0x01, 0x01, 0x01, 0x01 };
		}

		private byte[] ComputeKeyB(byte[] request)
		{
			return new byte[] { 0xef, 0xef, 0xef, 0xef, 0xef, 0xef };
		}

		private byte[] ComputeControlBlock()
		{
			return new byte[] { 0x78, 0x77, 0x88, 0x69 };
		}

		private String GetUserId()
		{
			return "012345";
		}
	}
}