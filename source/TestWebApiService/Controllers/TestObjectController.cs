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

		[ODataQuery]
		public HttpResponseMessage Get()
		{
			return Request.CreateResponse(HttpStatusCode.OK, Items.AsQueryable());
		}

		public TestObject Get(int id)
		{
			return Items.SingleOrDefault(x => x.Id == id);
		}

		public HttpResponseMessage Post(TestObject value)
		{
			Items.Add(value);

			return Request.CreateResponse(HttpStatusCode.Created);
		}

		public HttpResponseMessage Put(int id, ChangeSet<TestObject> changeSet)
		{
			var item = Items.SingleOrDefault(x => x.Id == id);

			var result = Request.CreateResponse(HttpStatusCode.OK);

			if (item != null)
			{
				changeSet.ApplyChanges(ref item);
			}
			else
			{
				result = Request.CreateResponse(HttpStatusCode.NotFound);
			}

			return result;
		}

		public HttpResponseMessage Delete(int id)
		{
			var item = Items.SingleOrDefault(x => x.Id == id);

			if (item != null)
			{
				Items.Remove(item);
			}

			// TODO: if the object does not exist, is it really an OK response code???
			return Request.CreateResponse(HttpStatusCode.OK);
		}
	}
}
