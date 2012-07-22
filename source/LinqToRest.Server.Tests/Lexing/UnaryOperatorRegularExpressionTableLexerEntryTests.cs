// ReSharper disable InconsistentNaming

using System;
using System.Linq;
using System.Security.Cryptography;

using LinqToRest.OData.Lexing;
using LinqToRest.Server.OData.Lexing;
using LinqToRest.Server.OData.Lexing.Impl;

using NUnit.Framework;

namespace LinqToRest.Server.Tests.Lexing
{
	[TestFixture]
	public class UnaryOperatorRegularExpressionTableLexerEntryTests
	{
		private static readonly string[] Operators = UnaryOperatorRegularExpressionTableLexerEntry.Operators.ToArray();

		private IRegularExpressionTableLexerEntry _regularExpressionTableLexerEntry;

		private static string BuildLiteralString()
		{
			var result = String.Format("{0}", RandomOperator());

			Console.WriteLine("Generated operator: '{0}'.", result);

			return result;
		}

		private static string RandomOperator()
		{
			var rng = RandomNumberGenerator.Create();

			var data = new byte[4];
			rng.GetBytes(data);

			var index = BitConverter.ToInt32(data, 0) % Operators.Length;

			while (index < 0)
			{
				index += Operators.Length;
			}

			return Operators[index];
		}

		[SetUp]
		public void TestSetUp()
		{
			_regularExpressionTableLexerEntry = new UnaryOperatorRegularExpressionTableLexerEntry();
		}

		[Test]
		public void TokenType_ReturnsUnaryOperator()
		{
			Assert.That(_regularExpressionTableLexerEntry.TokenType, Is.EqualTo(TokenType.UnaryOperator));
		}

		[Test]
		public void IsContainedIn_ValidUnaryOperator_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.IsContainedIn(String.Format("{0}", BuildLiteralString()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsContainedIn_ValidUnaryOperatorFlushWithTextAtEnd_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsContainedIn(String.Format("hello world{0}", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void IsAtStart_ValidUnaryOperatorAtStart_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("{0} hello world", BuildLiteralString()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsAtStart_ValidUnaryOperatorFlushWithTextAtStart_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("{0}hello world", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void IsAtStart_ValidUnaryOperatorNotAtStart_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("hello world {0}", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void MatchesEntireText_ValidUnaryOperator_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.MatchesEntireText(String.Format("{0}", BuildLiteralString()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void MatchesEntireText_TextBeforeValidUnaryOperator_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.MatchesEntireText(String.Format("hello world {0}", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void MatchesEntireText_TextAfterValidUnaryOperator_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.MatchesEntireText(String.Format("{0} hello world", BuildLiteralString()));

			Assert.That(result, Is.False);
		}
	}
}