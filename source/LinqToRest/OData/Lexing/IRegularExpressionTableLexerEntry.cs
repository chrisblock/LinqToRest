namespace LinqToRest.OData.Lexing
{
	public interface IRegularExpressionTableLexerEntry
	{
		bool IsContainedIn(string text);
		bool IsAtStart(string text);
		bool MatchesEntireText(string text);
		bool TryConsume(ref string text, out Token token);
	}
}
