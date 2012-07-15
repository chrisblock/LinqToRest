// ReSharper disable InconsistentNaming

using System;

using LinqToRest.OData;
using LinqToRest.OData.Filters;

using NUnit.Framework;

namespace LinqToRest.Tests.OData
{
	[TestFixture]
	public class FilterQueryPartTests
	{
		[Test]
		public void Constructor_NullFilterExpression_ThrowsException()
		{
			Assert.That(() => new FilterQueryPart(null), Throws.InstanceOf<ArgumentNullException>());
		}

		[Test]
		public void QueryPartType_ReturnsFilterQueryPart()
		{
			var filterQueryPart = new FilterQueryPart(FilterExpression.Constant("string!"));

			Assert.That(filterQueryPart.QueryPartType, Is.EqualTo(ODataQueryPartType.Filter));
			Assert.That(filterQueryPart.ToString(), Is.EqualTo("$filter='string!'"));
		}
	}
}
