using LinqToRest.OData.Literals;
using LinqToRest.OData.Literals.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Literals
{
	[TestFixture]
	public class DateTimeLiteralTests
	{
		private ILiteral _literal = new DateTimeLiteral();

		[SetUp]
		public void TestSetUp()
		{
			_literal = new DateTimeLiteral();
		}
	}
}
