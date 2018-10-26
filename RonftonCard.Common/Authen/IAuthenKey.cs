using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Common.Authen
{
	public interface IAuthenKey : IDisposable
	{
		KeyInfo GetKeyInfo();

		void Close();

		bool Initialize(byte[] defaultAdminPwd,out byte[] newAdminPwd, out byte[] pid);

		bool Authen(AuthenMode authenMode, byte[] pin);
	}
}