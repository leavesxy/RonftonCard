using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Common.Reader
{
	// define handler for device
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

		#region "--- implement Device operation ---"

		protected abstract void Open();

		public abstract void Beep(int times = 1, int duration = 10);

		public abstract void Close();

		public abstract String GetVersion();

		public abstract void Light(bool onOff);

		#endregion

		#region "--- implement Card operation ---"
		public abstract bool Authen(KeyMode keyMode, int descriptor, byte[] pwd);

		public abstract bool Read(int descriptor, out byte[] data);

		public abstract bool Select(out byte[] cardId);

		public abstract bool Write(int descriptor, byte[] data);
		#endregion

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
				}

				Close();
				disposedValue = true;
			}
		}

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion

	}
}
