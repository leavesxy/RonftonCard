using System;
using System.Text;
using Bluemoon;

namespace RonftonCard.Core.Dongle
{
    using Core.DTO;

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
		void Close();

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

		ResultArgs CreateUserRootKey(String userId, byte[] userRootKey, DongleKeyInfo keyInfo);

		ResultArgs CreateAuthenKey(String userId, DongleKeyInfo keyInfo);

		bool Encrypt(DongleType dongleType, byte[] plain, out byte[] cipher);
	}
}