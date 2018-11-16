﻿using Bluemoon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.Dongle
{
	public interface IDongle : IDisposable
	{
		/// <summary>
		/// last error code & message
		/// </summary>
		String LastErrorMessage { get; }
		uint LastErrorCode { get; }

		/// <summary>
		/// last operation is succ or not
		/// </summary>
		bool Succ();

		/// <summary>
		/// all dongle keys
		/// </summary>
		DongleInfo[] Dongles { get; }


		/// <summary>
		/// Close device
		/// </summary>
		void Close();
		void Close(int seq);

		/// <summary>
		/// open device in enumerate sequence
		/// </summary>
		bool Open(int seq = 0);

		/// <summary>
		/// open device with specified key id
		/// </summary>
		bool Open(String keyId);

		/// <summary>
		/// restore current key
		/// </summary>
		bool Restore(int seq,byte[] adminPin);

		/// <summary>
		/// set status as anonymous
		/// </summary>
		bool Reset(int seq = 0);

		/// <summary>
		/// Create user root key
		/// and must use default admin pin
		/// </summary>
		ResultArgs CreateUserRootKey(int seq, String userId, String appId, byte[] userRootKey);
	}
}
