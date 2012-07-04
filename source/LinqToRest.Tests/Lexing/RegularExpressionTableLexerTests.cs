// ReSharper disable InconsistentNaming

using System;
using System.Linq;

using LinqToRest.OData.Lexing;
using LinqToRest.OData.Lexing.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Lexing
{
	[TestFixture]
	public class RegularExpressionTableLexerTests
	{
		private IRegularExpressionTableLexer _tableLexer;

		[SetUp]
		public void TestSetUp()
		{
			_tableLexer = new RegularExpressionTableLexer();
		}

		[Test]
		public void Tokenize_NullString_ThrowsException()
		{
			Assert.That(() => _tableLexer.Tokenize(null), Throws.InstanceOf<ArgumentNullException>());
		}

		[Test]
		public void Tokenize_EmptyString_ThrowsException()
		{
			Assert.That(() => _tableLexer.Tokenize(String.Empty), Throws.InstanceOf<ArgumentNullException>());
		}

		[Test]
		public void Tokenize_ValidString_ReturnsCorrectTokenCollection()
		{
			var result = _tableLexer.Tokenize("null ne 'hello world'").ToList();

			Assert.That(result.Count, Is.EqualTo(3));
			Assert.That(result[0].TokenType, Is.EqualTo(TokenType.Null));
			Assert.That(result[0].Value, Is.EqualTo("null"));

			Assert.That(result[1].TokenType, Is.EqualTo(TokenType.BinaryOperator));
			Assert.That(result[1].Value, Is.EqualTo("ne"));

			Assert.That(result[2].TokenType, Is.EqualTo(TokenType.String));
			Assert.That(result[2].Value, Is.EqualTo("'hello world'"));
		}
	}
}
