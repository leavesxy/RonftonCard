using System;


namespace RonftonCard.Core.Util
{
	using Bluemoon;
	public class ComboxDisplayBindingItem
	{
		public String Name { get; set; }

		[Alias("desc")]
		public String Description { get; set; }

		[Alias("idx")]
		public int Index { get; set; }

	}
}