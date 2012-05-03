using System;

using NUnit.Framework;

namespace LinqToRest.Tests
{
	[TestFixture]
	public class DependencyResolverTests
	{
		[Test]
		public void SetDependencyResolver_Null_ThrowsArgumentNullException()
		{
			Assert.That(() => DependencyResolver.SetDependencyResolver(null), Throws.InstanceOf<ArgumentNullException>());
		}
	}
}
