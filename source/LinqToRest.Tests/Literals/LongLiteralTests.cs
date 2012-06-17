using LinqToRest.OData.Literals;
using LinqToRest.OData.Literals.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests.Literals
{
	[TestFixture]
	public class LongLiteralTests
	{
		private ILiteral _literal = new LongLiteral();

		[SetUp]
		public void TestSetUp()
		{
			_literal = new LongLiteral();
		}
	}
}
