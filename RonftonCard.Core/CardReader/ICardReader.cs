using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.CardReader
{
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
		void Light(bool flag);

		#endregion

		#region "--- M1 Card operation ---"

		bool Authen(KeyMode keyMode, int descriptor, byte[] pwd);

		/// <summary>
		/// for M1,descriptor is sector
		/// for CPU, descriptor is file_descriptor
		/// </summary>
		bool ReadBlock(int blockNum, out byte[] data, int len);
		bool ReadSector(int sectorNum, out byte[] data, int len);

		bool WriteBlock(int blockNum, byte[] data, int len);

		/// <summary>
		/// select card
		/// </summary>
		bool Select(out byte[] cardId);

		#endregion
	}
}