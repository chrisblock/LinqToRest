using LinqToRest.OData.Literals;
using LinqToRest.OData.Literals.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Literals
{
	[TestFixture]
	public class TimeLiteralTests
	{
		private ILiteral _literal = new TimeLiteral();

		[SetUp]
		public void TestSetUp()
		{
			_literal = new TimeLiteral();
		}
	}
}
