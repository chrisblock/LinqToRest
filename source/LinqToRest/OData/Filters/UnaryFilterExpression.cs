using System;

namespace LinqToRest.OData.Filters
{
	public class UnaryFilterExpression : FilterExpression
	{
		public override FilterExpressionType ExpressionType { get { return FilterExpressionType.Unary; } }

		public FilterExpressionOperator Operator { get; private set; }

		public FilterExpression Operand { get; private set; }

		public UnaryFilterExpression(FilterExpressionOperator op, FilterExpression operand)
		{
			if (op == FilterExpressionOperator.Unknown)
			{
				throw new ArgumentException("Cannot create UnaryFilterExpression with operator type 'Unknown'.");
			}

			if (operand == null)
			{
				throw new ArgumentNullException("operand", "Operand cannot be null.");
			}

			Operator = op;
			Operand = operand;
		}

		public override string ToString()
		{
			return String.Format("({0}({1}))", Operator.GetODataQueryOperatorString(), Operand);
		}
	}
}
