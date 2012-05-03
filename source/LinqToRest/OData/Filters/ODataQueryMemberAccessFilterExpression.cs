using System;

namespace LinqToRest.OData.Filters
{
	public class ODataQueryMemberAccessFilterExpression : ODataQueryFilterExpression
	{
		public override ODataQueryFilterExpressionType ExpressionType { get { return ODataQueryFilterExpressionType.MemberAccess; } }

		public ODataQueryFilterExpression Instance { get; private set; }

		public string Member { get; private set; }

		public ODataQueryMemberAccessFilterExpression(ODataQueryFilterExpression instance, string member)
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
