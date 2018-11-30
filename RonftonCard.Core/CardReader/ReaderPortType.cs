namespace RonftonCard.Core.CardReader
{
	/// <summary>
	///	com			: 0~99
	///	USB			: 100~199
	///	PCSC		: 200~299
	///	Bluetooth	: 300~399
	/// </summary>
	public enum ReaderPortType : int
	{
		COM=0,
		USB=100,
		PCSC=200,
		BLUE_TOOTH=300
	}
}
