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
		public void FilterQueryPart_InheritsFromIEquatable()
		{
			var actual = new FilterQueryPart(FilterExpression.Binary(FilterExpression.MemberAccess("Hello"), FilterExpressionOperator.LessThan, FilterExpression.Constant(4)));

			Assert.That(actual, Is.InstanceOf<IEquatable<FilterQueryPart>>());
		}

		[Test]
		public void Equals_ObjectNull_ReturnsFalse()
		{
			var queryPart = new FilterQueryPart(FilterExpression.Binary(FilterExpression.MemberAccess("Hello"), FilterExpressionOperator.LessThan, FilterExpression.Constant(4)));
			object other = null;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_FilterQueryPartNull_ReturnsFalse()
		{
			var queryPart = new FilterQueryPart(FilterExpression.Binary(FilterExpression.MemberAccess("Hello"), FilterExpressionOperator.LessThan, FilterExpression.Constant(4)));
			FilterQueryPart other = null;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_ObjectOfDifferentType_ReturnsFalse()
		{
			var queryPart = new FilterQueryPart(FilterExpression.Binary(FilterExpression.MemberAccess("Hello"), FilterExpressionOperator.LessThan, FilterExpression.Constant(4)));
			object other = String.Empty;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_ObjectSelf_ReturnsTrue()
		{
			var queryPart = new FilterQueryPart(FilterExpression.Binary(FilterExpression.MemberAccess("Hello"), FilterExpressionOperator.LessThan, FilterExpression.Constant(4)));
			object other = queryPart;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void Equals_Self_ReturnsTrue()
		{
			var queryPart = new FilterQueryPart(FilterExpression.Binary(FilterExpression.MemberAccess("Hello"), FilterExpressionOperator.LessThan, FilterExpression.Constant(4)));
			var other = queryPart;

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void Equals_AnotherDifferentFilterQueryPart_ReturnsFalse()
		{
			var queryPart = new FilterQueryPart(FilterExpression.MemberAccess("Hello"));
			var other = new FilterQueryPart(FilterExpression.MemberAccess("World"));

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.False);
		}

		[Test]
		public void Equals_AnotherEquivalentFilterQueryPart_ReturnsTrue()
		{
			var queryPart = new FilterQueryPart(FilterExpression.MemberAccess("Hello"));
			var other = new FilterQueryPart(FilterExpression.MemberAccess("Hello"));

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void Equals_ObjectAnotherEquivalentFilterQueryPart_ReturnsTrue()
		{
			var queryPart = new FilterQueryPart(FilterExpression.Binary(FilterExpression.MemberAccess("Hello"), FilterExpressionOperator.LessThan, FilterExpression.Constant(4)));
			object other = new FilterQueryPart(FilterExpression.Binary(FilterExpression.MemberAccess("Hello"), FilterExpressionOperator.LessThan, FilterExpression.Constant(4)));

			var actual = queryPart.Equals(other);

			Assert.That(actual, Is.True);
		}

		[Test]
		public void GetHashCode_FilterExpression_ReturnsFilterExpressionHashCode()
		{
			var filterExpression = FilterExpression.Binary(FilterExpression.Constant(4), FilterExpressionOperator.LessThanOrEqual, FilterExpression.MemberAccess("HelloWorld"));

			var queryPart = new FilterQueryPart(filterExpression);

			Assert.That(queryPart.GetHashCode(), Is.EqualTo(filterExpression.GetHashCode()));
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
