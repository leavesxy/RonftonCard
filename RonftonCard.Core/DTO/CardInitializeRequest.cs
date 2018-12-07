using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.DTO
{
	public class CardInitializeRequest
	{
		public byte[] SN { get; set; }

		//钱包扇区(文件) : 'RFT'(3) + sn(4) + userId(6) + 'P' + '00'
		//身份扇区(文件) : 'rft'(3) + sn(4) + userId(6) + 'i' + '00'

	}
}
