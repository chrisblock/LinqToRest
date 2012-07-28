// ReSharper disable InconsistentNaming

using System;

using LinqToRest.OData.Formatting;
using LinqToRest.OData.Formatting.Impl;
using LinqToRest.OData.Lexing;

using NUnit.Framework;

namespace LinqToRest.Tests.Formatting
{
	[TestFixture]
	public class EdmTypeFormatterTests
	{
		private ITypeFormatter _formatter;

		[SetUp]
		public void TestSetUp()
		{
			_formatter = new EdmTypeFormatter();
		}

		[Test]
		public void Format_NullType_ThrowsException()
		{
			var value = typeof (string);

			Assert.That(() => _formatter.Format(null, value), Throws.Exception);
		}

		[Test]
		public void Format_NullValue_ThrowsException()
		{
			Assert.That(() => _formatter.Format(typeof (string), null), Throws.Exception);
		}

		[Test]
		[Sequential]
		public void Format_TypeValue_FormattedResult([Values(typeof (bool), typeof (Guid), typeof (string), typeof (sbyte), typeof (byte), typeof (short), typeof (int), typeof (long), typeof (float), typeof (double), typeof (decimal), typeof (DateTime), typeof (DateTimeOffset), typeof (TimeSpan))]Type type)
		{
			var value = type;
			var result = _formatter.Format(value);

			Assert.That(result, Is.EqualTo(EdmTypeNames.Lookup(value)));
		}
	}
}
