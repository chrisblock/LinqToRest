using System.Collections.Generic;
using System.Linq;

namespace LinqToRest.Client
{
	// TODO: this may actually work to accomplish InlineCount semantics on the client side...need to implement it on the server side first though...

	class InlineCount<T>
	{
		public int Count { get; set; }
		public IEnumerable<T> Results { get; set; }
	}

	static class InlineCountExtensions
	{
		public static InlineCount<T> InlineCount<T>(this IQueryable<T> queryable)
		{
			// TODO: queryable.Cast<InlineCount<T>>().Single() ????

			return queryable.Cast<object>().Cast<InlineCount<T>>().Single();
		}
	}
}
