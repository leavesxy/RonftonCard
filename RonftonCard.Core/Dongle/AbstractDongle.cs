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
		protected static ILog logger = LogManager.GetLogger("RonftonCardLog");
		private uint SUCC = 0x00000000;

		protected DongleInfo[] dongleInfo;
		protected readonly byte[] seed;
		protected String defaultAdminPin;
		protected String defaultUserPin;
		private Properties errorMsgProp;

		#region "--- constructor ---"

		protected AbstractDongle(String encoding, String seed, String errMsgFileName, String defaultAdminPin, String defaultUserPin)
		{
			this.Encoder = Encoding.GetEncoding(encoding);

			this.seed = String.IsNullOrEmpty(seed) ? this.Encoder.GetBytes(DongleConst.DEFAULT_SEED_KEY) : this.Encoder.GetBytes(seed);

			if (!String.IsNullOrEmpty(errMsgFileName))
				this.errorMsgProp = new Properties(errMsgFileName);

			this.defaultAdminPin = String.IsNullOrEmpty(defaultAdminPin) ? DongleConst.DEFAULT_ADMIN_PIN_DONGLE : defaultAdminPin;
			this.defaultUserPin = String.IsNullOrEmpty(defaultUserPin) ? DongleConst.DEFAULT_USER_PIN_DONGLE : defaultUserPin;
		}

		#endregion

		#region "--- public properties ---"

		public DongleInfo[] Dongles
		{
			get { return this.dongleInfo; }
		}

		public uint LastErrorCode { get; protected set; }

		public String LastErrorMessage
		{
			get
			{
				return this.errorMsgProp.Get(GetErrorMsgKey());
			}
		}

		public Encoding Encoder { get; protected set; }

		#endregion

		public bool IsSucc
		{
			get { return (SUCC == this.LastErrorCode); }
		}

		public bool Open(String keyId)
		{
			if (this.dongleInfo.IsNullOrEmpty())
				return false;

			for(int i=0; i< this.dongleInfo.Length; i++)
			{
				if ( this.dongleInfo[i].KeyId.Equals(keyId))
					return Open(this.dongleInfo[i].Seq);
			}

			return false;
		}

		#region "--- abstract ---"

		public abstract void Close();
		public abstract void Close(int seq);
		public abstract bool Open(int seq = 0);
		public abstract bool Reset(int seq = 0);
		public abstract bool Enumerate();
		public abstract bool Restore(byte[] adminPin, int seq=0);

		public abstract ResultArgs CreateUserRootKey(String userId, byte[] userRootKey, int seq = 0);

		public abstract ResultArgs CreateAuthenKey(String userId, int seq = 0);

		public abstract bool Encrypt(byte[] plain, out byte[] cipher, int seq=0);

		public abstract bool PriEncrypt(byte[] plain, out byte[] cipher, int seq=0);

		protected abstract String GetErrorMsgKey();

		#endregion

		#region "--- IDisposable implements ---"
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

		public void Dispose()
		{
			Dispose(true);
		}

		#endregion
	}
}