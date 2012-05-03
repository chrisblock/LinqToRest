using System;

namespace LinqToRest.OData.Filters
{
	public class ODataQueryBinaryFilterExpression : ODataQueryFilterExpression
	{
		public override ODataQueryFilterExpressionType ExpressionType { get { return ODataQueryFilterExpressionType.Binary; } }

		public ODataQueryFilterExpressionOperator Operator { get; private set; }

		public ODataQueryFilterExpression Left { get; private set; }

		public ODataQueryFilterExpression Right { get; private set; }

		public ODataQueryBinaryFilterExpression(ODataQueryFilterExpression left, ODataQueryFilterExpressionOperator op, ODataQueryFilterExpression right)
		{
			Left = left;
			Operator = op;
			Right = right;
		}

		public override string ToString()
		{
			return String.Format("({0} {1} {2})", Left, Operator.GetODataQueryOperatorString(), Right);
		}
	}
}
