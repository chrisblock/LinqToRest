// ReSharper disable InconsistentNaming

using LinqToRest.OData.Lexing;
using LinqToRest.OData.Lexing.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Lexing
{
	[TestFixture]
	public class MemberAccessRegularExpressionTableLexerEntryTests
	{
		private IRegularExpressionTableLexerEntry _regularExpressionTableLexerEntry;

		[SetUp]
		public void TestSetUp()
		{
			_regularExpressionTableLexerEntry = new MemberAccessRegularExpressionTableLexerEntry();
		}

		[Test]
		public void TokenType_ReturnsMemberAccess()
		{
			Assert.That(_regularExpressionTableLexerEntry.TokenType, Is.EqualTo(TokenType.MemberAccess));
		}
	}
}
