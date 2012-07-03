// ReSharper disable InconsistentNaming

using System;

using NUnit.Framework;

namespace LinqToRest.Tests
{
	[TestFixture]
	public class StringExtensionTests
	{
		private string _str;

		[SetUp]
		public void TestSetUp()
		{
			_str = "The quick brown fox jumped over the lazy dog.";
		}

		[Test]
		public void SubString_EndLessThanStart_ThrowsArgumentException()
		{
			Assert.That(() => _str.SubString(100, 0), Throws.ArgumentException);
		}

		[Test]
		public void SubString_StartAndEndEqual_ReturnsEmptyString()
		{
			var result = _str.SubString(3, 3);

			Assert.That(result, Is.EqualTo(String.Empty));
		}

		[Test]
		public void SubString_EndAfterEndOfString_ReturnsRestOfString()
		{
			var result = _str.SubString(3, 9000);

			Assert.That(result, Is.EqualTo(" quick brown fox jumped over the lazy dog."));
		}

		[Test]
		public void SubString_ValidStartAndEnd_ReturnsCorrectSubString()
		{
			var result = _str.SubString(4, 9);

			Assert.That(result, Is.EqualTo("quick"));
		}

		[Test]
		public void SubString_NegativeEnd_ReturnsCorrectSubString()
		{
			var result = _str.SubString(4, -1);

			Assert.That(result, Is.EqualTo("quick brown fox jumped over the lazy dog"));
		}
	}
}
