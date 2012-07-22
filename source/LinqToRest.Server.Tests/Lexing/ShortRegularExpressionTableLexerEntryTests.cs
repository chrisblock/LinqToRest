// ReSharper disable InconsistentNaming

using System;
using System.Security.Cryptography;

using LinqToRest.OData.Lexing;
using LinqToRest.Server.OData.Lexing;
using LinqToRest.Server.OData.Lexing.Impl;

using NUnit.Framework;

namespace LinqToRest.Server.Tests.Lexing
{
	[TestFixture]
	public class ShortRegularExpressionTableLexerEntryTests
	{
		private IRegularExpressionTableLexerEntry _regularExpressionTableLexerEntry;

		private static string BuildLiteralString()
		{
			var result = String.Format("{0}", RandomShort());

			Console.WriteLine("Generated short: '{0}'.", result);

			return result;
		}

		private static short RandomShort()
		{
			var rng = RandomNumberGenerator.Create();

			var bytes = new byte[2];
			rng.GetBytes(bytes);

			return BitConverter.ToInt16(bytes, 0);
		}

		[SetUp]
		public void TestSetUp()
		{
			_regularExpressionTableLexerEntry = new ShortRegularExpressionTableLexerEntry();
		}

		[Test]
		public void TokenType_ReturnsShort()
		{
			Assert.That(_regularExpressionTableLexerEntry.TokenType, Is.EqualTo(TokenType.Short));
		}

		[Test]
		public void IsContainedIn_ValidShort_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.IsContainedIn(String.Format("{0}", BuildLiteralString()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsContainedIn_ValidFloat_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsContainedIn(String.Format("{0}f", 123.45));

			Assert.That(result, Is.False);
		}

		[Test]
		public void IsContainedIn_ValidDouble_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsContainedIn(String.Format("{0}", 123.45));

			Assert.That(result, Is.False);
		}

		[Test]
		public void IsContainedIn_ValidDecimal_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsContainedIn(String.Format("{0}m", 123.45));

			Assert.That(result, Is.False);
		}

		[Test]
		public void IsContainedIn_ValidShortFlushWithText_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsContainedIn(String.Format("hello world{0}", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void IsAtStart_ValidShortAtStart_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("{0} hello world", BuildLiteralString()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsAtStart_ValidShortFlushWithTextAtStart_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("{0}hello world", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void IsAtStart_ValidShortFlushWithParenthesisAtStart_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("{0})", BuildLiteralString()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsAtStart_ValidShortNotAtStart_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("hello world {0}", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void MatchesEntireText_ValidShort_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.MatchesEntireText(String.Format("{0}", BuildLiteralString()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void MatchesEntireText_TextBeforeValidShort_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.MatchesEntireText(String.Format("hello world {0}", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void MatchesEntireText_TextAfterValidShort_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.MatchesEntireText(String.Format("{0} hello world", BuildLiteralString()));

			Assert.That(result, Is.False);
		}
	}
}
