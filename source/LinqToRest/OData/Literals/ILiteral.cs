namespace LinqToRest.OData.Literals
{
	public interface ILiteral
	{
		bool IsContainedIn(string text);
		bool IsAtStart(string text);
		bool MatchesEntireText(string text);
	}
}
