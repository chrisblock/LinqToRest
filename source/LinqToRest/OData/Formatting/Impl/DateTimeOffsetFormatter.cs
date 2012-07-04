using System;

namespace LinqToRest.OData.Formatting.Impl
{
	public class DateTimeOffsetFormatter : AbstractTypeFormatter<DateTimeOffset>
	{
		protected override string Format(DateTimeOffset value)
		{
			return String.Format("datetimeoffset'{0:yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK}'", value);
		}
	}
}
