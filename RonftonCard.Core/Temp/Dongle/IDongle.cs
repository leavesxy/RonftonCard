using Bluemoon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.Temp.Dongle
{
	public interface IDongle : IDisposable
	{
		#region "--- properties ---"
		String LastErrorMessage { get; }
		uint LastErrorCode { get; }
		Encoding Encoder { get; }
		DongleInfo[] Dongles { get; }

		bool IsSucc { get; }

		#endregion

		#region "--- device interface ---"
		/// <summary>
		/// last operation is succ or not
		/// </summary>


		void Close();
		void Close(int seq);

		bool Open(int seq = 0);

		bool Open(String keyId);

		/// <summary>
		/// restore current key
		/// </summary>
		bool Restore(byte[] adminPin, int seq = 0);

		/// <summary>
		/// set status as anonymous
		/// </summary>
		bool Reset(int seq = 0);

		bool Enumerate();

		#endregion

		#region "--- bizz interface ---"
		/// <summary>
		/// Create user root key
		/// and must use default admin pin
		/// </summary>
		ResultArgs CreateUserRootKey(String userId, byte[] userRootKey, int seq=0);

		ResultArgs CreateAuthenKey(String userId, int seq = 0);

		bool Encrypt(byte[] plain, out byte[] cipher, int seq=0);

		bool PriEncrypt(byte[] plain, out byte[] cipher, int seq=0);

		#endregion
	}
}