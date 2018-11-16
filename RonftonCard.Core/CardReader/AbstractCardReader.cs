using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.CardReader
{
	using DEV_HANDLER = System.Int32;

	public abstract class AbstractCardReader : ICardReader
	{
		protected DEV_HANDLER hDev;
		protected int port;
		protected int baud;

		protected AbstractCardReader(int port, int baud)
		{
			this.port = port;
			this.baud = baud;
			this.hDev = -1;
		}

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// dispose managed state here(managed objects).
				}

				// dispose un-managed state
				Close();
				disposedValue = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion

		#region "--- abstract method ---"

		protected abstract void Open();
		public abstract void Close();
		public abstract void Beep(int times = 1, int duration = 10);
		public abstract String GetVersion();
		public abstract void Light(bool onOff);

		public abstract bool Authen(KeyMode keyMode, int descriptor, byte[] pwd);
		public abstract bool Select(out byte[] cardId);
		public abstract bool Read(int descriptor, out byte[] data, int len);
		public abstract bool Write(int descriptor, byte[] data, int len);
		#endregion

	}
}
