// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LinqToRest.OData.Filters;

using NUnit.Framework;

namespace LinqToRest.Tests.Filters
{
	[TestFixture]
	public class UnaryFilterExpressionTests
	{
		[Test]
		public void Constructor_UnknownOperatorType_ThrowsArgumentException()
		{
			Assert.That(() => new UnaryFilterExpression(FilterExpressionOperator.Unknown, new ConstantFilterExpression(null, typeof (string))), Throws.ArgumentException);
		}

		[Test]
		public void Constructor_NullOperand_ThrowsArgumentNullException()
		{
			Assert.That(() => new UnaryFilterExpression(FilterExpressionOperator.Add, null), Throws.InstanceOf<ArgumentNullException>());
		}

		[Test]
		public void ToString_NotOperatorEqualsExpression_GeneratesStringCorrectly()
		{
			var left = new MemberAccessFilterExpression(null, "TestMember");
			var right = new ConstantFilterExpression("hello", typeof (string));
			var expr = new BinaryFilterExpression(left, FilterExpressionOperator.Equal, right);

			var notExpr = new UnaryFilterExpression(FilterExpressionOperator.Not, expr);

			Assert.That(notExpr.ToString(), Is.EqualTo("(not((TestMember eq 'hello')))"));
		}
	}
}
