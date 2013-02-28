using System.Linq;

namespace LinqToRest.Client.Linq
{
	public interface IRestQueryableFactory
	{
		IQueryable<T> Create<T>();
	}
}
