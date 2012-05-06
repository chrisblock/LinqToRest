using System.Linq;

using LinqToRest.OData;
using LinqToRest.OData.Filters;
using LinqToRest.OData.Parsing;
using LinqToRest.OData.Parsing.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.ODataQueryParserStrategies
{
	[TestFixture]
	public class FilterQueryPartParserStrategyTests
	{
		private const ODataQueryPartType Type = ODataQueryPartType.Filter;
		private readonly IODataQueryParserStrategy _strategy = new FilterQueryPartParserStrategy();

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
			Assert.That(filter.FilterExpression.ExpressionType, Is.EqualTo(ODataQueryFilterExpressionType.Binary));
			Assert.That(filter.FilterExpression, Is.InstanceOf<ODataQueryBinaryFilterExpression>());

			var binaryExpression = filter.FilterExpression as ODataQueryBinaryFilterExpression;

			Assert.That(binaryExpression.Operator, Is.EqualTo(ODataQueryFilterExpressionOperator.LessThan));
			Assert.That(binaryExpression.Left, Is.InstanceOf<ODataQueryMemberAccessFilterExpression>());

			var left = binaryExpression.Left as ODataQueryMemberAccessFilterExpression;

			Assert.That(left.Instance, Is.Null);
			Assert.That(left.Member, Is.EqualTo("TestInteger"));

			Assert.That(binaryExpression.Right, Is.InstanceOf<ODataQueryConstantFilterExpression>());

			var right = binaryExpression.Right as ODataQueryConstantFilterExpression;

			Assert.That(right.Type, Is.EqualTo(typeof(int)));
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
			Assert.That(filter.FilterExpression.ExpressionType, Is.EqualTo(ODataQueryFilterExpressionType.Binary));
			Assert.That(filter.FilterExpression, Is.InstanceOf<ODataQueryBinaryFilterExpression>());

			var binaryExpression = filter.FilterExpression as ODataQueryBinaryFilterExpression;

			Assert.That(binaryExpression.Operator, Is.EqualTo(ODataQueryFilterExpressionOperator.Equal));
			Assert.That(binaryExpression.Left, Is.InstanceOf<ODataQueryMethodCallFilterExpression>());

			var left = binaryExpression.Left as ODataQueryMethodCallFilterExpression;

			Assert.That(left.Method, Is.EqualTo(Function.Trim));
			Assert.That(left.Arguments.Count(), Is.EqualTo(1));
			Assert.That(left.Arguments.First().ExpressionType, Is.EqualTo(ODataQueryFilterExpressionType.MemberAccess));

			var argument = left.Arguments.First() as ODataQueryMemberAccessFilterExpression;
			Assert.That(argument.Instance, Is.EqualTo(null));
			Assert.That(argument.Member, Is.EqualTo("TestString"));

			Assert.That(binaryExpression.Right, Is.InstanceOf<ODataQueryConstantFilterExpression>());

			var right = binaryExpression.Right as ODataQueryConstantFilterExpression;

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
			Assert.That(filter.FilterExpression.ExpressionType, Is.EqualTo(ODataQueryFilterExpressionType.Binary));
			Assert.That(filter.FilterExpression, Is.InstanceOf<ODataQueryBinaryFilterExpression>());

			var binaryExpression = filter.FilterExpression as ODataQueryBinaryFilterExpression;

			Assert.That(binaryExpression.Operator, Is.EqualTo(ODataQueryFilterExpressionOperator.NotEqual));
			Assert.That(binaryExpression.Left, Is.InstanceOf<ODataQueryMethodCallFilterExpression>());

			var left = binaryExpression.Left as ODataQueryMethodCallFilterExpression;

			Assert.That(left.Method, Is.EqualTo(Function.Replace));
			Assert.That(left.Arguments.Count(), Is.EqualTo(3));

			var memberToActUpon = left.Arguments.ElementAt(0) as ODataQueryMemberAccessFilterExpression;

			Assert.That(memberToActUpon.Instance, Is.Null);
			Assert.That(memberToActUpon.Member, Is.EqualTo("TestString"));

			var stringToReplace = left.Arguments.ElementAt(1) as ODataQueryConstantFilterExpression;

			Assert.That(stringToReplace.Type, Is.EqualTo(typeof(string)));
			Assert.That(stringToReplace.Value, Is.EqualTo("hel"));

			var stringToReplaceWith = left.Arguments.ElementAt(2) as ODataQueryConstantFilterExpression;

			Assert.That(stringToReplaceWith.Type, Is.EqualTo(typeof(string)));
			Assert.That(stringToReplaceWith.Value, Is.EqualTo("hell"));

			Assert.That(binaryExpression.Right, Is.InstanceOf<ODataQueryConstantFilterExpression>());

			var right = binaryExpression.Right as ODataQueryConstantFilterExpression;

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
			Assert.That(filter.FilterExpression.ExpressionType, Is.EqualTo(ODataQueryFilterExpressionType.Binary));
			Assert.That(filter.FilterExpression, Is.InstanceOf<ODataQueryBinaryFilterExpression>());

			var binaryExpression = filter.FilterExpression as ODataQueryBinaryFilterExpression;

			Assert.That(binaryExpression.Operator, Is.EqualTo(ODataQueryFilterExpressionOperator.Equal));
			Assert.That(binaryExpression.Left, Is.InstanceOf<ODataQueryBinaryFilterExpression>());

			var mathExpression = binaryExpression.Left as ODataQueryBinaryFilterExpression;
			Assert.That(mathExpression.Operator, Is.EqualTo(ODataQueryFilterExpressionOperator.Add));

			var threeExpression = mathExpression.Left as ODataQueryConstantFilterExpression;
			Assert.That(threeExpression.Type, Is.EqualTo(typeof(int)));
			Assert.That(threeExpression.Value, Is.EqualTo(3));

			var multiplicationExpression = mathExpression.Right as ODataQueryBinaryFilterExpression;
			Assert.That(multiplicationExpression.Operator, Is.EqualTo(ODataQueryFilterExpressionOperator.Multiply));

			var fourExpression = multiplicationExpression.Left as ODataQueryConstantFilterExpression;
			Assert.That(fourExpression.Type, Is.EqualTo(typeof(int)));
			Assert.That(fourExpression.Value, Is.EqualTo(4));

			var twoExpression = multiplicationExpression.Right as ODataQueryConstantFilterExpression;
			Assert.That(twoExpression.Type, Is.EqualTo(typeof(int)));
			Assert.That(twoExpression.Value, Is.EqualTo(2));

			var elevenExpression = binaryExpression.Right as ODataQueryConstantFilterExpression;

			Assert.That(elevenExpression.Type, Is.EqualTo(typeof(int)));
			Assert.That(elevenExpression.Value, Is.EqualTo(11));

			Assert.That(result.ToString(), Is.EqualTo("$filter=((3 add (4 mul 2)) eq 11)"));
		}

		// TODO: expressions as arguments of functions

		// TODO: expressions containing parenthesis
	}
}
