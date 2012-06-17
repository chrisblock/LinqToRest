// ReSharper disable InconsistentNaming

using System;

using LinqToRest.OData.Literals;
using LinqToRest.OData.Literals.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Literals
{
	[TestFixture]
	public class GuidLiteralTests
	{
		private ILiteral _literal;

		[SetUp]
		public void TestSetUp()
		{
			_literal = new GuidLiteral();
		}

		[Test]
		public void Matches_ValidGuid_ReturnsTrue()
		{
			var result = _literal.IsContainedIn(String.Format("guid'{0}'", Guid.NewGuid()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsAtStart_ValidGuidAtStart_ReturnsTrue()
		{
			var result = _literal.IsAtStart(String.Format("guid'{0}' hello world", Guid.NewGuid()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsAtStart_ValidGuidNotAtStart_ReturnsFalse()
		{
			var result = _literal.IsAtStart(String.Format("hello world guid'{0}'", Guid.NewGuid()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void MatchesEntireText_ValidGuid_ReturnsTrue()
		{
			var result = _literal.MatchesEntireText(String.Format("guid'{0}'", Guid.NewGuid()));

			Assert.That(result, Is.True);
		}

		[Test]
		public void MatchesEntireText_TextBeforeValidGuid_ReturnsFalse()
		{
			var result = _literal.MatchesEntireText(String.Format("hello world guid'{0}'", Guid.NewGuid()));

			Assert.That(result, Is.False);
		}

		[Test]
		public void MatchesEntireText_TextAfterValidGuid_ReturnsFalse()
		{
			var result = _literal.MatchesEntireText(String.Format("guid'{0}' hello world", Guid.NewGuid()));

			Assert.That(result, Is.False);
		}
	}
}
