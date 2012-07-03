// ReSharper disable InconsistentNaming

using System;

using LinqToRest.OData.Literals;
using LinqToRest.OData.Literals.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Literals
{
	[TestFixture]
	public class StringRegularExpressionTableLexerEntryTests
	{
		private IRegularExpressionTableLexerEntry _regularExpressionTableLexerEntry = new StringRegularExpressionTableLexerEntry();

		[SetUp]
		public void TestSetUp()
		{
			_regularExpressionTableLexerEntry = new StringRegularExpressionTableLexerEntry();
		}

		[Test]
		public void IsContainedIn_ValidString_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.IsContainedIn(String.Format("'{0}'", "The quick brown fox jumped over the lazy dog."));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsContainedIn_ValidStringContainingEscapedSingleQuotes_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.IsContainedIn(String.Format("'{0}'", "The quick brown \\'fox jumped over the lazy dog."));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsContainedIn_StringEndingInEscapedSingleQuote_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsContainedIn(String.Format("'{0}'", "The quick brown fox jumped over the lazy dog.\\"));

			Assert.That(result, Is.False);
		}

		[Test]
		public void IsContainedIn_ValidStringEndingInEscapedSingleQuote_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.IsContainedIn(String.Format("'{0}'", "The quick brown fox jumped over the lazy dog.\\'"));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsAtStart_ValidStringAtStart_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("'{0}' hello world", "The quick brown fox jumped over the lazy dog."));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsAtStart_ValidStringFlushWithParenthesisAtStart_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("'{0}')", "The quick brown fox jumped over the lazy dog."));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsAtStart_ValidStringNotAtStart_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("hello world '{0}'", "The quick brown fox jumped over the lazy dog."));

			Assert.That(result, Is.False);
		}

		[Test]
		public void MatchesEntireText_ValidString_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.MatchesEntireText(String.Format("'{0}'", "The quick brown fox jumped over the lazy dog."));

			Assert.That(result, Is.True);
		}

		[Test]
		public void MatchesEntireText_TextBeforeValidString_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.MatchesEntireText(String.Format("hello world '{0}'", "The quick brown fox jumped over the lazy dog."));

			Assert.That(result, Is.False);
		}

		[Test]
		public void MatchesEntireText_TextAfterValidString_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.MatchesEntireText(String.Format("'{0}' hello world", "The quick brown fox jumped over the lazy dog."));

			Assert.That(result, Is.False);
		}
	}
}
