using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.DTO
{
	/// <summary>
	/// data written to card
	/// </summary>
	public class CardInfo
	{
		/// <summary>
		/// version of card
		/// </summary>
		[M1Card(offset:0, dataType: CardDataType.BYTE,length:1)]
		public byte Version { get; set; }

		/// <summary>
		/// university Code, 6 digit
		/// </summary>
		[M1Card(offset: 1, dataType: CardDataType.BCD, length: 3)]
		public String UniversityCode { get; set; }

		/// <summary>
		/// school number,user number
		/// </summary>
		[M1Card(offset: 4, dataType: CardDataType.STR, length: 12)]
		public String SNO { get; set; }

		/// <summary>
		/// internal card number
		/// </summary>
		public Int32 CardNo { get; set; }

		/// <summary>
		/// expire date of card
		/// </summary>
		public DateTime ExpireDate { get; set; }

		public byte IdType { get; set; }

		public byte CardStatus { get; set; }

		public String Name { get; set; }

		public byte Sex { get; set; }

		public byte Nation { get; set; }

		public byte Country { get; set; }

		/// <summary>
		/// six bytes
		/// </summary>
		public byte[] DeptNo { get; set; }

		public Int32 Balance { get; set; }


		public Int16 Seq { get; set; }

		/// <summary>
		/// 最后餐次累计消费额
		/// </summary>
		public Int16 LastSumByMeal { get; set; }

		/// <summary>
		/// 最后餐次
		/// </summary>
		public byte LastMealNo { get; set; }

		/// <summary>
		/// 最后消费日期
		/// </summary>
		public DateTime LastDate { get; set; }

		/// <summary>
		/// 支付密码
		/// </summary>
		public String PayPwd { get; set; }

		/// <summary>
		/// 餐次最高消费限额
		/// </summary>
		public Int16 SumLmtMeal { get; set; }
	}
}
