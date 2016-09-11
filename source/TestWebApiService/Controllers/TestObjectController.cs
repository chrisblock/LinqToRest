using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Changes;

using DataModel.Tests;

using LinqToRest.Server.WebApi;

namespace TestWebApiService.Controllers
{
	[RoutePrefix(@"api/TestObjects")]
	public class TestObjectController : ApiController
	{
		private static readonly ICollection<TestObject> Items;
		
		static TestObjectController()
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

		[HttpGet]
		[ODataQuery]
		[Route("", Name = "GetTestObjectsApi")]
		public IHttpActionResult Get()
		{
			var result = Items.AsQueryable();

			return Ok(result);
		}

		[HttpGet]
		[Route("{id:int}", Name = "GetTestObjectApi")]
		public IHttpActionResult Get(int id)
		{
			var result = Items.SingleOrDefault(x => x.Id == id);

			return Ok(result);
		}

		[HttpPost]
		[Route("", Name = "AddTestObjectsApi")]
		public IHttpActionResult Post(TestObject value)
		{
			Items.Add(value);

			return Created(Url.Route("GetTestObjectsApi", null), value);
		}

		[HttpPut]
		[Route("{id:int}", Name = "UpdateTestObjectsApi")]
		public IHttpActionResult Put(int id, ChangeSet<TestObject> changeSet)
		{
			var item = Items.SingleOrDefault(x => x.Id == id);

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
			var item = Items.SingleOrDefault(x => x.Id == id);

			IHttpActionResult result = NotFound();

			if (item != null)
			{
				Items.Remove(item);

				result = Ok();
			}

			return result;
		}
	}
}
