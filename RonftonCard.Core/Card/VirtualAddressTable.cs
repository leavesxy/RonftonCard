﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core.Card
{
	public class VirtualAddressTable
	{
		public List<VirtualAddressUnit> virtualAddressTable;

		public VirtualAddressTable()
		{
		}

		public int Size
		{
			get { return virtualAddressTable.Sum(v => v.Size); }
		}

		public Boolean IsValid()
		{
			VirtualAddressUnit[] va = this.virtualAddressTable.OrderBy(v => v.BlockNo).ToArray();

			// rule
			for (int i = 0; i < va.Length; i++)
			{
				if (!va[i].IsValid())
					return false;

				if (i > 0)
				{
					// address overlap
					if (va[i].Offset < va[i - 1].Offset + va[i - 1].Size)
						return false;

					// Block No duplication
					if (va[i].BlockNo == va[i - 1].BlockNo || va[i].LogicBlockNo == va[i - 1].LogicBlockNo)
						return false;
				}
			}

			return true;
		}
	}
}
