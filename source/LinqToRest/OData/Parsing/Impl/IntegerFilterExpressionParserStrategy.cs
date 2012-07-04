using System;

namespace LinqToRest.OData.Parsing.Impl
{
	public class IntegerFilterExpressionParserStrategy : AbstractFilterExpressionParserStrategy<int>
	{
		protected override int Parse(string text)
		{
			int result;

			if (Int32.TryParse(text, out result) == false)
			{
				throw new ArgumentException(String.Format("'{0}' could not be parsed as a {1}.", text, Type));
			}

			return result;
		}
	}
}
