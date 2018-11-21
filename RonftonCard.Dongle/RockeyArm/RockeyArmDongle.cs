using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluemoon;
using RonftonCard.Core.Dongle;

namespace RonftonCard.Dongle.RockeyArm
{
	public partial class RockeyArmDongle : IDongle
	{
		public DongleInfo[] DongleInfo
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public Encoding Encoder
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool IsSucc
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public string LastErrorMessage
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public void Close()
		{
			throw new NotImplementedException();
		}

		public ResultArgs CreateAuthenKey(string userId)
		{
			throw new NotImplementedException();
		}

		public ResultArgs CreateUserRootKey(string userId, byte[] userRootKey)
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public bool Encrypt(byte[] plain, out byte[] cipher)
		{
			throw new NotImplementedException();
		}

		public bool Open()
		{
			throw new NotImplementedException();
		}

		public bool Reset()
		{
			throw new NotImplementedException();
		}

		public bool Restore(byte[] adminPin)
		{
			throw new NotImplementedException();
		}

		public bool WriteUserInfo(DongleUserInfo userInfo)
		{
			throw new NotImplementedException();
		}
	}
}
