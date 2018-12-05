using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RonftonCard.Core
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class M1CardAttribute : Attribute
	{
		public int Offset { get; private set; }

		public CardDataType DataType { get; private set; }

		public int Length { get; private set; }

		public String Description { get; private set; }

		public M1CardAttribute(int offset, CardDataType dataType, int length)
			: this(offset, dataType, length, null)
		{
		}

		public M1CardAttribute(int offset, CardDataType dataType, int length, String desc)
		{
			this.Offset = offset;
			this.DataType = dataType;
			this.Length = length;
			this.Description = desc;
		}

		public static M1CardAttribute GetM1Info(PropertyInfo prop)
		{
			M1CardAttribute[] attrs = prop.GetCustomAttributes<M1CardAttribute>(false) as M1CardAttribute[];

			if (attrs != null && attrs.Length > 0)
				return attrs[0];

			return null;
		}
	}
}