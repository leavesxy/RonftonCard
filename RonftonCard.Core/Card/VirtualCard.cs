using System;
using System.Collections.Generic;

namespace RonftonCard.Core.Card
{
	using System.Reflection;
	using Bluemoon;
	using DTO;
	using DataTypeHandler;

	public class VirtualCard
	{
		private static IDictionary<String, ICardDataTypeHandler> handler=new Dictionary<String, ICardDataTypeHandler>()
		{
			{ "byte", new ByteTypeHandler() },
			{ "bcdString", new BcdStringTypeHandler() },
			{ "string", new StringTypeHandler() },
			{ "number", new NumberTypeHandler() },
			{ "bool", new BoolTypeHandler() },
			{ "date", new DateTypeHandler() },
		};

		private int size;
		private byte[] buffer;
		public VirtualCard(int size)
		{
			this.size = size;
			this.buffer = ByteUtil.Malloc(size);
		}

		public VirtualCard(byte[] cardData)
		{
			this.size = cardData.Length;
			if (this.size > 0)
			{
				this.buffer = cardData;
			}
		}

		public void Write<T>(T cardInfo) where T : new()
		{
			if (cardInfo == null)
				return;

			foreach (PropertyInfo p in cardInfo.GetType().GetProperties())
			{
				M1CardAttribute m1Attr = p.GetCustomAttribute<M1CardAttribute>();

				if (m1Attr != null && handler.Keys.Contains(m1Attr.DataType))
				{
					ICardDataTypeHandler __handler = handler[m1Attr.DataType];
					byte[] __buffer = __handler.GetBytes(p.GetValue(cardInfo), m1Attr.Length);
					Array.Copy(__buffer, 0, this.buffer, m1Attr.Offset, m1Attr.Length);
				}
			}
		}

		public byte[] DumpBuffer()
		{
			byte[] __buffer = new byte[this.size];
			Array.Copy(this.buffer, 0, __buffer, 0, this.size);
			return __buffer;
		}

		public RT Parse<RT>() where RT:new()
		{
			if (this.size == 0 || this.buffer.IsNullOrEmpty())
				return default(RT);

			RT entity = new RT();

			foreach (PropertyInfo p in entity.GetType().GetProperties())
			{
				M1CardAttribute m1Attr = p.GetCustomAttribute<M1CardAttribute>();

				if (m1Attr != null && handler.Keys.Contains(m1Attr.DataType))
				{
					ICardDataTypeHandler __handler = handler[m1Attr.DataType];
					byte[] __buffer = new byte[m1Attr.Length];
					Array.Copy(buffer, m1Attr.Offset, __buffer, 0, m1Attr.Length);

					p.SetValue(entity, __handler.Parse(p.PropertyType, __buffer));
				}
			}

			return entity;
		}
	}
}