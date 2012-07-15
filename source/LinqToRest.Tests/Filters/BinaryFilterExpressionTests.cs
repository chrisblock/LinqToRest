// ReSharper disable InconsistentNaming

using System;

using LinqToRest.OData.Filters;

using NUnit.Framework;

namespace LinqToRest.Tests.Filters
{
	[TestFixture]
	public class BinaryFilterExpressionTests
	{
		[Test]
		public void Constructor_NullLeftExpression_ThrowsArgumentExpresion()
		{
			Assert.That(() => new BinaryFilterExpression(null, FilterExpressionOperator.Equal, new ConstantFilterExpression("hello")), Throws.InstanceOf<ArgumentNullException>());
		}

		[Test]
		public void Constructor_NullRightExpression_ThrowsArgumentExpresion()
		{
			Assert.That(() => new BinaryFilterExpression(new ConstantFilterExpression("hello"), FilterExpressionOperator.Equal, null), Throws.InstanceOf<ArgumentNullException>());
		}

		[Test]
		public void Constructor_UnknownOperator_ThrowsArgumentExpresion()
		{
			Assert.That(() => new BinaryFilterExpression(new ConstantFilterExpression("world"), FilterExpressionOperator.Unknown, new ConstantFilterExpression("hello")), Throws.ArgumentException);
		}
	}
}
