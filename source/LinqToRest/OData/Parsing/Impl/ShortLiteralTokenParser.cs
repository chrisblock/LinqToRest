using System;

namespace LinqToRest.OData.Parsing.Impl
{
	public class ShortLiteralTokenParser : AbstractLiteralTokenParser<short>
	{
		protected override short Parse(string text)
		{
			short result;

			if (Int16.TryParse(text, out result) == false)
			{
				throw new ArgumentException(String.Format("'{0}' could not be parsed as a {1}.", text, Type));
			}

			return result;
		}
	}
}
