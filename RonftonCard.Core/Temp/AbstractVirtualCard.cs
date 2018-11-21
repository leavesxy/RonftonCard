﻿using System;
using System.Reflection;
using Bluemoon;
using RonftonCard.Core.Config;
using RonftonCard.Core.Util;

namespace RonftonCard.Core
{
	public abstract class AbstractVirtualCard
	{
		protected byte[] virtualBuffer;
		protected CardContext cardContext;

		protected AbstractVirtualCard(CardContext cardContext)
		{
			this.cardContext = cardContext;
			InitVirtualCard();
		}

		public bool InitVirtualCard()
		{
			this.virtualBuffer = new byte[this.cardContext.CardTemplete.StorageSize];
			return true;
		}

		#region "--- virtual card operator ---"
		public bool WriteEntity<T>(T entity)
		{
			if (entity == null)
				return false;

			PropertyInfo[] props = typeof(T).GetProperties();
			foreach (PropertyInfo p in props)
			{
				String name = p.GetAliasName() ?? p.Name;

				TempleteDataDescriptor descriptor = this.cardContext.CardTemplete.GetTempleteDataDescriptor(name);

				if (descriptor != null)
				{
					byte[] b = ArrayUtil.CopyFrom(DataTypeHelper.ToByte(p.GetValue(entity), descriptor.DataType), descriptor.Length);
					if (b != null)
					{
						Write(descriptor.Offset, b);
					}
				}
			}
			return true;
		}

		public bool Write(int offset, byte[] buffer)
		{
			return Write(offset, buffer, buffer.Length);
		}

		public bool Write(int offset, byte[]buffer, int length)
		{
			Array.Copy(buffer, 0, this.virtualBuffer, offset, length);
			return true;
		}
		#endregion

		#region "--- abstract ---"

		public abstract String DbgBuffer();

		public abstract String DbgArgs();
		
		#endregion
	}
}