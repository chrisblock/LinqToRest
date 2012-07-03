// ReSharper disable InconsistentNaming

using System;
using System.Security.Cryptography;

using LinqToRest.OData.Literals;
using LinqToRest.OData.Literals.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Literals
{
	[TestFixture]
	public class LongRegularExpressionTableLexerEntryTests
	{
		private IRegularExpressionTableLexerEntry _regularExpressionTableLexerEntry;

		private static string BuildLiteralString()
		{
			var result = String.Format("{0}L", RandomLong());

			Console.WriteLine("Generated long: '{0}'.", result);

			return result;
		}

		private static long RandomLong()
		{
			var rng = RandomNumberGenerator.Create();

			var bytes = new byte[8];
			rng.GetBytes(bytes);

			return BitConverter.ToInt64(bytes, 0);
		}

		[SetUp]
		public void TestSetUp()
		{
			_regularExpressionTableLexerEntry = new LongRegularExpressionTableLexerEntry();
		}

		[Test]
		public void IsContainedIn_ValidLong_ReturnsTrue()
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
		public void IsContainedIn_ValidLongFlushWithText_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsContainedIn(String.Format("hello world{0}", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void IsAtStart_ValidLongAtStart_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("{0} hello world", BuildLiteralString()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsAtStart_ValidLongFlushWithTextAtStart_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("{0}hello world", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void IsAtStart_ValidLongFlushWithParenthesisAtStart_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("{0})", BuildLiteralString()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsAtStart_ValidLongNotAtStart_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("hello world {0}", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void MatchesEntireText_ValidLong_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.MatchesEntireText(String.Format("{0}", BuildLiteralString()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void MatchesEntireText_TextBeforeValidLong_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.MatchesEntireText(String.Format("hello world {0}", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void MatchesEntireText_TextAfterValidLong_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.MatchesEntireText(String.Format("{0} hello world", BuildLiteralString()));

			Assert.That(result, Is.False);
		}
	}
}
