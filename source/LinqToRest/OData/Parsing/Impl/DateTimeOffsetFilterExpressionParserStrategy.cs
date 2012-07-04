using System;
using System.Globalization;

namespace LinqToRest.OData.Parsing.Impl
{
	public class DateTimeOffsetFilterExpressionParserStrategy : AbstractFilterExpressionParserStrategy<DateTimeOffset>
	{
		protected override DateTimeOffset Parse(string text)
		{
			var dateTimeOffsetLiteral = text.SubString(15, -1);

			DateTimeOffset result;
			if (DateTimeOffset.TryParseExact(dateTimeOffsetLiteral, "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK", null, DateTimeStyles.AssumeUniversal, out result) == false)
			{
				throw new ArgumentException(String.Format("'{0}' could not be parsed as a {1}.", text, Type));
			}

			return result;
		}
	}
}
