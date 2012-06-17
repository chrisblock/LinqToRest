// ReSharper disable InconsistentNaming

using System;

using LinqToRest.OData.Literals;
using LinqToRest.OData.Literals.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Literals
{
	[TestFixture]
	public class StringLiteralTests
	{
		private ILiteral _literal = new StringLiteral();

		[SetUp]
		public void TestSetUp()
		{
			_literal = new StringLiteral();
		}

		[Test]
		public void Matches_ValidString_ReturnsTrue()
		{
			var result = _literal.IsContainedIn(String.Format("'{0}'", "The quick brown fox jumped over the lazy dog."));

			Assert.That(result, Is.True);
		}

		[Test]
		public void Matches_ValidStringContainingEscapedSingleQuotes_ReturnsTrue()
		{
			var result = _literal.IsContainedIn(String.Format("'{0}'", "The quick brown \\'fox jumped over the lazy dog."));

			Assert.That(result, Is.True);
		}

		[Test]
		public void Matches_StringEndingInEscapedSingleQuote_ReturnsFalse()
		{
			var result = _literal.IsContainedIn(String.Format("'{0}'", "The quick brown fox jumped over the lazy dog.\\"));

			Assert.That(result, Is.False);
		}

		[Test]
		public void Matches_ValidStringEndingInEscapedSingleQuote_ReturnsTrue()
		{
			var result = _literal.IsContainedIn(String.Format("'{0}'", "The quick brown fox jumped over the lazy dog.\\'"));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsAtStart_ValidStringAtStart_ReturnsTrue()
		{
			var result = _literal.IsAtStart(String.Format("'{0}' hello world", "The quick brown fox jumped over the lazy dog."));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsAtStart_ValidStringNotAtStart_ReturnsFalse()
		{
			var result = _literal.IsAtStart(String.Format("hello world '{0}'", "The quick brown fox jumped over the lazy dog."));

			Assert.That(result, Is.False);
		}

		[Test]
		public void MatchesEntireText_ValidString_ReturnsTrue()
		{
			var result = _literal.MatchesEntireText(String.Format("'{0}'", "The quick brown fox jumped over the lazy dog."));

			Assert.That(result, Is.True);
		}

		[Test]
		public void MatchesEntireText_TextBeforeValidString_ReturnsFalse()
		{
			var result = _literal.MatchesEntireText(String.Format("hello world '{0}'", "The quick brown fox jumped over the lazy dog."));

			Assert.That(result, Is.False);
		}

		[Test]
		public void MatchesEntireText_TextAfterValidString_ReturnsFalse()
		{
			var result = _literal.MatchesEntireText(String.Format("'{0}' hello world", "The quick brown fox jumped over the lazy dog."));

			Assert.That(result, Is.False);
		}
	}
}
