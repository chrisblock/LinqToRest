using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using LinqToRest.OData;
using LinqToRest.OData.Building;
using LinqToRest.OData.Filters;

using Remotion.Linq;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.ResultOperators;

namespace LinqToRest.Linq
{
	public class RestQueryModelVisitor : QueryModelVisitorBase
	{
		private readonly CompleteODataQuery _query = new CompleteODataQuery();

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
			var uri = fromClause.ItemType.GetServiceUri();

			_query.Uri = uri;

			base.VisitMainFromClause(fromClause, queryModel);
		}

		public override void VisitOrderByClause(OrderByClause orderByClause, QueryModel queryModel, int index)
		{
			base.VisitOrderByClause(orderByClause, queryModel, index);
		}

		public override void VisitOrdering(Ordering ordering, QueryModel queryModel, OrderByClause orderByClause, int index)
		{
			if (_query.OrderByPredicate == null)
			{
				_query.OrderByPredicate = ODataQuery.OrderBy();
			}

			if (ordering.Expression.NodeType == ExpressionType.MemberAccess)
			{
				var memberExpression = (MemberExpression)ordering.Expression;

				var direction = (ordering.OrderingDirection == OrderingDirection.Asc)
					? ODataOrderingDirection.Asc
					: ODataOrderingDirection.Desc;

				var o = ODataQuery.Ordering(memberExpression.Member.Name, direction);

				_query.OrderByPredicate.AddOrdering(o);
			}

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

				_query.SkipPredicate = ODataQuery.Skip(skipCount);
			}
			else if (resultOperator is TakeResultOperator)
			{
				var takeOperator = (TakeResultOperator) resultOperator;

				var takeCount = takeOperator.GetConstantCount();

				_query.TopPredicate = ODataQuery.Top(takeCount);
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
					else
					{
						throw new NotSupportedException("Cannot select non-members from the result set.");
					}
				}
			}
			else if (selectClause.Selector.NodeType == ExpressionType.MemberInit)
			{
				var memberInit = (MemberInitExpression) selectClause.Selector;

				var constructor = memberInit.NewExpression;

				foreach (var argument in constructor.Arguments)
				{
					if (argument.NodeType == ExpressionType.MemberAccess)
					{
						var member = (MemberExpression) argument;

						selectors.Add(member.Member.Name);
					}
					else
					{
						throw new NotSupportedException("Cannot select non-members from the result set.");
					}
				}

				foreach (var memberBinding in memberInit.Bindings)
				{
					if (memberBinding.BindingType == MemberBindingType.Assignment)
					{
						var assignment = (MemberAssignment)memberBinding;

						if (assignment.Expression.NodeType == ExpressionType.MemberAccess)
						{
							var selectedMember = (MemberExpression)assignment.Expression;
							selectors.Add(selectedMember.Member.Name);
						}
						else
						{
							throw new NotSupportedException("Cannot select non-members into the result set.");
						}
					}
					else
					{
						throw new NotSupportedException("Cannot select lists from the result set.");
					}
				}
			}

			if (selectors.Any())
			{
				_query.SelectPredicate = ODataQuery.Select(selectors.Distinct().Select(ODataQueryFilterExpression.MemberAccess).ToArray());
			}

			base.VisitSelectClause(selectClause, queryModel);
		}

		public override void VisitWhereClause(WhereClause whereClause, QueryModel queryModel, int index)
		{
			// the predicate here is not a lambda; it is just the body of the Where() lambda
			var oDataFilterExpression = new ODataFilterExpressionVisitor().Translate(whereClause.Predicate);

			_query.FilterPredicate = ODataQuery.Filter(oDataFilterExpression);

			base.VisitWhereClause(whereClause, queryModel, index);
		}
	}
}
