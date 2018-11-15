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
		/// get all keys
		/// </summary>
		List<DongleInfo> GetDongleKeys();


		/// <summary>
		/// Close device
		/// </summary>
		void Close();

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
		bool Restore(byte[] adminPin);

		/// <summary>
		/// set status as anonymous
		/// </summary>
		bool Reset();

		/// <summary>
		/// Create user root key
		/// and must use default admin pin
		/// </summary>
		ResultArgs CreateUserRootKey(String userId, String appId, byte[] userRootKey);

		////////////////////////////////////////////////////////////////////////////////////////////
		///// <summary>
		///// Create authen key
		///// </summary>
		//ResultArgs CreateAuthenKey();

		///// <summary>
		///// encrypt plain text use root key or private key
		///// </summary>
		//bool Encrypt(byte[] plain, out byte[] cipher);
	}
}