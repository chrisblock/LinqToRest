using System;

namespace LinqToRest.OData.Parsing.Impl
{
	public class StringFilterExpressionParserStrategy : AbstractFilterExpressionParserStrategy<string>
	{
		protected override string Parse(string text)
		{
			if (String.IsNullOrWhiteSpace(text))
			{
				throw new ArgumentException(String.Format("'{0}' could not be parsed as a {1}.", text, Type));
			}

			var result = text.SubString(1, -1).Replace("\\'", "'");

			return result;
		}
	}
}
