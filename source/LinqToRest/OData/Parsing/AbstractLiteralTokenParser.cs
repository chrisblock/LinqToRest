using System;

using LinqToRest.OData.Filters;
using LinqToRest.OData.Literals;

namespace LinqToRest.OData.Parsing
{
	public abstract class AbstractLiteralTokenParser<T> : ILiteralTokenParser
	{
		protected Type Type { get { return typeof (T); } }

		protected abstract T Parse(string text);

		public FilterExpression Parse(Token token)
		{
			if (token == null)
			{
				throw new ArgumentNullException("token", "Cannot parse a null token.");
			}

			var value = Parse(token.Value);
			var type = typeof (T);

			return FilterExpression.Constant(value, type);
		}
	}
}
