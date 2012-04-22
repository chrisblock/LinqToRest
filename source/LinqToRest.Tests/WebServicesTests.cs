using System.Linq;

using LinqToRest.Tests.Mocks;

using NUnit.Framework;

namespace LinqToRest.Tests
{
	[TestFixture]
	public class WebServicesTests
	{
		[SetUp]
		public void TestSetUp()
		{
			DependencyResolver.SetDependencyResolver(new MockDependencyResolver());
		}

		[Test]
		public void Find_TypeWithoutServiceUrlAttributeSpecified_ThrowsArgumentException()
		{
			Assert.That(() => WebServices.Find<int>().ToList(), Throws.ArgumentException);
		}

		[Test]
		public void Find_TestObject_GeneratesCorrectUrl()
		{
			var queryable = WebServices.Find<TestObject>();

			var result = queryable.ToList();

			Assert.That(result, Is.EquivalentTo(MockHttpService.Result));

			var requestedUrl = MockHttpService.RequestedUrls.Pop();

			Assert.That(requestedUrl, Is.EqualTo("http://localhost/api/TestObjects?$format=json"));
		}

		[Test]
		public void Find_TestObjectSkipThree_GeneratesCorrectUrl()
		{
			var skipConstant = 3;
			var queryable = WebServices.Find<TestObject>().Skip(skipConstant);

			var result = queryable.ToList();

			Assert.That(result, Is.EquivalentTo(MockHttpService.Result));

			var requestedUrl = MockHttpService.RequestedUrls.Pop();

			Assert.That(requestedUrl, Is.EqualTo("http://localhost/api/TestObjects?$format=json&$skip=3"));
		}

		[Test]
		public void Find_TestObjectOrderBy_GeneratesCorrectUrl()
		{
			var queryable = WebServices.Find<TestObject>().OrderBy(x => x.TestProperty);

			var result = queryable.ToList();

			Assert.That(result, Is.EquivalentTo(MockHttpService.Result));

			var requestedUrl = MockHttpService.RequestedUrls.Pop();

			Assert.That(requestedUrl, Is.EqualTo("http://localhost/api/TestObjects?$format=json&$orderby=TestProperty asc"));
		}
	}
}
