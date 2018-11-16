using Bluemoon;
using Bluemoon.Config;
using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace RonftonCard.Core.Dongle
{
	public abstract class AbstractDongle : IDongle
	{
		// use log4net logger
		protected static ILog logger = LogManager.GetLogger("RonftonCardLog");

		protected DongleInfo[] dongles;

		// seed
		protected readonly byte[] seed;

		// encoding charset
		protected Encoding encoding;

		// SUCC flag
		protected uint succ;

		// password for admin & user
		protected String adminPin;
		protected String userPin;

		// error message config
		private Properties errorMsgProp;

		#region "--- constructor ---"

		protected AbstractDongle(Encoding encoding, byte[] seed, String errMsgFileName, String adminPin, String userPin)
		{
			this.encoding = encoding;
			this.seed = seed;

			if (!String.IsNullOrEmpty(errMsgFileName))
				this.errorMsgProp = new Properties(errMsgFileName);

			this.adminPin = String.IsNullOrEmpty(adminPin) ? DongleConst.DEFAULT_ADMIN_PIN_DONGLE : adminPin;
			this.userPin = String.IsNullOrEmpty(userPin) ? DongleConst.DEFAULT_USER_PIN_DONGLE : userPin;

			this.succ = 0x00000000;
		}

		#endregion


		#region "--- implement IAuthenKey ---"

		public DongleInfo[] Dongles
		{
			get { return this.dongles; }
		}

		public bool Succ()
		{
			return this.LastErrorCode == this.succ;
		}

		public uint LastErrorCode { get; protected set; }

		public String LastErrorMessage
		{
			get
			{
				return this.errorMsgProp.Get(GetErrorMsgKey());
			}
		}

		/// <summary>
		/// open specified dongle by KEY_ID
		/// </summary>
		public bool Open(String keyId)
		{
			if (this.Dongles.IsNullOrEmpty())
				return false;

			for(int i=0; i< this.Dongles.Length; i++)
			{
				if ( this.Dongles[i].KeyId.Equals(keyId))
					return Open(this.Dongles[i].Seq);
			}

			return false;
		}
		#endregion

		#region "--- abstract ---"

		public abstract void Close();
		public abstract void Close(int seq);

		public abstract bool Open(int seq = 0);
		public abstract bool Reset(int seq = 0);

		public abstract bool Restore(int seq, byte[] adminPin);

		public abstract ResultArgs CreateUserRootKey(int seq, String userId, String appId, byte[] userRootKey);

		//public abstract ResultArgs CreateAuthenKey();
		//public abstract bool Encrypt(byte[] plain, out byte[] cipher);

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
					//dispose managed state (managed objects).
				}

				// free unmanaged resources (unmanaged objects) and override a finalizer below.
				Close();
				disposedValue = true;
			}
		}

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			Dispose(true);
		}

		#endregion
	}
}
