using System;
using System.Text;
using Bluemoon;
using RonftonCard.Core.Entity;

namespace RonftonCard.Core.Dongle
{
	/// <summary>
	/// interface of Dongle
	/// </summary>
	public interface IDongle : IDisposable
	{
		DongleInfo[] Dongles { get; }
		String LastErrorMessage { get; }
		Encoding Encoder { get; }
		bool IsSucc { get; }

		/// <summary>
		/// enumerate all dongles
		/// </summary>
		void Enumerate();

		/// <summary>
		/// erase specified dongle
		/// </summary>
		bool Restore(int seq, byte[] adminPin);

		/// <summary>
		/// reset status of dongle
		/// </summary>
		bool Reset(int seq);

		bool CreateUserInfo(int seq, DongleUserInfo userInfo);

		ResultArgs CreateUserRootKey(int seq, String userId, byte[] userRootKey);

		ResultArgs CreateAuthenKey(int seq, String userId);

		bool Encrypt(int seq, DongleType dongleType, byte[] plain, out byte[] cipher);
	}
}