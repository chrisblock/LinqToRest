using System;

namespace LinqToRest.OData.Parsing.Impl
{
	public class DecimalLiteralTokenParser : AbstractLiteralTokenParser<decimal>
	{
		protected override decimal Parse(string text)
		{
			var decimalLiteral = text.SubString(0, -1);

			decimal result;

			if (Decimal.TryParse(decimalLiteral, out result) == false)
			{
				throw new ArgumentException(String.Format("'{0}' could not be parsed as a {1}.", text, Type));
			}

			return result;
		}
	}
}
