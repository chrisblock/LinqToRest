using System;

namespace LinqToRest.OData.Parsing.Impl
{
	public class FloatLiteralTokenParser : AbstractLiteralTokenParser<float>
	{
		protected override float Parse(string text)
		{
			float result;

			if (Single.TryParse(text, out result) == false)
			{
				throw new ArgumentException(String.Format("'{0}' could not be parsed as a {1}.", text, Type));
			}

			return result;
		}
	}
}
