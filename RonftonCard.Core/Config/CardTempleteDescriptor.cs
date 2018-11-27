using Bluemoon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.Config
{
	public class CardTempleteDescriptor
	{
		[Alias("name")]
		public String TempleteName { get; private set; }

		[Alias("desc")]
		public String TempleteDesc { get; private set; }

		[Alias("data")]
		public TempleteDataDescriptor[] DataDescriptor { get; set; }

		[Alias("storage")]
		public TempleteStorageDescriptor[] StorageDescriptor { get; set; }

		//////////////////////////////////////////////////////////////////////////////////////////

		/// <summary>
		/// storage size
		/// </summary>
		public int StorageSize
		{
			get
			{
				int size = 0;
				Array.ForEach(this.StorageDescriptor, item => size += item.Size);
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
				Array.ForEach(this.DataDescriptor, item => size += item.Length);
				return size;
			}
		}

		/// <summary>
		/// segment of storage
		///		M1 :	sectors
		///		CPU:	file descriptors
		///		Flash : segment address
		/// </summary>
		public UInt16[] SegmentAddr
		{
			get
			{
				var result = this.StorageDescriptor.Select(p => p.Address);
				return result.GroupBy(p => p).Select(p => p.Key).ToArray();
			}
		}

		//////////////////////////////////////////////////////////////////////////////////////////

		public TempleteDataDescriptor GetTempleteDataDescriptor(String name)
		{
			if (this.DataDescriptor.IsNullOrEmpty())
				return null;

			return this.DataDescriptor.FirstOrDefault(d => d.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
		}

		#region "--- for debug ---"
		public String GetDataItemInfo()
		{
			if (this.DataDescriptor.IsNullOrEmpty())
				return "Card DataDescriptor is null ! please check configuration!";

			StringBuilder sb = new StringBuilder();
			//sb.Append("Card data structure :" + Environment.NewLine);
			//sb.Append("------------------------------------------------------------------------------------" + Environment.NewLine);

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

			//sb.Append("------------------------------------------------------------------------------------" + Environment.NewLine);
			sb.Append("Card Structure total Size : " + size.ToString() + Environment.NewLine);
			return sb.ToString();
		}
		
		public String GetStorageInfo()
		{
			if (this.StorageDescriptor.IsNullOrEmpty())
				return "Card StorageDescriptor is null, Please check configuration!";

			StringBuilder sb = new StringBuilder();
			//sb.Append("Card Storage table" + Environment.NewLine);
			//sb.Append("------------------------------------------------------------------------------------" + Environment.NewLine);

			int size = 0;

			for (int i = 0; i < this.StorageDescriptor.Length; i++)
			{
				size += this.StorageDescriptor[i].Size;
				sb.Append(this.StorageDescriptor[i].ToString());
			}

			//sb.Append("------------------------------------------------------------------------------------" + Environment.NewLine);
			sb.Append("Card Storage total Size : " + size.ToString() + Environment.NewLine);
			return sb.ToString();
		}
		#endregion
	}
}
