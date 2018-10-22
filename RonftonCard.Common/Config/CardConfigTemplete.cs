using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueMoon;
using System.Xml;
using RonftonCard.Common.Util;

namespace RonftonCard.Common.Config
{
	public class CardConfigTemplete
	{
		public CardConfigTemplete()
		{
		}

		#region "--- properties ---"

		[Alias("name")]
		public String TempleteName { get; private set; }

		[Alias("desc")]
		public String TempleteDesc { get; private set; }

		[Alias("data")]
		public CardDataDescriptor[] DataDescriptor { get; set; }

		[Alias("storage")]
		public CardStorageDescriptor[] StorageDescriptor { get; set; }

		/// <summary>
		/// stroage size for store data
		/// </summary>
		public int CardSize
		{
			get
			{
				int size = 0;
				this.StorageDescriptor.ToList().ForEach(item => size += item.Size);
				return size;
			}
		}

		/// <summary>
		/// data size
		/// </summary>
		public int DataSize
		{
			get
			{
				int size = 0;
				this.DataDescriptor.ToList().ForEach(item => size += item.Length);
				return size;
			}
		}

		/// <summary>
		/// return segment of storage
		///		M1 :	sectors
		///		CPU:	file descriptors
		///		Flash : segment address
		/// </summary>
		public int[] SegmentAddr
		{
			get
			{
				var result = this.StorageDescriptor.Select(p => p.Address);
				return result.GroupBy(p => p).Select(p => p.Key).ToArray();
			}
		}
		#endregion

		public CardDataDescriptor GetCardDataDescriptor(String name)
		{
			if ( this.DataDescriptor.IsNullOrEmpty() )
				return null;

			return this.DataDescriptor.FirstOrDefault(d => d.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
		}

		public static CardConfigTemplete Create(XmlNode node)
		{
			CardConfigTemplete templete = new CardConfigTemplete();
			templete.TempleteName = ((XmlElement)node).GetAttrValue("name","Unknown");
			templete.TempleteDesc = ((XmlElement)node).GetAttrValue("desc", "");

			templete.DataDescriptor = ConfigureUtil.CreateItem<CardDataDescriptor>(node.SelectSingleNode("data"), "item").OrderBy(item => item.Offset).ToArray();
			templete.StorageDescriptor = ConfigureUtil.CreateItem<CardStorageDescriptor>(node.SelectSingleNode("storage"), "addr").OrderBy(item => item.Address).ToArray();

			return templete;
		}

		#region "--- debug ---"
		public String DbgDataDescriptor()
		{
			if (this.DataDescriptor.IsNullOrEmpty() )
				return "Card DataDescriptor is null ! please check configuration!";

			StringBuilder sb = new StringBuilder();
			sb.Append("Card data structure :" + Environment.NewLine);
			sb.Append("------------------------------------------------------------------------------------" + Environment.NewLine);

			int size = 0;

			for (int i = 0; i < this.DataDescriptor.Length; i++)
			{
				size += this.DataDescriptor[i].Length;

				if (i > 0 &&
					(this.DataDescriptor[i].Offset < this.DataDescriptor[i - 1].Offset + this.DataDescriptor[i - 1].Length))
				{
					// there is a address conflict!!!
					sb.Append("[*] ");
				}
				else
					sb.Append("    ");
				sb.Append(this.DataDescriptor[i].ToString());
			}

			sb.Append("------------------------------------------------------------------------------------" + Environment.NewLine);
			sb.Append("Card Structure total Size : " + size.ToString() + Environment.NewLine);
			return sb.ToString();
		}

		public String DbgStorageDescriptor()
		{
			if (this.StorageDescriptor.IsNullOrEmpty())
				return "Card StorageDescriptor is null, Please check configuration!";

			StringBuilder sb = new StringBuilder();
			sb.Append("Card Storage table" + Environment.NewLine);
			sb.Append("------------------------------------------------------------------------------------" + Environment.NewLine);

			int size = 0;

			for (int i = 0; i < this.StorageDescriptor.Length; i++)
			{
				size += this.StorageDescriptor[i].Size;
				sb.Append(this.StorageDescriptor[i].ToString());
			}

			sb.Append("------------------------------------------------------------------------------------" + Environment.NewLine);
			sb.Append("Card Storage total Size : " + size.ToString() + Environment.NewLine);
			return sb.ToString();
		}
		#endregion
	}
}
