using System;

namespace LinqToRest.OData.Filters
{
	public class ODataQueryUnaryFilterExpression : ODataQueryFilterExpression
	{
		public override ODataQueryFilterExpressionType ExpressionType { get { return ODataQueryFilterExpressionType.Unary; } }

		public ODataQueryFilterExpressionOperator Operator { get; private set; }

		public ODataQueryFilterExpression Operand { get; private set; }

		public ODataQueryUnaryFilterExpression(ODataQueryFilterExpressionOperator op, ODataQueryFilterExpression operand)
		{
			Operator = op;
			Operand = operand;
		}

		public override string ToString()
		{
			return String.Format("({0}({1}))", Operator.GetODataQueryOperatorString(), Operand);
		}
	}
}
