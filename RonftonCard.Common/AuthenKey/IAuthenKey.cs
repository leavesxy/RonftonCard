using Bluemoon;
using System;
using System.Collections.Generic;

namespace RonftonCard.Common.AuthenKey
{
	public interface IAuthenKey : IDisposable
	{
		/// <summary>
		/// last error code & message
		/// </summary>
		String LastErrorMessage { get; }
		uint LastErrorCode { get; }

		/// <summary>
		/// last operation is succ or not
		/// </summary>
		bool IsSucc();

		/// <summary>
		/// get all keys
		/// </summary>
		/// <returns></returns>
		List<AuthenKeyInfo> GetAuthenKeys();


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
		bool Restore(byte[] pin);


		/// <summary>
		/// Create user root key
		/// </summary>
		ResultArgs CreateUserRootKey(String userId, String appId, byte[] seed, byte[] rootPin);



		////////////////////////////////////////////////////////////////////////////////////////////



		/// <summary>
		/// initialize key
		/// </summary>
		ResultArgs Initialize();



		/// <summary>
		/// Create authen key
		/// </summary>
		ResultArgs CreateAuthenKey();

		/// <summary>
		/// encrypt plain text use root key or private key
		/// </summary>
		bool Encrypt(byte[] plain, out byte[] cipher);
	}
}