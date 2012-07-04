using System;

namespace LinqToRest.OData.Parsing.Impl
{
	public class FloatFilterExpressionParserStrategy : AbstractFilterExpressionParserStrategy<float>
	{
		protected override float Parse(string text)
		{
			float result;

			// peel off the 'f' qualifier..
			var floatLiteral = text.SubString(0, -1);

			if (Single.TryParse(floatLiteral, out result) == false)
			{
				throw new ArgumentException(String.Format("'{0}' could not be parsed as a {1}.", text, Type));
			}

			return result;
		}
	}
}
