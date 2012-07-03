using LinqToRest.OData.Filters;
using LinqToRest.OData.Literals;

namespace LinqToRest.OData.Parsing.Impl
{
	public class NameLiteralTokenParser : ILiteralTokenParser
	{
		public FilterExpression Parse(Token token)
		{
			return FilterExpression.MemberAccess(token.Value);
		}
	}
}
