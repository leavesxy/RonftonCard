using System;

namespace RonftonCard.Common.Reader
{
	/// <summary>
	/// interface for IC card reader
	/// </summary>
	public interface ICardReader : IDisposable
	{
		#region "--- device operation ---"

		/// <summary>
		/// get device information,maybe is null!
		/// </summary>
		String GetVersion();

		/// <summary>
		/// close device
		/// </summary>
		void Close();

		/// <summary>
		/// control to beep
		/// duration equal 10 ms as default for each beep
		/// </summary>
		void Beep(int times = 1, int duration = 10);

		/// <summary>
		/// turn on or off signal light
		/// </summary>
		void Light(bool onOff);

		#endregion

		#region "--- Card operation ---"

		#endregion
	}
}
