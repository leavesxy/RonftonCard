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
		/// open & close device
		/// </summary>
		/// <returns></returns>
		bool Open();
		void Close();
				
		/// <summary>
		/// get device information,maybe is null!
		/// </summary>
		String GetVersion();
		

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

		/// <summary>
		/// select card
		/// </summary>
		bool Select(out byte[] cardId);

		bool Authen(KeyMode keyMode, int descriptor, byte[] pwd);

		/// <summary>
		/// for M1,descriptor is sector
		/// for CPU, descriptor is file_descriptor
		/// </summary>
		bool ReadBlock(int block, out byte[] outData);

		bool ReadSector(int sector, out byte[] outData, out int len);

		bool WriteBlock(int block, byte[] inData);

		bool WriteSector(int sector, byte[] inData, int len);

		#endregion
	}
}