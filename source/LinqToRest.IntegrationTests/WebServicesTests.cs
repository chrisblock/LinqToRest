// ReSharper disable InconsistentNaming

using System.Linq;

using LinqToRest.Client;

using NUnit.Framework;

using TestWebApiService.Models;

namespace LinqToRest.IntegrationTests
{
	[TestFixture]
	public class WebServicesTests
	{
		private WebServiceHost _host;

		[SetUp]
		public void TestSetUp()
		{
			_host = new WebServiceHost();
		}

		[TearDown]
		public void TestTearDown()
		{
			_host.Dispose();

			_host = null;
		}

		[Test]
		public void Find_WhereIdEqualsThree_ReturnsListWithSingleResult()
		{
			var result = WebServices.Get<TestModel>().Where(x => x.Id == 3).ToList();

			Assert.That(result.Count, Is.EqualTo(1));
			Assert.That(result.Single().Id, Is.EqualTo(3));
			Assert.That(result.Single().TestProperty, Is.EqualTo("3"));
		}

		[Test]
		public void Find_WhereIdModTwoEqualsZero_ReturnsListWithTwoResults()
		{
			var result = WebServices.Get<TestModel>().Where(x => x.Id % 2 == 0).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
			Assert.That(result.ElementAt(0).Id, Is.EqualTo(2));
			Assert.That(result.ElementAt(0).TestProperty, Is.EqualTo("2"));
			Assert.That(result.ElementAt(1).Id, Is.EqualTo(4));
			Assert.That(result.ElementAt(1).TestProperty, Is.EqualTo("4"));
		}
	}
}
