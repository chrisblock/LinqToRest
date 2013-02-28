// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using Changes;

using DataModel.Tests;

using LinqToRest.Client.Http;
using LinqToRest.Client.Linq;
using LinqToRest.OData.Impl;

using NUnit.Framework;

using Rhino.Mocks;

namespace LinqToRest.Client.Tests
{
	[TestFixture]
	public class EndpointTests
	{
		private IHttpService _mockHttpService;
		private Endpoint _endpoint;

		private static IEnumerable<TestObject> GenerateResults(int count)
		{
			return Enumerable.Range(count % 2, count).Select(x => new TestObject
			{
				Id = x % count,
				TestProperty = String.Format("{0}", x % count)
			});
		}

		private static void SetExpectation<T>(IHttpService httpService, Uri expectedUri, IEnumerable<T> result)
		{
			httpService
				.Expect(x => x.Get<IEnumerable<T>>(expectedUri))
				.Return(result);
		}

		[SetUp]
		public void TestSetUp()
		{
			var uriFactory = new UriFactory(new Uri("http://localhost:6789/api/"));

			_mockHttpService = MockRepository.GenerateStub<IHttpService>();
			var queryFactory = new DefaultODataQueryFactory();

			var queryModelTranslator = new RestQueryModelVisitor(uriFactory, queryFactory);

			var restQueryableFactory = new RestQueryableFactory(_mockHttpService, queryModelTranslator);

			_endpoint = new Endpoint(restQueryableFactory, _mockHttpService, uriFactory);
		}

		[TearDown]
		public void TestTearDown()
		{
			_endpoint = null;
			_mockHttpService = null;
		}

		[Test]
		public void Get_TestObject_GeneratesCorrectUrl()
		{
			var expected = GenerateResults(7)
				.ToList();

			SetExpectation(_mockHttpService, new Uri("http://localhost:6789/api/TestObject/?$format=json"), expected);

			var queryable = _endpoint.Get<TestObject>();

			var result = queryable.ToList();

			Assert.That(result, Is.EquivalentTo(expected));

			_mockHttpService.VerifyAllExpectations();
		}

		[Test]
		public void Get_TestObjectSelectTestProperty_GeneratesCorrectUrl()
		{
			var expected = GenerateResults(6)
				.Select(x => x.TestProperty)
				.ToList();

			SetExpectation(_mockHttpService, new Uri("http://localhost:6789/api/TestObject/?$format=json&$select=TestProperty"), expected);

			var queryable = _endpoint.Get<TestObject>().Select(x => x.TestProperty);

			var result = queryable.ToList();

			Assert.That(result, Is.EquivalentTo(expected));

			_mockHttpService.VerifyAllExpectations();
		}

		[Test]
		public void Get_TestObjectSelectAnonymousType_GeneratesCorrectUrl()
		{
			var expected = GenerateResults(9)
				.Select(x => new { x.TestProperty, x.Id })
				.ToList();

			SetExpectation(_mockHttpService, new Uri("http://localhost:6789/api/TestObject/?$format=json&$select=TestProperty, Id"), expected);

			var queryable = _endpoint.Get<TestObject>().Select(x => new { x.TestProperty, x.Id });

			var result = queryable.ToList();

			Assert.That(result, Is.EquivalentTo(expected));

			_mockHttpService.VerifyAllExpectations();
		}

		[Test]
		public void Get_TestObjectCount_GeneratesCorrectUrl()
		{
			const int expected = 13;

			_mockHttpService
				.Expect(x => x.Get<int>(new Uri("http://localhost:6789/api/TestObject/?$format=json&$count")))
				.Return(expected);

			var count = _endpoint.Get<TestObject>().Count();

			Assert.That(count, Is.EqualTo(expected));

			_mockHttpService.VerifyAllExpectations();
		}

		[Test]
		public void Get_TestObjectSkipThree_GeneratesCorrectUrl()
		{
			var expected = GenerateResults(9)
				.Skip(3)
				.ToList();

			SetExpectation(_mockHttpService, new Uri("http://localhost:6789/api/TestObject/?$format=json&$skip=3"), expected);

			var queryable = _endpoint.Get<TestObject>().Skip(3);

			var result = queryable.ToList();

			Assert.That(result, Is.EquivalentTo(expected));

			_mockHttpService.VerifyAllExpectations();
		}

