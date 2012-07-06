// ReSharper disable InconsistentNaming

using LinqToRest.OData.Lexing;
using LinqToRest.OData.Lexing.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Lexing
{
	[TestFixture]
	public class RightParenthesisRegularExpressionTableLexerEntryTests
	{
		private IRegularExpressionTableLexerEntry _regularExpressionTableLexerEntry;

		[SetUp]
		public void TestSetUp()
		{
			_regularExpressionTableLexerEntry = new RightParenthesisRegularExpressionTableLexerEntry();
		}

		[Test]
		public void TokenType_ReturnsRightParenthesis()
		{
			Assert.That(_regularExpressionTableLexerEntry.TokenType, Is.EqualTo(TokenType.RightParenthesis));
		}
	}
}
