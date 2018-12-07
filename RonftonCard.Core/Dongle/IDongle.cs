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
		#region "--- Properties ---"
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
		/// last operation if is SUCC
		/// </summary>
		bool IsSucc { get; }

		/// <summary>
		/// index of active dongle
		/// </summary>
		int SelectedIndex { get; }

		#endregion

		#region "--- device interface ---"
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

		/// <summary>
		/// get timer of device
		/// </summary>
		/// <returns></returns>
		DateTime GetDevTimer();

		#endregion


		#region "--- App interface ---"

		/// <summary>
		/// create User root key dongle
		/// </summary>
		ResultArgs CreateUserRootDongle(String userId, byte[] userRootKey, DongleUserInfo keyInfo);


		/// <summary>
		/// create applicaton authentication key dongle
		/// </summary>
		ResultArgs CreateAppAuthenDongle(String userId, DongleUserInfo keyInfo);

		/// <summary>
		/// encrypt by dongle
		/// </summary>
		bool Encrypt(DongleType dongleType, byte[] plain, out byte[] cipher);

		#endregion
	}
}