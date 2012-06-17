// ReSharper disable InconsistentNaming

using LinqToRest.OData;
using LinqToRest.OData.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.OData
{
	[TestFixture]
	public class DefaultODataQueryFactoryTests
	{
		private IODataQueryFactory _queryFactory;

		[SetUp]
		public void TestSetUp()
		{
			_queryFactory = new DefaultODataQueryFactory();
		}

		[Test]
		public void Create_NoParameters_ReturnsInstanceWithFormatSet()
		{
			var query = _queryFactory.Create();

			Assert.That(query.FormatPredicate, Is.Not.Null);
			Assert.That(query.FormatPredicate.DataFormat, Is.EqualTo(ODataFormat.Json));
		}
	}
}
