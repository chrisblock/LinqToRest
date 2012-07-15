// ReSharper disable InconsistentNaming

using LinqToRest.OData.Formatting;
using LinqToRest.OData.Formatting.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Formatting
{
	[TestFixture]
	public class LongFormatterTests
	{
		private ITypeFormatter _formatter;

		[SetUp]
		public void TestSetUp()
		{
			_formatter = new LongFormatter();
		}

		[Test]
		public void Format_NullType_ThrowsException()
		{
			var value = 42L;

			Assert.That(() => _formatter.Format(null, value), Throws.Exception);
		}

		[Test]
		public void Format_NullValue_ThrowsException()
		{
			Assert.That(() => _formatter.Format(typeof (long), null), Throws.Exception);
		}

		[Test]
		public void Format_LongValue_FormattedResult()
		{
			var value = 42L;
			var result = _formatter.Format(value);

			Assert.That(result, Is.EqualTo("42L"));
		}
	}
}
