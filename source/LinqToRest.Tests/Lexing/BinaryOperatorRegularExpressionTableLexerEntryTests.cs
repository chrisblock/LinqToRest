// ReSharper disable InconsistentNaming

using System;
using System.Linq;
using System.Security.Cryptography;

using LinqToRest.OData.Lexing;
using LinqToRest.OData.Lexing.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Lexing
{
	[TestFixture]
	public class BinaryOperatorRegularExpressionTableLexerEntryTests
	{
		private static readonly string[] Operators = BinaryOperatorRegularExpressionTableLexerEntry.Operators.ToArray();

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
			_regularExpressionTableLexerEntry = new BinaryOperatorRegularExpressionTableLexerEntry();
		}

		[Test]
		public void TokenType_ReturnsBinaryOperator()
		{
			Assert.That(_regularExpressionTableLexerEntry.TokenType, Is.EqualTo(TokenType.BinaryOperator));
		}

		[Test]
		public void IsContainedIn_ValidBinaryOperator_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.IsContainedIn(String.Format("{0}", BuildLiteralString()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsContainedIn_ValidBinaryOperatorFlushWithTextAtEnd_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsContainedIn(String.Format("hello world{0}", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void IsAtStart_ValidBinaryOperatorAtStart_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("{0} hello world", BuildLiteralString()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsAtStart_ValidBinaryOperatorFlushWithTextAtStart_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("{0}hello world", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void IsAtStart_ValidBinaryOperatorNotAtStart_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("hello world {0}", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void MatchesEntireText_ValidBinaryOperator_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.MatchesEntireText(String.Format("{0}", BuildLiteralString()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void MatchesEntireText_TextBeforeValidBinaryOperator_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.MatchesEntireText(String.Format("hello world {0}", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void MatchesEntireText_TextAfterValidBinaryOperator_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.MatchesEntireText(String.Format("{0} hello world", BuildLiteralString()));

			Assert.That(result, Is.False);
		}
	}
}
