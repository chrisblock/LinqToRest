using System;

namespace LinqToRest.OData
{
	public abstract class ODataQueryVisitor
	{
		protected void Visit(ODataQuery query)
		{
			VisitODataQuery(query);
		}

		protected void Visit(ODataQueryPart query)
		{
			switch (query.QueryPartType)
			{
				case ODataQueryPartType.Expand:
					VisitExpand((ExpandQueryPart) query);
					break;
				case ODataQueryPartType.Filter:
					VisitFilter((FilterQueryPart) query);
					break;
				case ODataQueryPartType.Format:
					VisitFormat((FormatQueryPart) query);
					break;
				case ODataQueryPartType.OrderBy:
					VisitOrderBy((OrderByQueryPart) query);
					break;
				case ODataQueryPartType.Ordering:
					VisitOrdering((ODataOrdering) query);
					break;
				case ODataQueryPartType.Select:
					VisitSelect((SelectQueryPart) query);
					break;
				case ODataQueryPartType.Skip:
					VisitSkip((SkipQueryPart) query);
					break;
				case ODataQueryPartType.SkipToken:
					VisitSkipToken((SkipTokenQueryPart) query);
					break;
				case ODataQueryPartType.Top:
					VisitTop((TopQueryPart) query);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		protected virtual ODataQuery VisitODataQuery(ODataQuery query)
		{
			var result = new ODataQuery();

			if (query.FilterPredicate != null)
			{
				result.FilterPredicate = VisitFilter(query.FilterPredicate);
			}

			if (query.OrderByPredicate != null)
			{
				result.OrderByPredicate = VisitOrderBy(query.OrderByPredicate);
			}

			if (query.CountPredicate != null)
			{
				result.CountPredicate = VisitCount(query.CountPredicate);
			}
			else
			{
				if (query.SkipPredicate != null)
				{
					result.SkipPredicate = VisitSkip(query.SkipPredicate);
				}

				if (query.TopPredicate != null)
				{
					result.TopPredicate = VisitTop(query.TopPredicate);
				}

				if (query.SelectPredicate != null)
				{
					result.SelectPredicate = VisitSelect(query.SelectPredicate);
				}
			}

			return result;
		}

		protected virtual CountQueryPart VisitCount(CountQueryPart count)
		{
			return count;
		}

		protected virtual ExpandQueryPart VisitExpand(ExpandQueryPart expand)
		{
			return expand;
		}

		protected virtual FilterQueryPart VisitFilter(FilterQueryPart filter)
		{
			return filter;
		}

		protected virtual FormatQueryPart VisitFormat(FormatQueryPart format)
		{
			return format;
		}

		protected virtual OrderByQueryPart VisitOrderBy(OrderByQueryPart orderBy)
		{
			foreach (var ordering in orderBy.Orderings)
			{
				Visit(ordering);
			}

			return orderBy;
		}

		protected virtual ODataOrdering VisitOrdering(ODataOrdering ordering)
		{
			return ordering;
		}

		protected virtual SelectQueryPart VisitSelect(SelectQueryPart select)
		{
			return select;
		}

		protected virtual SkipQueryPart VisitSkip(SkipQueryPart skip)
		{
			return skip;
		}

		protected virtual SkipTokenQueryPart VisitSkipToken(SkipTokenQueryPart skipToken)
		{
			return skipToken;
		}

		protected virtual TopQueryPart VisitTop(TopQueryPart top)
		{
			return top;
		}
	}
}
