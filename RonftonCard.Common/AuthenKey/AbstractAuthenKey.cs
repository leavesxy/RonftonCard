using Bluemoon;
using Bluemoon.Config;
using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace RonftonCard.Common.AuthenKey
{
	/// <summary>
	/// base authen Key
	/// </summary>
	public abstract class AbstractAuthenKey : IAuthenKey
	{
		// use log4net logger
		protected static ILog logger = LogManager.GetLogger("RonftonCardLog");

		// default seed
		protected readonly byte[] seed;

		// default encoding charset
		protected Encoding encoding;

		// SUCC flag
		protected uint succ;

		// default password for admin & user
		protected String adminPin;
		protected String userPin;

		// error message config
		private Properties errorMsgProp;

		#region "--- constructor ---"

		protected AbstractAuthenKey(byte[] seed, String errMsgFileName, String adminPin, String userPin, Encoding encoding)
		{
			this.adminPin = adminPin;
			this.userPin = userPin;
			this.succ = 0x00000000;
			this.encoding = encoding;
			this.seed = seed;
			
			if ( !String.IsNullOrEmpty(errMsgFileName))
				this.errorMsgProp = new Properties(errMsgFileName);
		}

		#endregion

		#region "--- implement IAuthenKey ---"

		public bool IsSucc()
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

		public bool Open(String keyId)
		{
			foreach(AuthenKeyInfo key in GetAuthenKeys())
			{
				if (key.KeyId.Equals(keyId))
					return Open(key.Seq);
			}
			return false;
		}
		#endregion


		public String GetAllErrorMessage()
		{
			return this.errorMsgProp.ToString();
		}

		#region "--- abstract method ---"

		public abstract void Close();
		public abstract bool Open(int seq = 0);
		
		public abstract List<AuthenKeyInfo> GetAuthenKeys();
		public abstract bool Restore(byte[] adminPin);

		public abstract ResultArgs CreateUserRootKey(String userId, String appId);

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
