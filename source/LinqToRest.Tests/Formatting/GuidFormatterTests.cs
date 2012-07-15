// ReSharper disable InconsistentNaming

using System;

using LinqToRest.OData.Formatting;
using LinqToRest.OData.Formatting.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Formatting
{
	[TestFixture]
	public class GuidFormatterTests
	{
		private ITypeFormatter _formatter;

		[SetUp]
		public void TestSetUp()
		{
			_formatter = new GuidFormatter();
		}

		[Test]
		public void Format_NullType_ThrowsException()
		{
			var value = Guid.NewGuid();

			Assert.That(() => _formatter.Format(null, value), Throws.Exception);
		}

		[Test]
		public void Format_NullValue_ThrowsException()
		{
			Assert.That(() => _formatter.Format(typeof(Guid), null), Throws.Exception);
		}

		[Test]
		public void Format_GuidValue_FormattedResult()
		{
			var value = Guid.NewGuid();
			var result = _formatter.Format(value);

			Assert.That(result, Is.EqualTo(String.Format("guid'{0}'", value)));
		}
	}
}
