// ReSharper disable InconsistentNaming

using System.Linq;
using System.Net;

using Changes;

using DataModel.Tests;

using LinqToRest.Client;

using NUnit.Framework;

using TestWebApiService.Controllers;

using WebApi.SelfHost;

namespace LinqToRest.IntegrationTests
{
	[TestFixture]
	public class WebServicesTests
	{
		private IWebServiceHost _host;

		[SetUp]
		public void TestSetUp()
		{
			_host = WebServiceHostFactory.CreateFor<TestObjectController>();

			var defaultRoute = new RouteCounfiguration
			{
				Name = "DefaultRoute",
				Template = "api/{controller}"
				// TODO: this anonymous type is not serializable and is thus failing the cross-domain marshaling
				//Defaults = new
				//{
				//    id = RouteParameter.Optional
				//}
			};

			var defaultRouteWithId = new RouteCounfiguration
			{
				Name = "DefaultRouteWithId",
				Template = "api/{controller}/{id}"
			};

			_host.Start(new RouteConfigurationTable(defaultRoute, defaultRouteWithId));
		}

		[TearDown]
		public void TestTearDown()
		{
			_host.Dispose();

			_host = null;
		}

		[Test]
		public void Get_NoConstraints_ReturnsAllItems()
		{
			var result = WebServices.Get<TestObject>().ToList();

			Assert.That(result.Count, Is.EqualTo(4));
		}

		[Test]
		public void Get_NoConstraintsSelectId_ReturnsAllIds()
		{
			var result = WebServices.Get<TestObject>().Select(x => x.Id).ToList();

			Assert.That(result.Count, Is.EqualTo(4));
		}

		[Test]
		public void Get_WhereIdEqualsThree_ReturnsListWithSingleResult()
		{
			var result = WebServices.Get<TestObject>().Where(x => x.Id == 3).ToList();

			Assert.That(result.Count, Is.EqualTo(1));
			Assert.That(result.Single().Id, Is.EqualTo(3));
			Assert.That(result.Single().TestProperty, Is.EqualTo("3"));
		}

		[Test]
		public void Get_WhereIdModTwoEqualsZero_ReturnsListWithTwoResults()
		{
			var result = WebServices.Get<TestObject>().Where(x => x.Id % 2 == 0).ToList();

			Assert.That(result.Count, Is.EqualTo(2));
			Assert.That(result.ElementAt(0).Id, Is.EqualTo(2));
			Assert.That(result.ElementAt(0).TestProperty, Is.EqualTo("2"));
			Assert.That(result.ElementAt(1).Id, Is.EqualTo(4));
			Assert.That(result.ElementAt(1).TestProperty, Is.EqualTo("4"));
		}

		[Test]
		public void Put_SetPropertyTo6_SetsProperty()
		{
			var changeSet = new ChangeSet<TestObject>();

			changeSet.SetChangeFor(x => x.TestProperty, "6");

			var putStatus = WebServices.Put(3, changeSet);

			var result = WebServices.Get<TestObject>().Single(x => x.Id == 3);

			Assert.That(putStatus, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(result.Id, Is.EqualTo(3));
			Assert.That(result.TestProperty, Is.EqualTo("6"));
		}

		[Test]
		public void Post_NewItem_AddsItem()
		{
			var item = new TestObject
			{
				Id = 42,
				TestProperty = "Hello, World."
			};

			var postStatus = WebServices.Post(item);

			var result = WebServices.Get<TestObject>().ToList();

			Assert.That(postStatus, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(result, Contains.Item(item));
		}

		[Test]
		public void Delete_ItemWithId3_RemovesItem()
		{
			var deleteStatus = WebServices.Delete<TestObject>(3);

			var result = WebServices.Get<TestObject>().Single(x => x.Id == 3);

			Assert.That(deleteStatus, Is.EqualTo(HttpStatusCode.OK));
			Assert.That(result, Is.Null);
		}
	}
}
