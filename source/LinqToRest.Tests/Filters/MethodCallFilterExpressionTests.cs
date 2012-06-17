// ReSharper disable InconsistentNaming

using LinqToRest.OData.Filters;

using NUnit.Framework;

namespace LinqToRest.Tests.Filters
{
	[TestFixture]
	public class MethodCallFilterExpressionTests
	{
		[Test]
		public void Constructor_UnknownFunction_ThrowsException()
		{
			Assert.That(() => new MethodCallFilterExpression(Function.Unknown, new ConstantFilterExpression("hello")), Throws.ArgumentException);
		}

		[Test]
		public void Constructor_NullParameter_ThrowsException()
		{
			Assert.That(() => new MethodCallFilterExpression(Function.Trim, null), Throws.ArgumentException);
		}

		[Test]
		public void Constructor_EmptyParameterArray_ThrowsException()
		{
			Assert.That(() => new MethodCallFilterExpression(Function.ToLower, new FilterExpression[0]), Throws.ArgumentException);
		}

		[Test]
		public void ToString_ValidFunctionAndNonEmptyArguments_GeneratesCorrectString()
		{
			var methodCallExpression = new MethodCallFilterExpression(Function.Trim, new ConstantFilterExpression("hello  "));

			Assert.That(methodCallExpression.ToString(), Is.EqualTo("trim('hello  ')"));
		}
	}
}
