// ReSharper disable InconsistentNaming

using LinqToRest.OData.Lexing;
using LinqToRest.Server.OData.Lexing;
using LinqToRest.Server.OData.Lexing.Impl;

using NUnit.Framework;

namespace LinqToRest.Server.Tests.Lexing
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
