using BlueMoon.Config;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Common.AuthenKey
{
	/// <summary>
	/// base authen Key
	/// </summary>
	public abstract class AbstractAuthenKey : IAuthenKey
	{
		// use log4net logger
		protected static ILog logger = LogManager.GetLogger("RonftonCardLog");

		// SUCC flag
		protected uint succ;

		// default password for admin & user
		protected String defaultAdminPin;
		protected String defaultUserPin;

		protected const ushort COMPANY_SEED_KEY_DESCRIPTOR = 0x1005;
		protected const ushort USER_ROOT_KEY_DESCRIPTOR = 0x1006;
		protected const ushort AUTHEN_KEY_DESCRIPTOR = 0x1007;

		// error message config
		private Properties errorMessage;

		public uint LastErrorCode { get; protected set; }

		#region "--- constructor ---"
		protected AbstractAuthenKey(String errMsgFileName)
			: this("FFFFFFFFFFFFFFFF", "12345678", errMsgFileName)
		{
		}

		protected AbstractAuthenKey(String adminPin, String userPin, String errMsgFileName)
		{
			this.defaultAdminPin = adminPin;
			this.defaultUserPin = userPin;
			this.succ = 0x00000000;

			if ( !String.IsNullOrEmpty(errMsgFileName))
				this.errorMessage = new Properties(errMsgFileName);
		}

		#endregion

		public bool IsSucc()
		{
			return this.LastErrorCode == this.succ;
		}

		public String LastErrorMessage
		{
			get
			{
				
				return errorMessage.Get(GetErrorMsgKey());
			}
		}

		public String GetAllErrorMessage()
		{
			return errorMessage.ToString();
		}


		#region "--- abstract method ---"
		public abstract void Close();
		public abstract bool Open(int seq = 0);
		public abstract AuthenKeyInfo[] Enumerate();
		public abstract bool Create(AuthenKeyType keyType, byte[] inData);
		public abstract bool Encrypt(AuthenKeyType keyType, byte[] plain, out byte[] cipher);

		protected abstract String GetErrorMsgKey();

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

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.
				Close();
				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~AbstractAuthenKey() {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

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
