using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using LinqToRest.OData;

using Remotion.Linq;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.ResultOperators;

namespace LinqToRest
{
	internal class RestQueryModelVisitor : QueryModelVisitorBase
	{
		private readonly ODataQuery _query = new ODataQuery();

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
			var url = fromClause.ItemType.GetCustomAttributes<ServiceUrlAttribute>().Single().Url;

			_query.Url = url;

			base.VisitMainFromClause(fromClause, queryModel);
		}

		public override void VisitOrderByClause(OrderByClause orderByClause, QueryModel queryModel, int index)
		{
			var orderings = new List<string>();

			// TODO: handle this in each ordering???
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
			// Remotion.Linq.Clauses.ResultOperators.AggregateFromSeedResultOperator
			// Remotion.Linq.Clauses.ResultOperators.AggregateResultOperator
			// Remotion.Linq.Clauses.ResultOperators.AllResultOperator
			// Remotion.Linq.Clauses.ResultOperators.AnyResultOperator
			// Remotion.Linq.Clauses.ResultOperators.AverageResultOperator
			// Remotion.Linq.Clauses.ResultOperators.CastResultOperator
			// Remotion.Linq.Clauses.ResultOperators.ChoiceResultOperatorBase
			// Remotion.Linq.Clauses.ResultOperators.ContainsResultOperator
			// Remotion.Linq.Clauses.ResultOperators.CountResultOperator
			// Remotion.Linq.Clauses.ResultOperators.DefaultIfEmptyResultOperator
			// Remotion.Linq.Clauses.ResultOperators.DistinctResultOperator
			// Remotion.Linq.Clauses.ResultOperators.ExceptResultOperator
			// Remotion.Linq.Clauses.ResultOperators.FirstResultOperator
			// Remotion.Linq.Clauses.ResultOperators.GroupResultOperator
			// Remotion.Linq.Clauses.ResultOperators.IntersectResultOperator
			// Remotion.Linq.Clauses.ResultOperators.LastResultOperator
			// Remotion.Linq.Clauses.ResultOperators.LongCountResultOperator
			// Remotion.Linq.Clauses.ResultOperators.MaxResultOperator
			// Remotion.Linq.Clauses.ResultOperators.MinResultOperator
			// Remotion.Linq.Clauses.ResultOperators.OfTypeResultOperator
			// Remotion.Linq.Clauses.ResultOperators.ReverseResultOperator
			// Remotion.Linq.Clauses.ResultOperators.SequenceFromSequenceResultOperatorBase
			// Remotion.Linq.Clauses.ResultOperators.SequenceTypePreservingResultOperatorBase
			// Remotion.Linq.Clauses.ResultOperators.SingleResultOperator
			// Remotion.Linq.Clauses.ResultOperators.SkipResultOperator
			// Remotion.Linq.Clauses.ResultOperators.SumResultOperator
			// Remotion.Linq.Clauses.ResultOperators.TakeResultOperator
			// Remotion.Linq.Clauses.ResultOperators.UnionResultOperator
			// Remotion.Linq.Clauses.ResultOperators.ValueFromSequenceResultOperatorBase

			if (resultOperator is SkipResultOperator)
			{
				var skipOperator = (SkipResultOperator) resultOperator;

				var skipCount = skipOperator.GetConstantCount();

				_query.Skip = skipCount;
			}
			else if (resultOperator is TakeResultOperator)
			{
				var takeOperator = (TakeResultOperator) resultOperator;

				var takeCount = takeOperator.GetConstantCount();

				_query.Top = takeCount;
			}

			base.VisitResultOperator(resultOperator, queryModel, index);
		}

		public override void VisitSelectClause(SelectClause selectClause, QueryModel queryModel)
		{
			var selectors = new List<string>();

			if (selectClause.Selector.NodeType == ExpressionType.MemberAccess)
			{
				var selector = (MemberExpression)selectClause.Selector;

				selectors.Add(selector.Member.Name);
			}
			else if (selectClause.Selector.NodeType == ExpressionType.New)
			{
				var constructor = (NewExpression) selectClause.Selector;

				foreach (var argument in constructor.Arguments)
				{
					if (argument.NodeType == ExpressionType.MemberAccess)
					{
						var member = (MemberExpression) argument;

						selectors.Add(member.Member.Name);
					}
				}
			}

			_query.SelectPredicate = String.Join(", ", selectors);

			base.VisitSelectClause(selectClause, queryModel);
		}

		public override void VisitWhereClause(WhereClause whereClause, QueryModel queryModel, int index)
		{
			// the predicate here is not a lambda; it is just the body of the Where() lambda
			var oDataFilterExpression = new ODataExpressionVisitor().Translate(whereClause.Predicate);

			_query.FilterPredicate = oDataFilterExpression;

			base.VisitWhereClause(whereClause, queryModel, index);
		}
	}
}
