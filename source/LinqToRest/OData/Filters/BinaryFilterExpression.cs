using System;

namespace LinqToRest.OData.Filters
{
	public class BinaryFilterExpression : FilterExpression
	{
		public override FilterExpressionType ExpressionType { get { return FilterExpressionType.Binary; } }

		public FilterExpressionOperator Operator { get; private set; }

		public FilterExpression Left { get; private set; }

		public FilterExpression Right { get; private set; }

		public BinaryFilterExpression(FilterExpression left, FilterExpressionOperator op, FilterExpression right)
		{
			if (left == null)
			{
				throw new ArgumentNullException("left", "Left operand of a binary expression cannot be null.");
			}

			if (right == null)
			{
				throw new ArgumentNullException("right", "Right operand of a binary expression cannot be null.");
			}

			if (op == FilterExpressionOperator.Unknown)
			{
				throw new ArgumentException("Cannot create binary expression with the 'Unknown' operator.", "op");
			}

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
