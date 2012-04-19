using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

using LinqToRest.OpenData;

using Remotion.Linq;
using Remotion.Linq.Clauses;

namespace LinqToRest
{
	internal class RestQueryModelVisitor : QueryModelVisitorBase
	{
		private ODataQuery _query;

		public string Translate(QueryModel queryModel)
		{
			base.VisitQueryModel(queryModel);

			return _query.ToString();
		}

		public override void VisitAdditionalFromClause(AdditionalFromClause fromClause, QueryModel queryModel, int index)
		{
			throw new NotSupportedException("Cannot select from multiple sources.");
			//base.VisitAdditionalFromClause(fromClause, queryModel, index);
		}

		public override void VisitGroupJoinClause(GroupJoinClause groupJoinClause, QueryModel queryModel, int index)
		{
			throw new NotSupportedException("Cannot select from multiple sources.");
			//base.VisitGroupJoinClause(groupJoinClause, queryModel, index);
		}

		public override void VisitJoinClause(JoinClause joinClause, QueryModel queryModel, int index)
		{
			throw new NotSupportedException("Cannot select from multiple sources.");
			//base.VisitJoinClause(joinClause, queryModel, index);
		}

		public override void VisitJoinClause(JoinClause joinClause, QueryModel queryModel, GroupJoinClause groupJoinClause)
		{
			throw new NotSupportedException("Cannot select from multiple sources.");
			//base.VisitJoinClause(joinClause, queryModel, groupJoinClause);
		}

		public override void VisitMainFromClause(MainFromClause fromClause, QueryModel queryModel)
		{
			var queryType = typeof (ODataQuery<>).MakeGenericType(fromClause.ItemType);
			_query = (ODataQuery)Activator.CreateInstance(queryType);

			base.VisitMainFromClause(fromClause, queryModel);
		}

		public override void VisitOrderByClause(OrderByClause orderByClause, QueryModel queryModel, int index)
		{
			var orderings = new List<string>();

			foreach (var ordering in orderByClause.Orderings)
			{
				if (ordering.Expression.NodeType == ExpressionType.MemberAccess)
				{
					var memberExpression = (MemberExpression) ordering.Expression;

					orderings.Add(String.Format("{0} {1}", memberExpression.Member.Name, ordering.OrderingDirection.ToString().ToLowerInvariant()));
				}
			}

			_query.OrderByPredicate = String.Join(", ", orderings);

			base.VisitOrderByClause(orderByClause, queryModel, index);
		}

		public override void VisitOrdering(Ordering ordering, QueryModel queryModel, OrderByClause orderByClause, int index)
		{
			base.VisitOrdering(ordering, queryModel, orderByClause, index);
		}

		public override void VisitResultOperator(ResultOperatorBase resultOperator, QueryModel queryModel, int index)
		{
			base.VisitResultOperator(resultOperator, queryModel, index);
		}

		public override void VisitSelectClause(SelectClause selectClause, QueryModel queryModel)
		{
			base.VisitSelectClause(selectClause, queryModel);
		}

		public override void VisitWhereClause(WhereClause whereClause, QueryModel queryModel, int index)
		{
			base.VisitWhereClause(whereClause, queryModel, index);
		}
	}
}
