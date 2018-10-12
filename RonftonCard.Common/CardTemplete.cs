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
		/// sorted by base addr
		/// </summary>
		public StorageItemDescriptor[] StorageItems { get; private set; }

		public CardTemplete()
		{
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

		public int DateSize
		{
			get
			{
				int size = 0;
				this.DataItems.ToList().ForEach(item => size += item.Length);
				return size;
			}
		}


		public static CardTemplete CreateCardTemplete(XmlNode node)
		{
			CardTemplete templete = new CardTemplete();
			templete.TempleteName = node.Attributes["name"].Value;
			templete.TempleteDesc = node.Attributes["desc"].Value;

			templete.DataItems = CreateTempleteItem<DataItemDescriptor>(node.SelectSingleNode("data"), "item").OrderBy(item => item.Offset).ToArray();
			templete.StorageItems = CreateTempleteItem<StorageItemDescriptor>(node.SelectSingleNode("storage"), "addr").OrderBy(item => item.PhysicalAddr).ToArray();

			return templete;
		}

		public static List<RT> CreateTempleteItem<RT>(XmlNode node, String tagName)
		{
			List<RT> items = new List<RT>();

			// node maybe null !!!
			if (node != null)
			{
				foreach (XmlNode n in node.ChildNodes)
				{
					if (XmlNodeType.Element == n.NodeType &&
						n.Name.Equals(tagName, StringComparison.CurrentCultureIgnoreCase))
					{
						items.Add(EntityUtil.CreateEntity<RT>(n));
					}
				}
			}
			return items;
		}

		#region "--- for debug ---"
		public String DbgDataItems()
		{
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