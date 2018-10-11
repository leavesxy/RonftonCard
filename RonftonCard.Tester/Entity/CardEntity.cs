using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Tester.Entity
{
	public class CardEntity
	{
		public byte Version { get; set; }
		public String UniversityCode { get; set; }
		public String Sno { get; set; }
		public UInt32 CardNo { get; set; }
		public DateTime ExpireDate { get; set; }
		public byte IdType { get; set; }
		public byte IdStatus { get; set; }
		public String Name { get; set; }
		public byte Sex { get; set; }
		public byte Nation { get; set; }
		public byte Country { get; set; }
		public byte[] DeptNo { get; set; }
		public double Balance { get; set; }
		public short Seq { get; set; }
		public short LastSumByMeal { get; set; }
		public byte LastMealNo { get; set; }
		public DateTime LastDate { get; set; }
		public byte[] PayPwd { get; set; }
		public short SumLmtMeal { get; set; }

		public static CardEntity CreateTestEntity()
		{
			return new CardEntity()
			{
				Version = 0x01,
				UniversityCode = "100001",
				Sno="123456",
				CardNo = 0x12345678,
				ExpireDate = DateTime.ParseExact("20181012", "yyyyMMdd",null),
				IdType=1,
				Name="融付通",
				Sex=0x01,
				Nation=0x01,
				Country=0x01,
				DeptNo=new byte[] { 0x01,0x02,0x03,0x04,0x05,0x06},
				Balance = 100.01,
				Seq=1,
				LastSumByMeal=0,
				LastMealNo=0x01,
				LastDate=DateTime.Now,
				PayPwd= new byte[] {0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00},
				SumLmtMeal = 30
			};
		}

		public override String ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("Version=").Append(this.Version.ToString("X2")).Append(Environment.NewLine);
			sb.Append("UniversityCode=").Append(this.UniversityCode).Append(Environment.NewLine);
			sb.Append("Sno=").Append(this.Sno).Append(Environment.NewLine);
			sb.Append("CardNo=").Append(this.CardNo.ToString("X8")).Append(Environment.NewLine);
			sb.Append("ExpireDate=").Append(this.ExpireDate.ToString("yyyy-MM-dd")).Append(Environment.NewLine);
			sb.Append("IdType=").Append(this.IdType).Append(Environment.NewLine);
			sb.Append("Name=").Append(this.Name).Append(Environment.NewLine);
			sb.Append("Sex=").Append(this.Sex==0x01?"男":"女").Append(Environment.NewLine);

			sb.Append("Nation=").Append(this.Nation).Append(Environment.NewLine);
			sb.Append("Country=").Append(this.Country).Append(Environment.NewLine);
			sb.Append("DeptNo=").Append(BitConverter.ToString(this.DeptNo)).Append(Environment.NewLine);
			sb.Append("Balance=").Append(this.Balance.ToString(".2f")).Append(Environment.NewLine);
			sb.Append("Seq=").Append(this.Seq).Append(Environment.NewLine);
			sb.Append("LastSumByMeal=").Append(this.LastSumByMeal).Append(Environment.NewLine);
			sb.Append("LastMealNo=").Append(this.LastMealNo).Append(Environment.NewLine);
			sb.Append("LastDate=").Append(this.LastDate.ToString("yyyy-MM-dd")).Append(Environment.NewLine);
			sb.Append("PayPwd=").Append(BitConverter.ToString(this.PayPwd)).Append(Environment.NewLine);
			sb.Append("SumLmtMeal=").Append(this.SumLmtMeal).Append(Environment.NewLine);

			return sb.ToString();
		}
	}
}