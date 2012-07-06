// ReSharper disable InconsistentNaming

using LinqToRest.OData.Lexing;
using LinqToRest.OData.Lexing.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Lexing
{
	[TestFixture]
	public class PrimitiveRegularExpressionTableLexerEntryTests
	{
		private IRegularExpressionTableLexerEntry _regularExpressionTableLexerEntry;

		[SetUp]
		public void TestSetUp()
		{
			_regularExpressionTableLexerEntry = new PrimitiveRegularExpressionTableLexerEntry();
		}

		[Test]
		public void TokenType_ReturnsPrimitive()
		{
			Assert.That(_regularExpressionTableLexerEntry.TokenType, Is.EqualTo(TokenType.Primitive));
		}
	}
}
