using LinqToRest.OData.Literals;
using LinqToRest.OData.Literals.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Literals
{
	[TestFixture]
	public class NullLiteralTests
	{
		private ILiteral _literal = new NullLiteral();

		[SetUp]
		public void TestSetUp()
		{
			_literal = new NullLiteral();
		}
	}
}
