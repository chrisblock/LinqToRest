using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using LinqToRest.Client.OData;
using LinqToRest.Client.OData.Impl;
using LinqToRest.OData;
using LinqToRest.OData.Filters;
using LinqToRest.OData.Formatting.Impl;
using LinqToRest.OData.Parsing.Impl;

using Remotion.Linq;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.ResultOperators;

namespace LinqToRest.Client.Linq
{
	public class RestQueryModelVisitor : QueryModelVisitorBase, IQueryModelTranslator
	{
		private readonly ODataQuery _query;
		private readonly IFilterExpressionTranslator _filterExpressionTranslator;

		public RestQueryModelVisitor(IODataQueryFactory queryFactory)
		{
			_query = queryFactory.Create();
			_filterExpressionTranslator = new ODataFilterExpressionVisitor(new FilterExpressionParserStrategy(), new TypeFormatter());
		}

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
				_query.OrderByPredicate = ODataQueryPart.OrderBy();
			}

			if (ordering.Expression.NodeType == ExpressionType.MemberAccess)
			{
				var memberExpression = (MemberExpression)ordering.Expression;

				var direction = (ordering.OrderingDirection == OrderingDirection.Asc)
					? ODataOrderingDirection.Asc
					: ODataOrderingDirection.Desc;

				var o = ODataQueryPart.Ordering(memberExpression.Member.Name, direction);

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

			if (resultOperator is CountResultOperator)
			{
				_query.CountPredicate = ODataQueryPart.Count();
			}
			else if (resultOperator is LongCountResultOperator)
			{
				_query.CountPredicate = ODataQueryPart.Count();
			}
			else if (resultOperator is SkipResultOperator)
			{
				var skipOperator = (SkipResultOperator) resultOperator;

				var skipCount = skipOperator.GetConstantCount();

				_query.SkipPredicate = ODataQueryPart.Skip(skipCount);
			}
			else if (resultOperator is TakeResultOperator)
			{
				var takeOperator = (TakeResultOperator) resultOperator;

				var takeCount = takeOperator.GetConstantCount();

				_query.TopPredicate = ODataQueryPart.Top(takeCount);
			}

			base.VisitResultOperator(resultOperator, queryModel, index);
		}

		public override void VisitSelectClause(SelectClause selectClause, QueryModel queryModel)
		{
			var selectors = new List<string>();

			if (selectClause.Selector.NodeType == ExpressionType.MemberAccess)
			{
				// TODO: handle access of child properties in selector statements

				var selector = (MemberExpression)selectClause.Selector;

				selectors.Add(selector.Member.Name);
			}
			else if (selectClause.Selector.NodeType == ExpressionType.New)
			{
				// TODO: trying to support this may not be viable...
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

							if (memberBinding.Member.Name == selectedMember.Member.Name)
							{
								selectors.Add(selectedMember.Member.Name);
							}
							else
							{
								throw new NotSupportedException(String.Format("Cannot alias member '{0}' as '{1}'. Aliasing projections is not supported in LinqToRest.", selectedMember.Member.Name, memberBinding.Member.Name));
							}
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
				_query.SelectPredicate = ODataQueryPart.Select(selectors.Distinct().Select(FilterExpression.MemberAccess).ToArray());
			}

			base.VisitSelectClause(selectClause, queryModel);
		}

		public override void VisitWhereClause(WhereClause whereClause, QueryModel queryModel, int index)
		{
			// the predicate here is not a lambda; it is just the body of the Where() lambda
			var oDataFilterExpression = _filterExpressionTranslator.Translate(whereClause.Predicate);

			_query.FilterPredicate = ODataQueryPart.Filter(oDataFilterExpression);

			base.VisitWhereClause(whereClause, queryModel, index);
		}
	}
}
