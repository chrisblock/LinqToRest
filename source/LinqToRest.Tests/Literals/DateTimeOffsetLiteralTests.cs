using LinqToRest.OData.Literals;
using LinqToRest.OData.Literals.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Literals
{
	[TestFixture]
	public class DateTimeOffsetLiteralTests
	{
		private ILiteral _literal = new DateTimeOffsetLiteral();

		[SetUp]
		public void TestSetUp()
		{
			_literal = new DateTimeOffsetLiteral();
		}
	}
}
