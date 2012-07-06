using System.Collections.Generic;
using System.Linq;
using System.Net;

using Changes;

using LinqToRest.Client.Http;
using LinqToRest.Client.Linq;

namespace LinqToRest.Client
{
	public static class WebServices
	{
		public static IQueryable<T> Get<T>()
		{
			return RestQueryableFactory.Create<T>();
		}

		public static HttpStatusCode Put<T>(object id, ChangeSet<T> item)
		{
			var httpService = DependencyResolver.Current.GetInstance<IHttpService>();

			var url = typeof (T).GetCustomAttributes<ServiceUrlAttribute>().Single().Url;

			return httpService.Put(url, item);
		}

		public static IEnumerable<HttpStatusCode> Post<T>(params T[] items)
		{
			return Post(items.AsEnumerable());
		}

		public static IEnumerable<HttpStatusCode> Post<T>(IEnumerable<T> items)
		{
			return items.Select(Post);
		}

		public static HttpStatusCode Post<T>(T item)
		{
			var httpService = DependencyResolver.Current.GetInstance<IHttpService>();

			var url = typeof(T).GetCustomAttributes<ServiceUrlAttribute>().Single().Url;

			return httpService.Post(url, item);
		}

		public static HttpStatusCode Delete<T>(object id)
		{
			var httpService = DependencyResolver.Current.GetInstance<IHttpService>();

			var url = typeof(T).GetCustomAttributes<ServiceUrlAttribute>().Single().Url;

			return httpService.Delete(url);
		}
	}
}
