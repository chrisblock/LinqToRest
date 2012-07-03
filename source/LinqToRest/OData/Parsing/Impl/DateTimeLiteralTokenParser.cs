using System;
using System.Globalization;

namespace LinqToRest.OData.Parsing.Impl
{
	public class DateTimeLiteralTokenParser : AbstractLiteralTokenParser<DateTime>
	{
		protected override DateTime Parse(string text)
		{
			var dateTimeLiteral = text.SubString(9, -1);

			DateTime result;
			if (DateTime.TryParseExact(dateTimeLiteral, "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffffff", null, DateTimeStyles.AssumeUniversal, out result) == false)
			{
				throw new ArgumentException(String.Format("'{0}' could not be parsed as a {1}.", text, Type));
			}

			return result;
		}
	}
}
