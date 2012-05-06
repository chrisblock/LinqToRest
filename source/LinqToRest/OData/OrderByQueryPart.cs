using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqToRest.OData
{
	public class OrderByQueryPart : ODataQueryPart
	{
		private readonly ICollection<ODataOrdering> _orderings = new List<ODataOrdering>();

		public override ODataQueryPartType QueryPartType { get { return ODataQueryPartType.OrderBy; } }

		public ICollection<ODataOrdering> Orderings { get { return _orderings; } }

		public OrderByQueryPart(params ODataOrdering[] orderings)
		{
			foreach (var ordering in orderings)
			{
				_orderings.Add(ordering);
			}
		}

		public void AddOrdering(ODataOrdering ordering)
		{
			_orderings.Add(ordering);
		}

		public override string ToString()
		{
			var result = String.Empty;

			if (_orderings.Any())
			{
				var orderingPredicate = String.Join(", ", _orderings.Select(x => x.ToString()));

				result = BuildParameterString(orderingPredicate);
			}

			return result;
		}
	}
}
