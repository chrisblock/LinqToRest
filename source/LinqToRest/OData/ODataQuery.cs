using System;
using System.Collections.Generic;

namespace LinqToRest.OData
{
	public class ODataQuery : IEquatable<ODataQuery>
	{
		public Uri Uri { get; set; }

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
			var result = false;

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
				result = Equals(other.TopPredicate, TopPredicate) && Equals(other.SkipTokenPredicate, SkipTokenPredicate) && Equals(other.SkipPredicate, SkipPredicate) && Equals(other.OrderByPredicate, OrderByPredicate) && Equals(other.SelectPredicate, SelectPredicate) && Equals(other.InlineCountPredicate, InlineCountPredicate) && Equals(other.FormatPredicate, FormatPredicate) && Equals(other.FilterPredicate, FilterPredicate) && Equals(other.ExpandPredicate, ExpandPredicate) && Equals(other.CountPredicate, CountPredicate) && Equals(other.Uri, Uri);
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
			var result = String.Format("Uri:{0};CountPredicate:{1};ExpandPredicate:{2};FilterPredicate:{3};FormatPredicate{4};InlineCountPredicate:{5};OrderByPredicate:{6};SelectPredicate:{7};SkipPredicate:{8};SkipTokenPredicate:{9};TopPredicate:{10};"
				, Uri
				, CountPredicate
				, ExpandPredicate
				, FilterPredicate
				, FormatPredicate
				, InlineCountPredicate
				, OrderByPredicate
				, SelectPredicate
				, SkipPredicate
				, SkipTokenPredicate
				, TopPredicate);

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

			return String.Format("{0}?{1}", Uri, String.Join("&", oDataQueryParts));
		}
	}
}
