using System;

namespace RonftonCard.Core.Dongle
{
	public interface IDongleFactory
	{
		/// <summary>
		/// all dongles
		/// </summary>
		DongleInfo[] Dongles { get; }

		/// <summary>
		/// get service instance by seq
		/// </summary>
		IDongleService GetDongleService(int seq);

		/// <summary>
		/// get service instance by key_id
		/// </summary>
		IDongleService GetDongleService(String keyId);
	}
}