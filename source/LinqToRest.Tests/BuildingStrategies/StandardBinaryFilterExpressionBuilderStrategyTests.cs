// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;

using LinqToRest.OData.Building.Strategies;
using LinqToRest.OData.Building.Strategies.Impl;
using LinqToRest.OData.Filters;

using NUnit.Framework;

using Rhino.Mocks;

namespace LinqToRest.Tests.BuildingStrategies
{
	[TestFixture]
	public class StandardBinaryFilterExpressionBuilderStrategyTests
	{
		private IFilterExpressionBuilderStrategy _baseStrategy;

		private IFilterExpressionBuilderStrategy _binaryStrategy;

		[SetUp]
		public void TestSetUp()
		{
			_baseStrategy = MockRepository.GenerateStub<IFilterExpressionBuilderStrategy>();

			_binaryStrategy = new StandardBinaryFilterExpressionBuilderStrategy(_baseStrategy);
		}

		[Test]
		public void BuildExpression_NullStack_ThrowsException()
		{
			Assert.That(() => _binaryStrategy.BuildExpression(null), Throws.InstanceOf<ArgumentNullException>());
		}

		[Test]
		public void BuildExpression_EmptyStack_ThrowsException()
		{
			var stack = new Stack<string>();

			Assert.That(() => _binaryStrategy.BuildExpression(stack), Throws.ArgumentException);
		}

		[Test]
		public void BuildExpression_StackWithOnlyOperator_ThrowsException()
		{
			var stack = new Stack<string>();

			stack.Push("eq");

			Assert.That(() => _binaryStrategy.BuildExpression(stack), Throws.ArgumentException);
		}

		[Test]
		public void BuildExpression_StackWithOnlyOneOperand_ThrowsException()
		{
			var stack = new Stack<string>();

			stack.Push("TestInt");

			Assert.That(() => _binaryStrategy.BuildExpression(stack), Throws.ArgumentException);
		}

		[Test]
		public void BuildExpression_StackWithOnlyTwoOperands_ThrowsException()
		{
			var stack = new Stack<string>();

			stack.Push("42.3m");
			stack.Push("TestDecimal");

			Assert.That(() => _binaryStrategy.BuildExpression(stack), Throws.ArgumentException);
		}

		[Test]
		public void BuildExpression_StackWithOnlyOperatorAndOneOperand_ThrowsException()
		{
			var stack = new Stack<string>();

			stack.Push("42.3m");
			stack.Push("eq");

			Assert.That(() => _binaryStrategy.BuildExpression(stack), Throws.ArgumentException);
		}

		[Test]
		public void BuildExpression_NonBinaryOperator_ThrowsException()
		{
			var stack = new Stack<string>();

			stack.Push("42.3m");
			stack.Push("TestDecimal");
			stack.Push("not");

			Assert.That(() => _binaryStrategy.BuildExpression(stack), Throws.ArgumentException);
		}

		[Test]
		public void BuildExpression_ValidBinaryExpressionStack_ThrowsException()
		{
			_baseStrategy.Stub(x => x.BuildExpression(null))
				.IgnoreArguments()
				.Return(FilterExpression.MemberAccess("TestInt"));

			var stack = new Stack<string>();

			stack.Push("TestInt");
			stack.Push("42");
			stack.Push("ne");

			var expression = _binaryStrategy.BuildExpression(stack);

			Assert.That(expression, Is.Not.Null);
			Assert.That(expression, Is.TypeOf<BinaryFilterExpression>());
		}
	}
}
