using System.Collections.Generic;
using System.Linq;

using LinqToRest.Http;
using LinqToRest.Linq;
using LinqToRest.Serialization;

namespace LinqToRest
{
	public static class WebServices
	{
		public static IQueryable<T> Find<T>()
		{
			return RestQueryableFactory.Create<T>();
		}

		public static IEnumerable<T> GetAll<T>()
		{
			return Find<T>().ToList();
		}

		public static T Get<T>(object id)
		{
			var httpService = DependencyResolver.Current.GetInstance<IHttpService>();
			var serializer = DependencyResolver.Current.GetInstance<ISerializer>();

			var uri = typeof (T).GetServiceUri();

			// TODO: use id to get only the single item
			var response = httpService.Get(uri);

			return serializer.Deserialize<T>(response);
		}
	}
}
