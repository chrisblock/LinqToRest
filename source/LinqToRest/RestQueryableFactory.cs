using System;
using System.Linq;

using Remotion.Linq;
using Remotion.Linq.Parsing.Structure;

namespace LinqToRest
{
	internal static class RestQueryableFactory
	{
		public static IQueryable<T> Create<T>()
		{
			var url = GetUrl(typeof (T));

			var parser = QueryParser.CreateDefault();
			IQueryExecutor executor = new RestQueryExecutor(url);

			var provider = new RestQueryProvider(parser, executor);

			return new RestQueryable<T>(provider);
		}

		private static string GetUrl(Type type)
		{
			var attributes = type.GetCustomAttributes<ServiceUrlAttribute>().ToList();

			if (attributes.Any() == false)
			{
				throw new ArgumentException(String.Format("Cannot determine Service URL for type '{0}'. Please specify it using the {1} attribute.", type.Name, typeof(ServiceUrlAttribute).Name));
			}

			return attributes.Single().Url;
		}
	}
}
