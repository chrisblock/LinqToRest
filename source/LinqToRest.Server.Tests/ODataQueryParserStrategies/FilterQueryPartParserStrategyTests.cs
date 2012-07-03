// ReSharper disable InconsistentNaming

using System.Linq;

using LinqToRest.OData;
using LinqToRest.OData.Filters;
using LinqToRest.OData.Literals.Impl;
using LinqToRest.Server.OData.Parsing;
using LinqToRest.Server.OData.Parsing.Impl;

using NUnit.Framework;

namespace LinqToRest.Server.Tests.ODataQueryParserStrategies
{
	[TestFixture]
	public class FilterQueryPartParserStrategyTests
	{
		private const ODataQueryPartType Type = ODataQueryPartType.Filter;
		private readonly IODataQueryPartParserStrategy _strategy = new FilterQueryPartParserStrategy(new RegularExpressionTableLexer());

		[Test]
		public void Parse_IncorrectType_ThrowsArgumentException()
		{
			Assert.That(() => _strategy.Parse(ODataQueryPartType.Ordering, "TestInteger lt 3"), Throws.ArgumentException);
		}

		[Test]
		public void Parse_TestIntegerLessThanThree_ReturnsCorrectQueryPart()
		{
			var result = _strategy.Parse(Type, "TestInteger lt 3");

			var filter = result as FilterQueryPart;

			Assert.That(result, Is.InstanceOf<FilterQueryPart>());
			Assert.That(filter.FilterExpression, Is.Not.Null);
			Assert.That(filter.FilterExpression.ExpressionType, Is.EqualTo(FilterExpressionType.Binary));
			Assert.That(filter.FilterExpression, Is.InstanceOf<BinaryFilterExpression>());

			var binaryExpression = filter.FilterExpression as BinaryFilterExpression;

			Assert.That(binaryExpression.Operator, Is.EqualTo(FilterExpressionOperator.LessThan));
			Assert.That(binaryExpression.Left, Is.InstanceOf<MemberAccessFilterExpression>());

			var left = binaryExpression.Left as MemberAccessFilterExpression;

			Assert.That(left.Instance, Is.Null);
			Assert.That(left.Member, Is.EqualTo("TestInteger"));

			Assert.That(binaryExpression.Right, Is.InstanceOf<ConstantFilterExpression>());

			var right = binaryExpression.Right as ConstantFilterExpression;

			Assert.That(right.Type, Is.EqualTo(typeof(byte)));
			Assert.That(right.Value, Is.EqualTo(3));

			Assert.That(result.ToString(), Is.EqualTo("$filter=(TestInteger lt 3)"));
		}

		[Test]
		public void Parse_TrimOfTestStringEqualToHelloWorld_ReturnsCorrectQueryPart()
		{
			var result = _strategy.Parse(Type, "trim(TestString) eq 'hello, world'");

			var filter = result as FilterQueryPart;

			Assert.That(result, Is.InstanceOf<FilterQueryPart>());
			Assert.That(filter.FilterExpression, Is.Not.Null);
			Assert.That(filter.FilterExpression.ExpressionType, Is.EqualTo(FilterExpressionType.Binary));
			Assert.That(filter.FilterExpression, Is.InstanceOf<BinaryFilterExpression>());

			var binaryExpression = filter.FilterExpression as BinaryFilterExpression;

			Assert.That(binaryExpression.Operator, Is.EqualTo(FilterExpressionOperator.Equal));
			Assert.That(binaryExpression.Left, Is.InstanceOf<MethodCallFilterExpression>());

			var left = binaryExpression.Left as MethodCallFilterExpression;

			Assert.That(left.Method, Is.EqualTo(Function.Trim));
			Assert.That(left.Arguments.Count(), Is.EqualTo(1));
			Assert.That(left.Arguments.First().ExpressionType, Is.EqualTo(FilterExpressionType.MemberAccess));

			var argument = left.Arguments.First() as MemberAccessFilterExpression;
			Assert.That(argument.Instance, Is.EqualTo(null));
			Assert.That(argument.Member, Is.EqualTo("TestString"));

			Assert.That(binaryExpression.Right, Is.InstanceOf<ConstantFilterExpression>());

			var right = binaryExpression.Right as ConstantFilterExpression;

			Assert.That(right.Type, Is.EqualTo(typeof(string)));
			Assert.That(right.Value, Is.EqualTo("hello, world"));

			Assert.That(result.ToString(), Is.EqualTo("$filter=(trim(TestString) eq 'hello, world')"));
		}

