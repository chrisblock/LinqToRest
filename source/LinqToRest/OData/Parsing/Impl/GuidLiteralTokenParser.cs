using System;

namespace LinqToRest.OData.Parsing.Impl
{
	public class GuidLiteralTokenParser : AbstractLiteralTokenParser<Guid>
	{
		protected override Guid Parse(string text)
		{
			var guidLiteral = text.SubString(5, -1);

			Guid result;
			if (Guid.TryParseExact(guidLiteral, "D", out result) == false)
			{
				throw new ArgumentException(String.Format("'{0}' could not be parsed as a {1}.", text, Type));
			}

			return result;
		}
	}
}
