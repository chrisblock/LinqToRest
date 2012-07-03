// ReSharper disable InconsistentNaming

using System;

using LinqToRest.OData.Literals;
using LinqToRest.OData.Literals.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Literals
{
	[TestFixture]
	public class GuidRegularExpressionTableLexerEntryTests
	{
		private IRegularExpressionTableLexerEntry _regularExpressionTableLexerEntry;

		private static string BuildLiteralString()
		{
			return String.Format("guid'{0}'", Guid.NewGuid());
		}

		[SetUp]
		public void TestSetUp()
		{
			_regularExpressionTableLexerEntry = new GuidRegularExpressionTableLexerEntry();
		}

		[Test]
		public void IsContainedIn_ValidGuid_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.IsContainedIn(String.Format("{0}", BuildLiteralString()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsContainedIn_ValidGuidFlushWithText_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsContainedIn(String.Format("hello world{0}", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void IsAtStart_ValidGuidAtStart_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("{0} hello world", BuildLiteralString()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsAtStart_ValidGuidFlushWithTextAtStart_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("{0}hello world", BuildLiteralString()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsAtStart_ValidGuidFlushWithParenthesisAtStart_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("{0})", BuildLiteralString()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsAtStart_ValidGuidNotAtStart_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("hello world {0}", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void MatchesEntireText_ValidGuid_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.MatchesEntireText(String.Format("{0}", BuildLiteralString()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void MatchesEntireText_TextBeforeValidGuid_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.MatchesEntireText(String.Format("hello world {0}", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void MatchesEntireText_TextAfterValidGuid_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.MatchesEntireText(String.Format("{0} hello world", BuildLiteralString()));

			Assert.That(result, Is.False);
		}
	}
}
