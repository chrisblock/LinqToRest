using LinqToRest.OData.Literals;
using LinqToRest.OData.Literals.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Literals
{
	[TestFixture]
	public class ShortLiteralTests
	{
		private ILiteral _literal = new ShortLiteral();

		[SetUp]
		public void TestSetUp()
		{
			_literal = new ShortLiteral();
		}
	}
}
