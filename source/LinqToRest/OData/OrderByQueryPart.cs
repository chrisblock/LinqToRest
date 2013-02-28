using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqToRest.OData
{
	public class OrderByQueryPart : ODataQueryPart, IEquatable<OrderByQueryPart>
	{
		public override ODataQueryPartType QueryPartType { get { return ODataQueryPartType.OrderBy; } }

		public ICollection<ODataOrdering> Orderings { get; private set; }

		public OrderByQueryPart(params ODataOrdering[] orderings)
		{
			Orderings = new List<ODataOrdering>(orderings);
		}

		public void AddOrdering(ODataOrdering ordering)
		{
			Orderings.Add(ordering);
		}

		public bool Equals(OrderByQueryPart other)
		{
			var result = false;

			if (ReferenceEquals(null, other))
			{
				result = false;
			}
			else if (ReferenceEquals(this, other))
			{
				result = true;
			}
			else
			{
				result = (other.Orderings.Count == Orderings.Count) && other.Orderings.Zip(Orderings, Equals).Aggregate(true, (previous, current) => previous && current);
			}

			return result;
		}

		public override bool Equals(object obj)
		{
			var result = false;

			if (ReferenceEquals(null, obj))
			{
				result = false;
			}
			else if (ReferenceEquals(this, obj))
			{
				result = true;
			}
			else if (obj.GetType() != typeof (OrderByQueryPart))
			{
				result = false;
			}
			else
			{
				result = Equals((OrderByQueryPart) obj);
			}

			return result;
		}

		public override int GetHashCode()
		{
			var result = 0;

			if (Orderings.Any())
			{
				var str = String.Format("Orderings:{0};", String.Join(",", Orderings));

				result = str.GetHashCode();
			}

			return result;
		}

		public override string ToString()
		{
			var result = String.Empty;

			if (Orderings.Any())
			{
				var orderingPredicate = String.Join(", ", Orderings.Select(x => x.ToString()));

				result = BuildParameterString(orderingPredicate);
			}

			return result;
		}
	}
}
