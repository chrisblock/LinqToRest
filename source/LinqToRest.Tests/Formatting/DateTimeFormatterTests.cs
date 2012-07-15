// ReSharper disable InconsistentNaming

using System;

using LinqToRest.OData.Formatting;
using LinqToRest.OData.Formatting.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Formatting
{
	[TestFixture]
	public class DateTimeFormatterTests
	{
		private ITypeFormatter _formatter;

		[SetUp]
		public void TestSetUp()
		{
			_formatter = new DateTimeFormatter();
		}

		[Test]
		public void Format_NullType_ThrowsException()
		{
			var value = DateTime.Now;

			Assert.That(() => _formatter.Format(null, value), Throws.Exception);
		}

		[Test]
		public void Format_NullValue_ThrowsException()
		{
			Assert.That(() => _formatter.Format(typeof(DateTime), null), Throws.Exception);
		}

		[Test]
		public void Format_DateTimeValue_FormattedResult()
		{
			var value = DateTime.Now;
			var result = _formatter.Format(value);

			Assert.That(result, Is.EqualTo(String.Format("datetime'{0:yyyy-MM-ddTHH:mm:ss.ffffff}'", value.ToUniversalTime())));
		}
	}
}
