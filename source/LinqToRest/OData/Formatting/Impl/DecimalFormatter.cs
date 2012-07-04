using System;

namespace LinqToRest.OData.Formatting.Impl
{
	public class DecimalFormatter : AbstractTypeFormatter<decimal>
	{
		protected override string Format(decimal value)
		{
			return String.Format("{0}m", value);
		}
	}
}
