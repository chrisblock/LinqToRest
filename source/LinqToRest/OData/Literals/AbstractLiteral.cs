using System;
using System.Text.RegularExpressions;

namespace LinqToRest.OData.Literals
{
	public abstract class AbstractLiteral : ILiteral
	{
		private readonly Regex _expression;
		private readonly Regex _startsWithExpresion;
		private readonly Regex _entireTextExpression;

		protected AbstractLiteral(string expression) : this(expression, true)
		{
		}

		protected AbstractLiteral(string expression, bool ignoreCase)
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
	}
}
