using System;

namespace LinqToRest.OData.Parsing.Impl
{
	public class BooleanFilterExpressionParserStrategy : AbstractFilterExpressionParserStrategy<bool>
	{
		protected override bool Parse(string text)
		{
			bool result;

			if (Boolean.TryParse(text, out result) == false)
			{
				throw new ArgumentException(String.Format("'{0}' could not be parsed as a {1}.", text, Type));
			}

			return result;
		}
	}
}
