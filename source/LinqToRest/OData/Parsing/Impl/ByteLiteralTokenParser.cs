using System;

namespace LinqToRest.OData.Parsing.Impl
{
	public class ByteLiteralTokenParser : AbstractLiteralTokenParser<byte>
	{
		protected override byte Parse(string text)
		{
			byte result;

			if (Byte.TryParse(text, out result) == false)
			{
				throw new ArgumentException(String.Format("'{0}' could not be parsed as a {1}.", text, Type));
			}

			return result;
		}
	}
}
