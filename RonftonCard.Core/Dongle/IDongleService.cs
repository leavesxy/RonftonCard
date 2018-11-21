using System;
using System.Text;

namespace RonftonCard.Core.Dongle
{
	public interface IDongleService
	{
		String LastErrorMessage { get; }

		Encoding Encoder { get; }

		DongleInfo[] DongleInfo { get; }

		bool IsSucc { get; }

		bool Encrypt(byte[] plain, out byte[] cipher);
	}
}