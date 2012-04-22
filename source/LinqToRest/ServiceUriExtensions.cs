using System;
using System.Linq;

namespace LinqToRest
{
	public static class ServiceUriExtensions
	{
		public static Uri GetBaseServiceUri<T>(this T item)
		{
			var url = typeof (T).GetCustomAttributes<ServiceUrlAttribute>().Single().Url;

			return new Uri(url);
		}

		public static Uri GetItemUri<T, TProperty>(this T item, Func<T, TProperty> idAccessor)
		{
			var id = idAccessor(item);

			var baseUri = item.GetBaseServiceUri();

			Uri result;
			if (Uri.TryCreate(String.Format("{0}/{1}", baseUri, id), UriKind.Absolute, out result) == false)
			{
				throw new ArgumentException(String.Format("Error creating URI '{0}/{1}'.", baseUri, id));
			}

			return result;
		}
	}
}
