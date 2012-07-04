// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Linq;

using LinqToRest.OData;
using LinqToRest.OData.Filters;
using LinqToRest.Server.OData;
using LinqToRest.Server.OData.Expressions;
using LinqToRest.Server.OData.Expressions.Impl;

using NUnit.Framework;

namespace LinqToRest.Server.Tests
{
	[TestFixture]
	public class ODataQueryVisitorTests
	{
		private readonly IODataQueryTranslator _translator = new ExpressionODataQueryVisitor(new FilterExpressionBuilder());

		private readonly IQueryable<TestModel> _source = BuildSource();

		private static readonly Guid TestGuid = Guid.NewGuid();

		private static IQueryable<TestModel> BuildSource()
		{
			return new List<TestModel>
			{
				new TestModel
				{
					TestBoolean = true,
					TestDateTime = new DateTime(2012, 1, 1, 1, 1, 10),
					TestDateTimeOffset = new DateTimeOffset(2008, 2, 2, 2, 2, 4, 0, TimeSpan.FromSeconds(0)),
					TestTime = TimeSpan.FromSeconds(0),
					TestDecimal = 3.9m,
					TestGuid = Guid.NewGuid(),
					TestInt = 3,
					TestObject = new object(),
					TestString = "hello'",
					TestChild = null
				},
				new TestModel
				{
					TestBoolean = true,
					TestDateTime = new DateTime(2012, 2, 1, 1, 1, 1),
					TestDateTimeOffset = new DateTimeOffset(2009, 3, 2, 2, 2, 2, 0, TimeSpan.FromSeconds(0)),
					TestTime = TimeSpan.FromSeconds(30),
					TestDecimal = 3.3m,
					TestGuid = Guid.NewGuid(),
					TestInt = 4,
					TestObject = String.Empty,
					TestString = "hello  ",
					TestChild = null
				},
				new TestModel
				{
					TestBoolean = true,
					TestDateTime = new DateTime(2012, 3, 4, 1, 1, 1),
					TestDateTimeOffset = new DateTimeOffset(2010, 4, 9, 2, 2, 2, 0, TimeSpan.FromSeconds(0)),
					TestTime = TimeSpan.FromMinutes(30),
					TestDecimal = 2.5m,
					TestGuid = Guid.NewGuid(),
					TestInt = 5,
					TestObject = "Hello World",
					TestString = "goodbye",
					TestChild = null
				},
				new TestModel
				{
					TestBoolean = false,
					TestDateTime = new DateTime(2012, 1, 5, 6, 1, 1),
					TestDateTimeOffset = new DateTimeOffset(2010, 2, 1, 4, 2, 2, 0, TimeSpan.FromSeconds(0)),
					TestTime = TimeSpan.FromHours(12),
					TestDecimal = 1.9m,
					TestGuid = TestGuid,
					TestInt = 6,
					TestObject = null,
					TestString = "omg",
					TestChild = null
				},
				new TestModel
				{
					TestBoolean = true,
					TestDateTime = new DateTime(2010, 1, 1, 7, 8, 1),
					TestDateTimeOffset = new DateTimeOffset(2010, 2, 2, 6, 7, 2, 0, TimeSpan.FromSeconds(0)),
					TestTime = TimeSpan.FromDays(300),
					TestDecimal = 4.0m,
					TestGuid = Guid.NewGuid(),
					TestInt = 7,
					TestObject = new object(),
					TestString = "wtf",
					TestChild = null
				},
				new TestModel
				{
					TestBoolean = false,
					TestDateTime = new DateTime(2011, 1, 1, 1, 9, 10),
					TestDateTimeOffset = new DateTimeOffset(2010, 2, 2, 2, 3, 5, 0, TimeSpan.FromSeconds(0)),
					TestTime = TimeSpan.FromDays(2 * 365),
					TestDecimal = 9.9m,
					TestGuid = Guid.NewGuid(),
					TestInt = 8,
					TestObject = new object(),
					TestString = "OMG",
					TestChild = null
				}
			}.AsQueryable();
		}
			
		[Test]
		public void Translate_TestIntEqualsThree_ReturnsOneResult()
		{
			var left = FilterExpression.MemberAccess("TestInt");
			var right = FilterExpression.Constant(3);

			var filterExpression = FilterExpression.Binary(left, FilterExpressionOperator.Equal, right);

			var query = new ODataQuery
			{
				FilterPredicate = ODataQueryPart.Filter(filterExpression)
			};

			var expression = _translator.Translate<TestModel>(query);

			var fn = (Func<IQueryable<TestModel>, IQueryable<TestModel>>) expression.Compile();

			var result = fn(_source).ToList();

			Assert.That(result.Count, Is.EqualTo(1));
			Assert.That(result[0].TestInt, Is.EqualTo(3));
		}

		[Test]
		public void Translate_SkipThree_ReturnsLastThreeResult()
		{
			var query = new ODataQuery
			{
				SkipPredicate = ODataQueryPart.Skip(3)
			};

			var expression = _translator.Translate<TestModel>(query);

			var fn = (Func<IQueryable<TestModel>, IQueryable<TestModel>>)expression.Compile();

			var result = fn(_source).ToList();

			Assert.That(result.Count, Is.EqualTo(3));
			Assert.That(result[0].TestInt, Is.EqualTo(6));
			Assert.That(result[1].TestInt, Is.EqualTo(7));
			Assert.That(result[2].TestInt, Is.EqualTo(8));
		}
	}
}
