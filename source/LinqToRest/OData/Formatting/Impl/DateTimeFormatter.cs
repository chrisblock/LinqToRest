using System;

namespace LinqToRest.OData.Formatting.Impl
{
	public class DateTimeFormatter : AbstractTypeFormatter<DateTime>
	{
		protected override string Format(DateTime value)
		{
			return String.Format("datetime'{0:yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffffff}'", value.ToUniversalTime());
		}
	}
}
