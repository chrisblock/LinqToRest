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
	public class FunctionFilterExpressionBuilderStrategyTests
	{
		private IFilterExpressionBuilderStrategy _baseStrategy;

		private IFilterExpressionBuilderStrategy _functionStrategy;

		[SetUp]
		public void TestSetUp()
		{
			_baseStrategy = MockRepository.GenerateStub<IFilterExpressionBuilderStrategy>();

			_functionStrategy = new FunctionFilterExpressionBuilderStrategy(_baseStrategy);
		}

		[Test]
		public void BuildExpresion_NullStack_ThrowsException()
		{
			Assert.That(() => _functionStrategy.BuildExpression(null), Throws.InstanceOf<ArgumentNullException>());
		}

		[Test]
		public void BuildExpresion_EmptyStack_ThrowsException()
		{
			var stack = new Stack<string>();

			Assert.That(() => _functionStrategy.BuildExpression(stack), Throws.ArgumentException);
		}

		[Test]
		public void BuildExpresion_StackWithOnlyFunctionName_ThrowsException()
		{
			var stack = new Stack<string>();

			stack.Push("trim");

			Assert.That(() => _functionStrategy.BuildExpression(stack), Throws.ArgumentException);
		}

		[Test]
		public void BuildExpresion_StackWithUnknownFunctionName_ThrowsException()
		{
			var stack = new Stack<string>();

			stack.Push(Guid.NewGuid().ToString());

			Assert.That(() => _functionStrategy.BuildExpression(stack), Throws.ArgumentException);
		}

		[Test]
		public void BuildExpresion_ValidStackExpression_ReturnsValidArityExpression()
		{
			var stack = new Stack<string>();

			stack.Push("TestString");
			stack.Push("trim");

			var expression = _functionStrategy.BuildExpression(stack);

			Assert.That(expression, Is.Not.Null);
			Assert.That(expression, Is.TypeOf<MethodCallFilterExpression>());
		}
	}
}
