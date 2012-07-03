// ReSharper disable InconsistentNaming

using System;

using LinqToRest.OData.Literals;
using LinqToRest.OData.Literals.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Literals
{
	[TestFixture]
	public class ByteRegularExpressionTableLexerEntryTests
	{
		private IRegularExpressionTableLexerEntry _regularExpressionTableLexerEntry;

		[SetUp]
		public void TestSetUp()
		{
			_regularExpressionTableLexerEntry = new ByteRegularExpressionTableLexerEntry();
		}

		[Test]
		public void IsContainedIn_ValidByte_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.IsContainedIn(String.Format("{0}", 123));

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
		public void IsContainedIn_ValidByteFlushWithTextBefore_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsContainedIn(String.Format("hello world{0}", 123));

			Assert.That(result, Is.False);
		}

		[Test]
		public void IsContainedIn_ValidByteFlushWithTextAfter_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsContainedIn(String.Format("{0}hello world", 123));

			Assert.That(result, Is.False);
		}

		[Test]
		public void IsAtStart_ValidByteAtStart_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("{0} hello world", 123));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsAtStart_ValidByteFlushWithTextAtStart_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("{0}hello world", 123));

			Assert.That(result, Is.False);
		}

		[Test]
		public void IsAtStart_ValidByteFlushWithParenthesisAtStart_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("{0})", 123));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsAtStart_ValidByteAtEnd_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.IsAtStart(String.Format("hello world {0}", 123));

			Assert.That(result, Is.False);
		}

		[Test]
		public void MatchesEntireText_ValidByte_ReturnsTrue()
		{
			var result = _regularExpressionTableLexerEntry.MatchesEntireText(String.Format("{0}", 123));

			Assert.That(result, Is.True);
		}

		[Test]
		public void MatchesEntireText_TextBeforeValidByte_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.MatchesEntireText(String.Format("hello world {0}", 123));

			Assert.That(result, Is.False);
		}

		[Test]
		public void MatchesEntireText_TextAfterValidByte_ReturnsFalse()
		{
			var result = _regularExpressionTableLexerEntry.MatchesEntireText(String.Format("{0} hello world", 123));

			Assert.That(result, Is.False);
		}
	}
}
