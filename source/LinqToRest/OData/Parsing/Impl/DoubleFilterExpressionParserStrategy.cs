using System;

namespace LinqToRest.OData.Parsing.Impl
{
	public class DoubleFilterExpressionParserStrategy : AbstractFilterExpressionParserStrategy<double>
	{
		protected override double Parse(string text)
		{
			double result;

			if (Double.TryParse(text, out result) == false)
			{
				throw new ArgumentException(String.Format("'{0}' could not be parsed as a {1}.", text, Type));
			}

			return result;
		}
	}
}
