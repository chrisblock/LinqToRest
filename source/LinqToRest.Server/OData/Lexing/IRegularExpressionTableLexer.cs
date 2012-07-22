using System.Collections.Generic;

using LinqToRest.OData.Lexing;

namespace LinqToRest.Server.OData.Lexing
{
	public interface IRegularExpressionTableLexer
	{
		IEnumerable<Token> Tokenize(string text);
	}
}
