using System;
using System.Collections.Generic;

namespace LinqToRest.OData
{
	public class ODataQuery : IEquatable<ODataQuery>
	{
		public CountQueryPart CountPredicate { get; set; }

		public ExpandQueryPart ExpandPredicate { get; set; }
		
		public FilterQueryPart FilterPredicate { get; set; }
		
		public FormatQueryPart FormatPredicate { get; set; }

		public InlineCountQueryPart InlineCountPredicate { get; set; }

		public OrderByQueryPart OrderByPredicate { get; set; }

		public SelectQueryPart SelectPredicate { get; set; }

		public SkipQueryPart SkipPredicate { get; set; }

		// TODO: this may have been removed from the standard...
		public SkipTokenQueryPart SkipTokenPredicate { get; set; }

		public TopQueryPart TopPredicate { get; set; }

		public bool Equals(ODataQuery other)
		{
			bool result;

			if (ReferenceEquals(null, other))
			{
				result = false;
			}
			else if (ReferenceEquals(this, other))
			{
				return true;
			}
			else
			{
				result = Equals(other.TopPredicate, TopPredicate)
					&& Equals(other.SkipTokenPredicate, SkipTokenPredicate)
					&& Equals(other.SkipPredicate, SkipPredicate)
					&& Equals(other.OrderByPredicate, OrderByPredicate)
					&& Equals(other.SelectPredicate, SelectPredicate)
					&& Equals(other.InlineCountPredicate, InlineCountPredicate)
					&& Equals(other.FormatPredicate, FormatPredicate)
					&& Equals(other.FilterPredicate, FilterPredicate)
					&& Equals(other.ExpandPredicate, ExpandPredicate)
					&& Equals(other.CountPredicate, CountPredicate);
			}

			return result;
		}

		public override bool Equals(object obj)
		{
			bool result;

			if (ReferenceEquals(null, obj))
			{
				result = false;
			}
			else if (ReferenceEquals(this, obj))
			{
				result = true;
			}
			else if (obj.GetType() != typeof (ODataQuery))
			{
				result = false;
			}
			else
			{
				result = Equals((ODataQuery)obj);
			}

			return result;
		}

		public override int GetHashCode()
		{
			var result = $"CountPredicate:{CountPredicate};ExpandPredicate:{ExpandPredicate};FilterPredicate:{FilterPredicate};FormatPredicate{FormatPredicate};InlineCountPredicate:{InlineCountPredicate};OrderByPredicate:{OrderByPredicate};SelectPredicate:{SelectPredicate};SkipPredicate:{SkipPredicate};SkipTokenPredicate:{SkipTokenPredicate};TopPredicate:{TopPredicate};";

			return result.GetHashCode();
		}

		public override string ToString()
		{
			var oDataQueryParts = new List<string>(10)
			{
				FormatPredicate.ToString()
			};

			if (CountPredicate != null)
			{
				oDataQueryParts.Add(CountPredicate.ToString());
			}

			if (ExpandPredicate != null)
			{
				oDataQueryParts.Add(ExpandPredicate.ToString());
			}

			if (FilterPredicate != null)
			{
				oDataQueryParts.Add(FilterPredicate.ToString());
			}

			if (OrderByPredicate != null)
			{
				oDataQueryParts.Add(OrderByPredicate.ToString());
			}

			if (SelectPredicate != null)
			{
				oDataQueryParts.Add(SelectPredicate.ToString());
			}

			if (SkipPredicate != null)
			{
				oDataQueryParts.Add(SkipPredicate.ToString());
			}

			if (SkipTokenPredicate != null)
			{
				oDataQueryParts.Add(SkipTokenPredicate.ToString());
			}

			if (TopPredicate != null)
			{
				oDataQueryParts.Add(TopPredicate.ToString());
			}

			if (InlineCountPredicate != null)
			{
				oDataQueryParts.Add(InlineCountPredicate.ToString());
			}

			return $"?{String.Join("&", oDataQueryParts)}";
		}
	}
}
