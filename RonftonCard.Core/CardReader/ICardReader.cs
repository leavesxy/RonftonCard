using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluemoon;

namespace RonftonCard.Core.CardReader
{
	public interface ICardReader : IDisposable
	{
		#region "--- device operation ---"

		bool Open();
		void Close();
		void Reset();

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

		bool Select(out byte[] cardId);
		bool Select2(out byte[] cardId, out UInt16 atqa, out byte sak);
		ResultArgs Select();

		/// <summary>
		/// logic block number
		/// </summary>
		bool Authen(M1KeyMode keyMode, int blockNo, byte[] pwd);

		/// <summary>
		/// block is 0-63 for M1_S50, and 0-255 for M1_S70 
		/// </summary>
		bool ReadBlock(int blockNo, out byte[] outData);
		bool WriteBlock(int blockNo, byte[] inData);

		/// <summary>
		/// sector is 0-15 for M1_S50, and 0-39 for M1_S70
		/// </summary>
		bool ReadSector(int sector, out byte[] outData, out int len);
		bool WriteSector(int sector, byte[] inData, int len);

		bool ChangeControlBlock(int sector, M1KeyMode keyMode, byte[] keyA, byte[] keyB);

		#endregion
	}
}