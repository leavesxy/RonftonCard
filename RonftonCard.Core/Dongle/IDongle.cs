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
		/// <summary>
		/// all dongle information
		/// </summary>
		DongleInfo[] Dongles { get; }

		/// <summary>
		/// last error message
		/// </summary>
		String LastErrorMessage { get; }

		/// <summary>
		/// encoding
		/// </summary>
		Encoding Encoder { get; }

		/// <summary>
		/// last invoke is SUCC or not
		/// </summary>
		bool IsSucc { get; }

		/// <summary>
		/// index of active dongle
		/// </summary>
		int SelectedIndex { get; }

		/// <summary>
		/// open specified dongle,and set select index
		/// </summary>
		bool Open(int seq);

		/// <summary>
		/// enumerate all dongles
		/// </summary>
		bool Enumerate();

		/// <summary>
		/// erase selected dongle
		/// </summary>
		bool Restore(byte[] adminPin);

		/// <summary>
		/// reset status of selected dongle
		/// </summary>
		bool Reset();


		bool CreateUserInfo(DongleUserInfo userInfo);

		ResultArgs CreateUserRootKey(String userId, byte[] userRootKey);

		ResultArgs CreateAuthenKey(String userId);

		bool Encrypt(DongleType dongleType, byte[] plain, out byte[] cipher);
	}
}