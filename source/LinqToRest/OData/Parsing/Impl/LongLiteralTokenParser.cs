using System;

namespace LinqToRest.OData.Parsing.Impl
{
	public class LongLiteralTokenParser : AbstractLiteralTokenParser<long>
	{
		protected override long Parse(string text)
		{
			long result;

			if (Int64.TryParse(text, out result) == false)
			{
				throw new ArgumentException(String.Format("'{0}' could not be parsed as a {1}.", text, Type));
			}

			return result;
		}
	}
}
