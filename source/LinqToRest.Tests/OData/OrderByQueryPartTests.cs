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
		public void OrderByQueryPart_InheritsFromIEquatable()
		{
			var actual = new OrderByQueryPart();

			Assert.That(actual, Is.InstanceOf<IEquatable<OrderByQueryPart>>());
		}

		[Test]
		public void Equals_ObjectNull_ReturnsFalse()
		{
			var queryPart = new OrderByQueryPart();
			object other = null;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_OrderByQueryPartNull_ReturnsFalse()
		{
			var queryPart = new OrderByQueryPart();
			OrderByQueryPart other = null;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_ObjectOfDifferentType_ReturnsFalse()
		{
			var queryPart = new OrderByQueryPart();
			object other = String.Empty;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_ObjectSelf_ReturnsTrue()
		{
			var queryPart = new OrderByQueryPart();
			object other = queryPart;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void Equals_Self_ReturnsTrue()
		{
			var queryPart = new OrderByQueryPart();
			var other = queryPart;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void Equals_AnotherDifferentOrderByQueryPart_ReturnsFalse()
		{
			var queryPart = new OrderByQueryPart(new ODataOrdering("World", ODataOrderingDirection.Desc));
			var other = new OrderByQueryPart(new ODataOrdering("Hello", ODataOrderingDirection.Asc));

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_AnotherEquivalentOrderByQueryPart_ReturnsTrue()
		{
			var queryPart = new OrderByQueryPart();
			var other = new OrderByQueryPart();

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void Equals_ObjectAnotherEquivalentOrderByQueryPart_ReturnsTrue()
		{
			var queryPart = new OrderByQueryPart();
			object other = new OrderByQueryPart();

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void GetHashCode_NoOrderings_ReturnsZero()
		{
			var queryPart = new OrderByQueryPart();

			var expected = 0;

			Assert.That(queryPart.GetHashCode(), Is.EqualTo(expected));
		}

		[Test]
		public void GetHashCode_MultipleOrderings_ReturnsCorrectValue()
		{
			var queryPart = new OrderByQueryPart(new ODataOrdering("Hello", ODataOrderingDirection.Asc), new ODataOrdering("World", ODataOrderingDirection.Desc));

			var expected = String.Format("Orderings:Hello asc,World desc;");

			Assert.That(queryPart.GetHashCode(), Is.EqualTo(expected.GetHashCode()));
		}

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

		[Test]
		public void ToString_TwoOrderingsPassedByConstructor_ReturnsCommaSeperatedList()
		{
			var orderByQueryPart = new OrderByQueryPart(new ODataOrdering("Hello", ODataOrderingDirection.Asc), new ODataOrdering("World", ODataOrderingDirection.Desc));

			Assert.That(orderByQueryPart.ToString(), Is.EqualTo("$orderby=Hello asc, World desc"));
		}
	}
}
