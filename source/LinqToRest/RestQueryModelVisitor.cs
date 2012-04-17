using System.Text;

using Remotion.Linq;
using Remotion.Linq.Clauses;

namespace LinqToRest
{
	internal class RestQueryModelVisitor : QueryModelVisitorBase
	{
		private readonly StringBuilder _queryStringBuilder = new StringBuilder();

		public string Translate(QueryModel queryModel)
		{
			base.VisitQueryModel(queryModel);

			return _queryStringBuilder.ToString();
		}

		public override void VisitAdditionalFromClause(AdditionalFromClause fromClause, QueryModel queryModel, int index)
		{
			base.VisitAdditionalFromClause(fromClause, queryModel, index);
		}

		public override void VisitGroupJoinClause(GroupJoinClause groupJoinClause, QueryModel queryModel, int index)
		{
			base.VisitGroupJoinClause(groupJoinClause, queryModel, index);
		}

		public override void VisitJoinClause(JoinClause joinClause, QueryModel queryModel, int index)
		{
			base.VisitJoinClause(joinClause, queryModel, index);
		}

		public override void VisitJoinClause(JoinClause joinClause, QueryModel queryModel, GroupJoinClause groupJoinClause)
		{
			base.VisitJoinClause(joinClause, queryModel, groupJoinClause);
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
