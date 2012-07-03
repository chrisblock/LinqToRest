using System.Collections.Generic;

namespace LinqToRest.OData.Literals
{
	public interface IRegularExpressionTableLexer
	{
		IEnumerable<Token> Tokenize(string text);
	}
}
