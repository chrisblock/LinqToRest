// ReSharper disable InconsistentNaming

using LinqToRest.OData.Lexing;
using LinqToRest.Server.OData.Lexing;
using LinqToRest.Server.OData.Lexing.Impl;

using NUnit.Framework;

namespace LinqToRest.Server.Tests.Lexing
{
	[TestFixture]
	public class LeftParenthesisRegularExpressionTableLexerEntryTests
	{
		private IRegularExpressionTableLexerEntry _regularExpressionTableLexerEntry;

		[SetUp]
		public void TestSetUp()
		{
			_regularExpressionTableLexerEntry = new LeftParenthesisRegularExpressionTableLexerEntry();
		}

		[Test]
		public void TokenType_ReturnsLeftParenthesis()
		{
			Assert.That(_regularExpressionTableLexerEntry.TokenType, Is.EqualTo(TokenType.LeftParenthesis));
		}
	}
}
