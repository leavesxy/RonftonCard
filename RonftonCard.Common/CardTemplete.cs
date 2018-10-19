using RonftonCard.Common.Entity;
using RonftonCard.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RonftonCard.Common
{
	public class CardTemplete
	{
		public String TempleteName { get; private set; }
		public String TempleteDesc { get; private set; }

		/// <summary>
		/// sorted by offset
		/// </summary>
		public DataItemDescriptor[] DataItems { get; private set; }

		/// <summary>
		/// sorted by physical addr
		/// </summary>
		public StorageItemDescriptor[] StorageItems { get; private set; }

		protected CardTemplete()
		{
		}

		public int[] GetCardPhysicalAddr()
		{
			List<int> physicalAddrList = new List<int>();
			foreach(StorageItemDescriptor item in this.StorageItems)
			{
				physicalAddrList.Add(item.PhysicalAddr);
			}
			// remove same physical address
			return physicalAddrList.ToArray().GroupBy(p => p).Select(p => p.Key).ToArray();
		}

		public int CardSize
		{
			get
			{
				int size = 0;
				this.StorageItems.ToList().ForEach(item => size += item.Size);
				return size;
			}
		}

		public int DataSize
		{
			get
			{
				int size = 0;
				this.DataItems.ToList().ForEach(item => size += item.Length);
				return size;
			}
		}

		#region "--- util ---"
		public static CardTemplete CreateCardTemplete(XmlNode node)
		{
			CardTemplete templete = new CardTemplete();
			templete.TempleteName = node.GetAttributeValue("name","Unknown");
			templete.TempleteDesc = node.GetAttributeValue("desc","");

			templete.DataItems = ConfigureUtil.CreateItem<DataItemDescriptor>(node.SelectSingleNode("data"), "item").OrderBy(item => item.Offset).ToArray();
			templete.StorageItems = ConfigureUtil.CreateItem<StorageItemDescriptor>(node.SelectSingleNode("storage"), "addr").OrderBy(item => item.PhysicalAddr).ToArray();

			return templete;
		}

		#endregion

		#region "--- for debug ---"
		public String DbgDataItems()
		{
			if (this.DataItems == null || this.DataItems.Length == 0)
				return "Card data structure is null ! please check configuration!";

			StringBuilder sb = new StringBuilder();
			sb.Append("Card data structure :" + Environment.NewLine);
			sb.Append("------------------------------------------------------------------------------------" + Environment.NewLine);

			int size = 0;

			for (int i = 0; i < this.DataItems.Length; i++)
			{
				size += this.DataItems[i].Length;

				if (i > 0 &&
					(this.DataItems[i].Offset < this.DataItems[i - 1].Offset + this.DataItems[i - 1].Length))
				{
					// there is a address conflict!!!
					sb.Append("[*] ");
				}
				else
					sb.Append("    ");
				sb.Append(this.DataItems[i].ToString());
			}

			sb.Append("------------------------------------------------------------------------------------" + Environment.NewLine);
			sb.Append("Card Structure total Size : " + size.ToString() + Environment.NewLine);
			return sb.ToString();
		}

		public String DbgStorageItems()
		{
			if (this.StorageItems == null || this.StorageItems.Length == 0)
				return "Card storage is null, Please check configuration!";

			StringBuilder sb = new StringBuilder();
			sb.Append("Card Storage table" + Environment.NewLine);
			sb.Append("------------------------------------------------------------------------------------" + Environment.NewLine);

			int size = 0;

			for (int i = 0; i < this.StorageItems.Length; i++)
			{
				size += this.StorageItems[i].Size;
				sb.Append(this.StorageItems[i].ToString());
			}

			sb.Append("------------------------------------------------------------------------------------" + Environment.NewLine);
			sb.Append("Card Storage total Size : " + size.ToString() + Environment.NewLine);
			return sb.ToString();
		}
		#endregion

	}
}