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
		[M1Card(offset:0, dataType: "byte",length:1)]
		public byte Version { get; set; }

		/// <summary>
		/// university Code, 6 digit
		/// </summary>
		[M1Card(offset: 1, dataType: "bcdString", length: 3)]
		public String UniversityCode { get; set; }

		/// <summary>
		/// school number,user number
		/// </summary>
		[M1Card(offset: 4, dataType: "string", length: 12)]
		public String SNO { get; set; }

        /// <summary>
        /// internal card number
        /// </summary>
        [M1Card(offset: 16, dataType: "number", length: 4)]
        public Int32 CardNo { get; set; }

        /// <summary>
        /// expire date of card
        /// </summary>
        [M1Card(offset: 20, dataType: "date", length: 3)]
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

        public CardInfo()
        {
            this.LastDate = DateTime.Now;
            this.ExpireDate = DateTime.Now;
        }


        public static CardInfo CreateTestCardInfo()
		{
			CardInfo cardInfo = new CardInfo()
			{
				Version = (byte)0x10,	//1.0
				UniversityCode = "012345",
				SNO = "201812060001",
				CardNo = 1,			//internal card no
				ExpireDate = new DateTime(2020, 12, 31),
				IdType = 0x01,		//teacher
				CardStatus = 0x00,	//normal
				Name = "Test",
				Sex = 0x01,			//male
				Nation = 0x01,		//Han
				Country = 0x01,		//chinese
				DeptNo = new byte[] { 0x01, 0x23, 0x45, 0x67, 0x89, 0xab },
				Balance = 100,		//1.00
				Seq = 1,
				LastSumByMeal = 100,   //1.00
				LastMealNo = 0x03,     //supper
				LastDate = new DateTime(2018, 12, 6),
				PayPwd = "012345",
				SumLmtMeal = 5000,     //5.00
			};
			return cardInfo;
		}

		public override String ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("version = 0x").Append(this.Version.ToString("X2") ).Append(Environment.NewLine);
			sb.Append("UniversityCode = ").Append(this.UniversityCode).Append(Environment.NewLine);
			sb.Append("SNO = ").Append(this.SNO).Append(Environment.NewLine);
			sb.Append("CardNo = ").Append(this.CardNo.ToString()).Append(Environment.NewLine);
			sb.Append("ExpireDate = ").Append(this.ExpireDate.ToString("yyyy-MM-dd HH:mm:ss")).Append(Environment.NewLine);
			sb.Append("IdType = ").Append(this.IdType.ToString()).Append(Environment.NewLine);
			sb.Append("CardStatus = ").Append(this.CardStatus.ToString()).Append(Environment.NewLine);
			sb.Append("Name = ").Append(this.Name??"null").Append(Environment.NewLine);
			sb.Append("Sex = ").Append(this.Sex.ToString()).Append(Environment.NewLine);
			sb.Append("Nation = ").Append(this.Nation.ToString()).Append(Environment.NewLine);
			sb.Append("Country = ").Append(this.Country.ToString()).Append(Environment.NewLine);
			sb.Append("DeptNo = ").Append(this.DeptNo == null ? "null" : BitConverter.ToString(this.DeptNo)).Append(Environment.NewLine);
			sb.Append("Balance = ").Append(this.Balance.ToString()).Append(Environment.NewLine);
			sb.Append("Seq = ").Append(this.Seq.ToString()).Append(Environment.NewLine);
			sb.Append("LastSumByMeal = ").Append(this.LastSumByMeal.ToString()).Append(Environment.NewLine);
			sb.Append("LastMealNo = ").Append(this.LastMealNo.ToString()).Append(Environment.NewLine);
			sb.Append("LastDate = ").Append(this.LastDate.ToString("yyyy-MM-dd HH:mm:ss")).Append(Environment.NewLine);
			sb.Append("PayPwd = ").Append(this.PayPwd ?? "null").Append(Environment.NewLine);
			sb.Append("SumLmtMeal = ").Append(this.SumLmtMeal.ToString()).Append(Environment.NewLine);
			return sb.ToString();
		}
	}
}
