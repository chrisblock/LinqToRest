// ReSharper disable InconsistentNaming

using System;

using LinqToRest.OData.Literals;
using LinqToRest.OData.Literals.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Literals
{
	[TestFixture]
	public class BooleanLiteralTests
	{
		private ILiteral _literal = new BooleanLiteral();

		[SetUp]
		public void TestSetUp()
		{
			_literal = new BooleanLiteral();
		}

		[Test]
		public void Matches_ValidBooleanTrue_ReturnsTrue()
		{
			var result = _literal.IsContainedIn(String.Format("{0}", true));

			Assert.That(result, Is.True);
		}

		[Test]
		public void Matches_ValidBooleanFalse_ReturnsTrue()
		{
			var result = _literal.IsContainedIn(String.Format("{0}", false));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsAtStart_ValidBooleanTrueAtStart_ReturnsTrue()
		{
			var result = _literal.IsAtStart(String.Format("{0} hello world", true));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsAtStart_ValidBooleanFalseAtStart_ReturnsTrue()
		{
			var result = _literal.IsAtStart(String.Format("{0} hello world", false));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsAtStart_ValidBooleanTrueNotAtStart_ReturnsFalse()
		{
			var result = _literal.IsAtStart(String.Format("hello world {0}", true));

			Assert.That(result, Is.False);
		}

		[Test]
		public void IsAtStart_ValidBooleanFalseNotAtStart_ReturnsFalse()
		{
			var result = _literal.IsAtStart(String.Format("hello world {0}", false));

			Assert.That(result, Is.False);
		}

		[Test]
		public void MatchesEntireText_ValidBooleanTrue_ReturnsTrue()
		{
			var result = _literal.MatchesEntireText(String.Format("{0}", true));

			Assert.That(result, Is.True);
		}

		[Test]
		public void MatchesEntireText_ValidBooleanFalse_ReturnsTrue()
		{
			var result = _literal.MatchesEntireText(String.Format("{0}", false));

			Assert.That(result, Is.True);
		}

		[Test]
		public void MatchesEntireText_TextBeforeValidBooleanTrue_ReturnsFalse()
		{
			var result = _literal.MatchesEntireText(String.Format("hello world {0}", true));

			Assert.That(result, Is.False);
		}

		[Test]
		public void MatchesEntireText_TextBeforeValidBooleanFalse_ReturnsFalse()
		{
			var result = _literal.MatchesEntireText(String.Format("hello world {0}", false));

			Assert.That(result, Is.False);
		}

		[Test]
		public void MatchesEntireText_TextAfterValidBooleanTrue_ReturnsFalse()
		{
			var result = _literal.MatchesEntireText(String.Format("{0} hello world", true));

			Assert.That(result, Is.False);
		}

		[Test]
		public void MatchesEntireText_TextAfterValidBooleanFalse_ReturnsFalse()
		{
			var result = _literal.MatchesEntireText(String.Format("{0} hello world", false));

			Assert.That(result, Is.False);
		}
	}
}
