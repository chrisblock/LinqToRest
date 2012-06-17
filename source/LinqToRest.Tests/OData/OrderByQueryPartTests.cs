// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LinqToRest.OData;

using NUnit.Framework;

namespace LinqToRest.Tests.OData
{
	[TestFixture]
	public class OrderByQueryPartTests
	{
		[Test]
		public void ToString_NoOrderings_ReturnsEmptyString()
		{
			var orderByQueryPart = new OrderByQueryPart();

			Assert.That(orderByQueryPart.ToString(), Is.EqualTo(String.Empty));
		}

		[Test]
		public void ToString_TwoOrderings_ReturnsCommaSeperatedList()
		{
			var orderByQueryPart = new OrderByQueryPart();

			orderByQueryPart.AddOrdering(new ODataOrdering("Hello", ODataOrderingDirection.Asc));
			orderByQueryPart.AddOrdering(new ODataOrdering("World", ODataOrderingDirection.Desc));

			Assert.That(orderByQueryPart.ToString(), Is.EqualTo("$orderby=Hello asc, World desc"));
		}
	}
}
