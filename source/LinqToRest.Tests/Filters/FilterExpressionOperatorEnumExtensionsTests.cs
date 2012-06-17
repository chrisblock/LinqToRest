// ReSharper disable InconsistentNaming

using System.Linq.Expressions;

using LinqToRest.OData.Filters;

using NUnit.Framework;

namespace LinqToRest.Tests.Filters
{
	[TestFixture]
	public class FilterExpressionOperatorEnumExtensionsTests
	{
		[Test]
		public void GetDotNetExpressionType_Unknown_ThrowsException()
		{
			Assert.That(() => FilterExpressionOperator.Unknown.GetDotNetExpressionType(), Throws.ArgumentException);
		}

		[Test]
		public void GetDotNetExpressionType_Add_ReturnsAdd()
		{
			var dotNetExpressionType = FilterExpressionOperator.Add.GetDotNetExpressionType();

			Assert.That(dotNetExpressionType, Is.EqualTo(ExpressionType.Add));
		}

		[Test]
		public void GetODataQueryOperatorString_Unknown_ThrowsException()
		{
			Assert.That(() => FilterExpressionOperator.Unknown.GetODataQueryOperatorString(), Throws.ArgumentException);
		}

		[Test]
		public void GetODataQueryOperatorString_Add_ReturnsAddString()
		{
			var dotNetExpressionType = FilterExpressionOperator.Add.GetODataQueryOperatorString();

			Assert.That(dotNetExpressionType, Is.EqualTo("add"));
		}

		[Test]
		public void GetFromODataQueryOperatorString_HelloWorldString_ThrowsException()
		{
			Assert.That(() => "Hello, World.".GetFromODataQueryOperatorString(), Throws.ArgumentException);
		}

		[Test]
		public void GetFromODataQueryOperatorString_AddString_ReturnsODataQueryFilterExpressionOperatorAdd()
		{
			var addEnumValue = "add".GetFromODataQueryOperatorString();

			Assert.That(addEnumValue, Is.EqualTo(FilterExpressionOperator.Add));
		}

		[Test]
		public void GetFromDotNetExpressionType_Convert_ThrowsException()
		{
			Assert.That(() => ExpressionType.Convert.GetFromDotNetExpressionType(), Throws.ArgumentException);
		}

		[Test]
		public void GetFromDotNetExpressionType_AndAlso_ReturnsODataQueryFilterExpressionOperatorAnd()
		{
			var enumValue = ExpressionType.AndAlso.GetFromDotNetExpressionType();

			Assert.That(enumValue, Is.EqualTo(FilterExpressionOperator.And));
		}
	}
}
