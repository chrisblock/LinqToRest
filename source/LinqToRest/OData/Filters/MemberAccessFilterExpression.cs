using System;

namespace LinqToRest.OData.Filters
{
	public class MemberAccessFilterExpression : FilterExpression
	{
		public override FilterExpressionType ExpressionType { get { return FilterExpressionType.MemberAccess; } }

		public FilterExpression Instance { get; private set; }

		public string Member { get; private set; }

		public MemberAccessFilterExpression(FilterExpression instance, string member)
		{
			Instance = instance;
			Member = member;
		}

		public override string ToString()
		{
			var result = Member;

			if (Instance != null)
			{
				result = String.Format("{0}/{1}", Instance, Member);
			}
			return result;
		}
	}
}
