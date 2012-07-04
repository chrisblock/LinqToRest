using System;

namespace LinqToRest.OData.Formatting.Impl
{
	public class LongFormatter : AbstractTypeFormatter<long>
	{
		protected override string Format(long value)
		{
			return String.Format("{0}L", value);
		}
	}
}
