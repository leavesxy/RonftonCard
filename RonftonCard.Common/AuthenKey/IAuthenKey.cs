using BlueMoon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Common.AuthenKey
{
	public interface IAuthenKey : IDisposable
	{
		/// <summary>
		/// last error message
		/// </summary>
		String LastErrorMessage { get; }

		/// <summary>
		/// enumerate all keys
		/// </summary>
		/// <returns></returns>
		AuthenKeyInfo[] Enumerate();


		/// <summary>
		/// Close device
		/// </summary>
		void Close();

		/// <summary>
		/// open device and first is default
		/// </summary>
		bool Open(int seq = 0);

		/// <summary>
		/// initialize key
		/// </summary>
		RetArgs Initialize();

		/// <summary>
		/// Create user root key
		/// </summary>
		RetArgs CreateRootKey();

		/// <summary>
		/// Create authen key
		/// </summary>
		RetArgs CreateAuthenKey();

		/// <summary>
		/// encrypt plain text use root key or private key
		/// </summary>
		bool Encrypt(byte[] plain, out byte[] cipher);
	}
}