using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace LinqToRest.OData.Literals
{
	public abstract class AbstractRegularExpressionTableLexerEntry : IRegularExpressionTableLexerEntry
	{
		private readonly Regex _expression;
		private readonly Regex _startsWithExpresion;
		private readonly Regex _entireTextExpression;

		public abstract TokenType TokenType { get; }

		protected AbstractRegularExpressionTableLexerEntry(string expression) : this(expression, true)
		{
		}

		protected AbstractRegularExpressionTableLexerEntry(string expression, bool ignoreCase)
		{
			var options = ignoreCase
				? RegexOptions.IgnoreCase
				: RegexOptions.None;

			_expression = new Regex(expression, options);
			_startsWithExpresion = new Regex(String.Format(@"^{0}", expression), options);
			_entireTextExpression = new Regex(String.Format("^{0}$", expression), options);
		}

		public bool IsContainedIn(string text)
		{
			return _expression.IsMatch(text);
		}

		public bool IsAtStart(string text)
		{
			return _startsWithExpresion.IsMatch(text);
		}

		public bool MatchesEntireText(string text)
		{
			return _entireTextExpression.IsMatch(text);
		}

		public bool TryConsume(ref string text, out Token token)
		{
			var result = false;
			token = null;

			var match = _startsWithExpresion.Match(text);

			if (match.Success)
			{
				var matchedText = match.Groups.Cast<Group>().Single().Value;

				text = text.Substring(matchedText.Length);

				token = new Token
				{
					TokenType = TokenType,
					Value = matchedText
				};

				result = true;
			}

			return result;
		}
	}
}
