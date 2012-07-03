using LinqToRest.OData.Filters;
using LinqToRest.OData.Literals;

namespace LinqToRest.OData.Parsing
{
	public interface ILiteralTokenParser
	{
		FilterExpression Parse(Token token);
	}
}