		[Test]
		public void Get_TestObjectOrderBy_GeneratesCorrectUrl()
		{
			var expected = GenerateResults(9)
				.OrderBy(x => x.TestProperty)
				.ToList();

			SetExpectation(_mockHttpService, new Uri("http://localhost:6789/api/TestObject/?$format=json&$orderby=TestProperty asc"), expected);

			var queryable = _endpoint.Get<TestObject>().OrderBy(x => x.TestProperty);

			var result = queryable.ToList();

			Assert.That(result, Is.EquivalentTo(expected));

			_mockHttpService.VerifyAllExpectations();
		}

		[Test]
		public void Get_TestObjectTakeThree_GeneratesCorrectUrl()
		{
			var expected = GenerateResults(9)
				.Take(3)
				.ToList();

			SetExpectation(_mockHttpService, new Uri("http://localhost:6789/api/TestObject/?$format=json&$top=3"), expected);

			var queryable = _endpoint.Get<TestObject>().Take(3);

			var result = queryable.ToList();

			Assert.That(result, Is.EquivalentTo(expected));

			_mockHttpService.VerifyAllExpectations();
		}

		[Test]
		public void Get_TestObjectWhereTestPropertyDoesNotEqualNull_GeneratesCorrectUrl()
		{
			var expected = GenerateResults(9)
				.Where(x => x.TestProperty != null)
				.ToList();

			SetExpectation(_mockHttpService, new Uri("http://localhost:6789/api/TestObject/?$format=json&$filter=(TestProperty ne Null)"), expected);

			var queryable = _endpoint.Get<TestObject>().Where(x => x.TestProperty != null);

			var result = queryable.ToList();

			Assert.That(result, Is.EquivalentTo(expected));

			_mockHttpService.VerifyAllExpectations();
		}

		[Test]
		public void Put_IdAndChangeSet_GeneratesCorrectUrilAndReturnsOK()
		{
			var id = Guid.NewGuid();
			var changes = new ChangeSet<TestObject>();

			changes.SetChangeFor(x => x.TestProperty, "Hello World");

			const HttpStatusCode expected = HttpStatusCode.OK;

			_mockHttpService
				.Expect(x => x.Put(new Uri(String.Format("http://localhost:6789/api/TestObject/{0}", id)), changes))
				.Return(expected);

			var result = _endpoint.Put(id, changes);

			Assert.That(result, Is.EqualTo(expected));

			_mockHttpService.VerifyAllExpectations();
		}

		[Test]
		public void Post_NewObject_GeneratesCorrectUrilAndReturnsOK()
		{
			var obj = new TestObject
			{
				Id = 1,
				TestProperty = "Hello, World!"
			};

			const HttpStatusCode expected = HttpStatusCode.OK;

			_mockHttpService
				.Expect(x => x.Post(new Uri("http://localhost:6789/api/TestObject/"), obj))
				.Return(expected);

			var result = _endpoint.Post(obj);

			Assert.That(result, Is.EqualTo(expected));

			_mockHttpService.VerifyAllExpectations();
		}

		[Test]
		public void Post_NewObjectArray_GeneratesCorrectUrilAndReturnsOK()
		{
			const int count = 4;

			var objects = GenerateResults(count)
				.ToArray();

			var expected = Enumerable.Range(0, count)
				.Select(x => HttpStatusCode.OK)
				.ToArray();

			for (int i = 0; i < count; i++)
			{
				_mockHttpService
					.Expect(x => x.Post(new Uri("http://localhost:6789/api/TestObject/"), objects[i]))
					.Return(expected[i]);
			}

			var result = _endpoint.Post(objects);

			Assert.That(result, Is.EquivalentTo(expected));

			_mockHttpService.VerifyAllExpectations();
		}

		[Test]
		public void Post_NewObjectEnumerable_GeneratesCorrectUrilAndReturnsOK()
		{
			const int count = 8;

			var objects = GenerateResults(count);

			var expected = Enumerable.Range(0, count)
				.Select(x => HttpStatusCode.OK);

			for (int i = 0; i < count; i++)
			{
				int index = i;
				_mockHttpService
					.Expect(x => x.Post(new Uri("http://localhost:6789/api/TestObject/"), objects.ElementAt(index)))
					.Return(expected.ElementAt(index));
			}

			var result = _endpoint.Post(objects);

			Assert.That(result, Is.EquivalentTo(expected));

			_mockHttpService.VerifyAllExpectations();
		}
	}
}
