using System;

namespace LinqToRest.OData
{
	public abstract class ODataQueryVisitor
	{
		protected void Visit(ODataQuery query)
		{
			VisitComplete(query);
		}

		protected void Visit(ODataQueryPart query)
		{
			switch (query.QueryPartType)
			{
				case ODataQueryPartType.Expand:
					VisitExpand((ExpandQueryPart)query);
					break;
				case ODataQueryPartType.Filter:
					VisitFilter((FilterQueryPart)query);
					break;
				case ODataQueryPartType.Format:
					VisitFormat((FormatQueryPart)query);
					break;
				case ODataQueryPartType.OrderBy:
					VisitOrderBy((OrderByQueryPart)query);
					break;
				case ODataQueryPartType.Ordering:
					VisitOrdering((ODataOrdering)query);
					break;
				case ODataQueryPartType.Select:
					VisitSelect((SelectQueryPart)query);
					break;
				case ODataQueryPartType.Skip:
					VisitSkip((SkipQueryPart)query);
					break;
				case ODataQueryPartType.SkipToken:
					VisitSkipToken((SkipTokenQueryPart)query);
					break;
				case ODataQueryPartType.Top:
					VisitTop((TopQueryPart)query);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		protected virtual ODataQuery VisitComplete(ODataQuery complete)
		{
			if (complete.FilterPredicate != null)
			{
				complete.FilterPredicate = VisitFilter(complete.FilterPredicate);
			}

			if (complete.OrderByPredicate != null)
			{
				complete.OrderByPredicate = VisitOrderBy(complete.OrderByPredicate);
			}

			if (complete.SkipPredicate != null)
			{
				complete.SkipPredicate = VisitSkip(complete.SkipPredicate);
			}

			if (complete.TopPredicate != null)
			{
				complete.TopPredicate = VisitTop(complete.TopPredicate);
			}

			if (complete.SelectPredicate != null)
			{
				complete.SelectPredicate = VisitSelect(complete.SelectPredicate);
			}

			return complete;
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
