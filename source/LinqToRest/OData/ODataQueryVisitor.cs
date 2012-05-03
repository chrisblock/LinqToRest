using System;

namespace LinqToRest.OData
{
	public abstract class ODataQueryVisitor
	{
		protected void Visit(ODataQuery query)
		{
			switch (query.QueryType)
			{
				case ODataQueryPartType.Complete:
					VisitComplete((CompleteODataQuery)query);
					break;
				case ODataQueryPartType.Expand:
					VisitExpand((ODataExpandQueryPart)query);
					break;
				case ODataQueryPartType.Filter:
					VisitFilter((ODataFilterQueryPart)query);
					break;
				case ODataQueryPartType.Format:
					VisitFormat((ODataFormatQueryPart)query);
					break;
				case ODataQueryPartType.OrderBy:
					VisitOrderBy((ODataOrderByQueryPart)query);
					break;
				case ODataQueryPartType.Ordering:
					VisitOrdering((ODataOrdering)query);
					break;
				case ODataQueryPartType.Select:
					VisitSelect((ODataSelectQueryPart)query);
					break;
				case ODataQueryPartType.Skip:
					VisitSkip((ODataSkipQueryPart)query);
					break;
				case ODataQueryPartType.SkipToken:
					VisitSkipToken((ODataSkipTokenQueryPart)query);
					break;
				case ODataQueryPartType.Top:
					VisitTop((ODataTopQueryPart)query);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		protected virtual CompleteODataQuery VisitComplete(CompleteODataQuery complete)
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

		protected virtual ODataExpandQueryPart VisitExpand(ODataExpandQueryPart expand)
		{
			return expand;
		}

		protected virtual ODataFilterQueryPart VisitFilter(ODataFilterQueryPart filter)
		{
			return filter;
		}

		protected virtual ODataFormatQueryPart VisitFormat(ODataFormatQueryPart format)
		{
			return format;
		}

		protected virtual ODataOrderByQueryPart VisitOrderBy(ODataOrderByQueryPart orderBy)
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

		protected virtual ODataSelectQueryPart VisitSelect(ODataSelectQueryPart select)
		{
			return select;
		}

		protected virtual ODataSkipQueryPart VisitSkip(ODataSkipQueryPart skip)
		{
			return skip;
		}

		protected virtual ODataSkipTokenQueryPart VisitSkipToken(ODataSkipTokenQueryPart skipToken)
		{
			return skipToken;
		}

		protected virtual ODataTopQueryPart VisitTop(ODataTopQueryPart top)
		{
			return top;
		}
	}
}
