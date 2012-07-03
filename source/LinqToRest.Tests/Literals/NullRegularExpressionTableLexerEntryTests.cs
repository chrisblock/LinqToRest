// ReSharper disable InconsistentNaming

using System;

using LinqToRest.OData.Literals;
using LinqToRest.OData.Literals.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Literals
{
	[TestFixture]
	public class NullRegularExpressionTableLexerEntryTests
	{
		private IRegularExpressionTableLexerEntry _regularExpressionTableLexerEntry;

		private static string BuildLiteralString()
		{
			var result = String.Format("null");

			return result;
		}

		[SetUp]
		public void TestSetUp()
		{
			_regularExpressionTableLexerEntry = new NullRegularExpressionTableLexerEntry();
		}

		[Test]
		public void IsContainedIn_ValidNull_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.IsContainedIn(String.Format("{0}", BuildLiteralString()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsContainedIn_ValidNullFlushWithText_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsContainedIn(String.Format("hello world{0}", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void IsAtStart_ValidNullAtStart_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("{0} hello world", BuildLiteralString()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsAtStart_ValidNullFlushWithTextAtStart_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("{0}hello world", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void IsAtStart_ValidNullFlushWithParenthesisAtStart_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("{0})", BuildLiteralString()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsAtStart_ValidNullNotAtStart_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("hello world {0}", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void MatchesEntireText_ValidNull_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.MatchesEntireText(String.Format("{0}", BuildLiteralString()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void MatchesEntireText_TextBeforeValidNull_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.MatchesEntireText(String.Format("hello world {0}", BuildLiteralString()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void MatchesEntireText_TextAfterValidNull_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.MatchesEntireText(String.Format("{0} hello world", BuildLiteralString()));

			Assert.That(result, Is.False);
		}
	}
}
