using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LinqToRest.OData.Lexing.Impl
{
	public class RegularExpressionTableLexer : IRegularExpressionTableLexer
	{
		private static readonly Regex LeadingWhiteSpace = new Regex(@"^\s+");

		private readonly IList<IRegularExpressionTableLexerEntry> _tableEntries;

		public RegularExpressionTableLexer()
		{
			_tableEntries = new List<IRegularExpressionTableLexerEntry>
			{
				new LeftParenthesisRegularExpressionTableLexerEntry(),
				new RightParenthesisRegularExpressionTableLexerEntry(),
				new CommaRegularExpressionTableLexerEntry(),
				new NullRegularExpressionTableLexerEntry(),
				new BinaryOperatorRegularExpressionTableLexerEntry(),
				new FunctionRegularExpressionTableLexerEntry(),
				new BooleanRegularExpressionTableLexerEntry(),
				new StringRegularExpressionTableLexerEntry(),
				new GuidRegularExpressionTableLexerEntry(),
				new DateTimeRegularExpressionTableLexerEntry(),
				new DateTimeOffsetRegularExpressionTableLexerEntry(),
				new TimeRegularExpressionTableLexerEntry(),
				new ByteRegularExpressionTableLexerEntry(),
				new ShortRegularExpressionTableLexerEntry(),
				new IntegerRegularExpressionTableLexerEntry(),
				new LongRegularExpressionTableLexerEntry(),
				new FloatRegularExpressionTableLexerEntry(),
				new DoubleRegularExpressionTableLexerEntry(),
				new DecimalRegularExpressionTableLexerEntry(),
				new PrimitiveCollectionRegularExpressionTableLexerEntry(),
				new PrimitiveRegularExpressionTableLexerEntry(),
				new NameRegularExpressionTableLexerEntry()
			};
		}

		public IEnumerable<Token> Tokenize(string text)
		{
			if (String.IsNullOrWhiteSpace(text))
			{
				throw new ArgumentNullException("text", String.Format("Cannot tokenize string '{0}'.  It is invalid.", text));
			}

			var copyOfText = text.Substring(0);

			var result = new List<Token>();

			while (String.IsNullOrWhiteSpace(copyOfText) == false)
			{
				ConsumeWhitespace(ref copyOfText);

				foreach (var tableEntry in _tableEntries)
				{
					if (tableEntry.IsAtStart(copyOfText))
					{
						Token token;
						if (tableEntry.TryConsume(ref copyOfText, out token))
						{
							result.Add(token);

							break;
						}
					}
				}
			}

			return result;
		}

		private static void ConsumeWhitespace(ref string text)
		{
			var match = LeadingWhiteSpace.Match(text);

			if (match.Success)
			{
				var matchedWhiteSpace = match.Groups.Cast<Group>().Single();

				text = text.Substring(matchedWhiteSpace.Length);
			}
		}
	}
}
