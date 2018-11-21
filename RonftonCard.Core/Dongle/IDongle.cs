using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bluemoon;

namespace RonftonCard.Core.Dongle
{
	public interface IDongle : IDisposable
	{
		void Close();

		bool Open();

		bool Restore(byte[] adminPin);

		bool Reset();

		bool WriteUserInfo(DongleUserInfo userInfo);

		ResultArgs CreateUserRootKey(String userId, byte[] userRootKey);

		ResultArgs CreateAuthenKey(String userId);

	}
}