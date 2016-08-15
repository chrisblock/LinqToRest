using System;
using System.Linq;

namespace LinqToRest.Client.Linq
{
	public interface IRestQueryableFactory
	{
		IQueryable<T> Create<T>(string url);
		IQueryable<T> Create<T>(Uri uri);
	}
}