		[Test]
		public void Parse_ReplaceHelWithHellNotEqualHelloWorld_ReturnsCorrectQueryPart()
		{
			var result = _strategy.Parse(Type, "replace(TestString, 'hel', 'hell') ne 'helloworld'");

			var filter = result as FilterQueryPart;

			Assert.That(result, Is.InstanceOf<FilterQueryPart>());
			Assert.That(filter.FilterExpression, Is.Not.Null);
			Assert.That(filter.FilterExpression.ExpressionType, Is.EqualTo(FilterExpressionType.Binary));
			Assert.That(filter.FilterExpression, Is.InstanceOf<BinaryFilterExpression>());

			var binaryExpression = filter.FilterExpression as BinaryFilterExpression;

			Assert.That(binaryExpression.Operator, Is.EqualTo(FilterExpressionOperator.NotEqual));
			Assert.That(binaryExpression.Left, Is.InstanceOf<MethodCallFilterExpression>());

			var left = binaryExpression.Left as MethodCallFilterExpression;

			Assert.That(left.Method, Is.EqualTo(Function.Replace));
			Assert.That(left.Arguments.Count(), Is.EqualTo(3));

			var memberToActUpon = left.Arguments.ElementAt(0) as MemberAccessFilterExpression;

			Assert.That(memberToActUpon.Instance, Is.Null);
			Assert.That(memberToActUpon.Member, Is.EqualTo("TestString"));

			var stringToReplace = left.Arguments.ElementAt(1) as ConstantFilterExpression;

			Assert.That(stringToReplace.Type, Is.EqualTo(typeof(string)));
			Assert.That(stringToReplace.Value, Is.EqualTo("hel"));

			var stringToReplaceWith = left.Arguments.ElementAt(2) as ConstantFilterExpression;

			Assert.That(stringToReplaceWith.Type, Is.EqualTo(typeof(string)));
			Assert.That(stringToReplaceWith.Value, Is.EqualTo("hell"));

			Assert.That(binaryExpression.Right, Is.InstanceOf<ConstantFilterExpression>());

			var right = binaryExpression.Right as ConstantFilterExpression;

			Assert.That(right.Type, Is.EqualTo(typeof(string)));
			Assert.That(right.Value, Is.EqualTo("helloworld"));

			Assert.That(result.ToString(), Is.EqualTo("$filter=(replace(TestString, 'hel', 'hell') ne 'helloworld')"));
		}

		[Test]
		public void Parse_VerifyOrderOfOperations_ReturnsCorrectQueryPart()
		{
			var result = _strategy.Parse(Type, "3 add 4 mul 2 eq 11");

			var filter = result as FilterQueryPart;

			Assert.That(result, Is.InstanceOf<FilterQueryPart>());
			Assert.That(filter.FilterExpression, Is.Not.Null);
			Assert.That(filter.FilterExpression.ExpressionType, Is.EqualTo(FilterExpressionType.Binary));
			Assert.That(filter.FilterExpression, Is.InstanceOf<BinaryFilterExpression>());

			var binaryExpression = filter.FilterExpression as BinaryFilterExpression;

			Assert.That(binaryExpression.Operator, Is.EqualTo(FilterExpressionOperator.Equal));
			Assert.That(binaryExpression.Left, Is.InstanceOf<BinaryFilterExpression>());

			var mathExpression = binaryExpression.Left as BinaryFilterExpression;
			Assert.That(mathExpression.Operator, Is.EqualTo(FilterExpressionOperator.Add));

			var threeExpression = mathExpression.Left as ConstantFilterExpression;
			Assert.That(threeExpression.Type, Is.EqualTo(typeof(byte)));
			Assert.That(threeExpression.Value, Is.EqualTo(3));

			var multiplicationExpression = mathExpression.Right as BinaryFilterExpression;
			Assert.That(multiplicationExpression.Operator, Is.EqualTo(FilterExpressionOperator.Multiply));

			var fourExpression = multiplicationExpression.Left as ConstantFilterExpression;
			Assert.That(fourExpression.Type, Is.EqualTo(typeof(byte)));
			Assert.That(fourExpression.Value, Is.EqualTo(4));

			var twoExpression = multiplicationExpression.Right as ConstantFilterExpression;
			Assert.That(twoExpression.Type, Is.EqualTo(typeof(byte)));
			Assert.That(twoExpression.Value, Is.EqualTo(2));

			var elevenExpression = binaryExpression.Right as ConstantFilterExpression;

			Assert.That(elevenExpression.Type, Is.EqualTo(typeof(byte)));
			Assert.That(elevenExpression.Value, Is.EqualTo(11));

			Assert.That(result.ToString(), Is.EqualTo("$filter=((3 add (4 mul 2)) eq 11)"));
		}

		// TODO: expressions as arguments of functions

		// TODO: expressions containing parenthesis
	}
}
