using System.Collections.Generic;

namespace LinqToRest.OData.Lexing
{
	public interface IRegularExpressionTableLexer
	{
		IEnumerable<Token> Tokenize(string text);
	}
}
