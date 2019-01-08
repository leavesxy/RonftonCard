using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using RonftonCard.Core;
using RonftonCard.Core.DTO;

namespace RonftonCard.KeyService
{
	public class M1KeyServiceUtil
	{
		protected static ILog logger = ContextManager.GetLogger();

		private static byte[] userId = Encoding.UTF8.GetBytes("000001");
		private static byte[] controlBlock = new byte[] { 0x78, 0x77, 0x88, 0x69 };
		private const String KEY_SALT = "RFT";


		/// <summary>
		/// Key : 'RFT'(3) + sn(4) + userId(6) + sectorType + '00'
		/// </summary>
		public static CardInitResponse ComputeKey(CardInitRequest req)
		{
			byte[] factor = PrepareData(req);

			CardInitResponse response = new CardInitResponse()
			{
				Sector = req.Sector,
				ReadableKey = BitConverter.ToString(ComputeReadableKey(factor)),
				WritableKey = BitConverter.ToString(ComputeWritableKey(factor))
			};

			return response;
		}

		private static byte[] ComputeReadableKey(byte[] factor)
		{
			return new byte[] { 0x0a, 0x0a, 0x0a, 0x0a, 0x0a, 0x0a, 0x78, 0x77, 0x88, 0x69, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b };
		}

		private static byte[] ComputeWritableKey(byte[] factor)
		{
			return new byte[] { 0x0a, 0x0a, 0x0a, 0x0a, 0x0a, 0x0a, 0x78, 0x77, 0x88, 0x69, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b, 0x0b };
		}

		private static byte[] PrepareData(CardInitRequest req)
		{
			List<byte> buffer = new List<byte>();
			buffer.AddRange(Encoding.ASCII.GetBytes("RFT"));		// 3
			buffer.AddRange(req.SN);								// 4
			buffer.AddRange(Encoding.ASCII.GetBytes(req.UserId));	// 6
			buffer.AddRange( BitConverter.GetBytes( req.Sector ));	// 2
			buffer.Add((byte)0x00);									// 1

			logger.Debug("Prepare Key data : " + BitConverter.ToString(buffer.ToArray()));
			return buffer.ToArray();
		}
	}
}
