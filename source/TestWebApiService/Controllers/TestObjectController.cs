using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using Changes;

using DataModel.Tests;

using LinqToRest.Server.WebApi;

namespace TestWebApiService.Controllers
{
	public class TestObjectRepository
	{
		private ICollection<TestObject> Items { get; }

		public TestObjectRepository()
		{
			Items = new List<TestObject>
			{
				new TestObject
				{
					Id = 1,
					TestProperty = "1"
				},
				new TestObject
				{
					Id = 2,
					TestProperty = "2"
				},
				new TestObject
				{
					Id = 3,
					TestProperty = "3"
				},
				new TestObject
				{
					Id = 4,
					TestProperty = "4"
				}
			};
		}

		public IQueryable<TestObject> Get()
		{
			return Items.AsQueryable();
		}

		public TestObject Get(int id)
		{
			var item = Items.SingleOrDefault(x => x.Id == id);

			return item;
		}

		public void Add(TestObject item)
		{
			Items.Add(item);
		}

		public bool Remove(int id)
		{
			var result = false;

			var item = Get(id);

			if (item != null)
			{
				Items.Remove(item);

				result = true;
			}

			return result;
		}
	}

	[RoutePrefix(@"api/TestObjects")]
	public class TestObjectController : ApiController
	{
		private readonly TestObjectRepository _repository;

		public TestObjectController(TestObjectRepository repository)
		{
			_repository = repository;
		}

		[HttpGet]
		[ODataQuery]
		[Route("", Name = "GetTestObjectsApi")]
		public IHttpActionResult Get()
		{
			var result = _repository.Get();

			return Ok(result);
		}

		[HttpGet]
		[Route("{id:int}", Name = "GetTestObjectApi")]
		public IHttpActionResult Get(int id)
		{
			var item = _repository.Get(id);

			IHttpActionResult result = NotFound();

			if (item != null)
			{
				result = Ok(item);
			}

			return result;
		}

		[HttpPost]
		[Route("", Name = "AddTestObjectsApi")]
		public IHttpActionResult Post(TestObject value)
		{
			_repository.Add(value);

			return Created(Url.Route("GetTestObjectsApi", null), value);
		}

		[HttpPut]
		[Route("{id:int}", Name = "UpdateTestObjectsApi")]
		public IHttpActionResult Put(int id, ChangeSet<TestObject> changeSet)
		{
			var item = _repository.Get(id);

			IHttpActionResult result = Ok();

			if (item != null)
			{
				changeSet.ApplyChanges(ref item);
			}
			else
			{
				result = NotFound();
			}

			return result;
		}

		[HttpDelete]
		[Route("{id:int}", Name = "DeleteTestObjectsApi")]
		public IHttpActionResult Delete(int id)
		{
			IHttpActionResult result = NotFound();

			if (_repository.Remove(id))
			{
				result = Ok();
			}

			return result;
		}
	}
}
