using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;

using Changes;

using LinqToRest.Client.Http;
using LinqToRest.Client.Linq;
using LinqToRest.Serialization;

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
			var serializer = DependencyResolver.Current.GetInstance<ISerializer>();

			var url = typeof (T).GetCustomAttributes<ServiceUrlAttribute>().Single().Url;

			var content = new StringContent(serializer.Serialize(item), Encoding.UTF8, serializer.MediaType);

			return httpService.Put(url, content);
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
			var serializer = DependencyResolver.Current.GetInstance<ISerializer>();

			var url = typeof(T).GetCustomAttributes<ServiceUrlAttribute>().Single().Url;

			var content = new StringContent(serializer.Serialize(item), Encoding.UTF8, serializer.MediaType);

			return httpService.Post(url, content);
		}

		public static HttpStatusCode Delete<T>(object id)
		{
			var httpService = DependencyResolver.Current.GetInstance<IHttpService>();

			var url = typeof(T).GetCustomAttributes<ServiceUrlAttribute>().Single().Url;

			return httpService.Delete(url);
		}
	}
}
