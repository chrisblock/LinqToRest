using System;

namespace LinqToRest.OData.Filters
{
	public class BinaryFilterExpression : FilterExpression, IEquatable<BinaryFilterExpression>
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

		public bool Equals(BinaryFilterExpression other)
		{
			var result = false;

			if (ReferenceEquals(null, other))
			{
				result = false;
			}
			else if (ReferenceEquals(this, other))
			{
				result = true;
			}
			else
			{
				result = Equals(other.Operator, Operator) && Equals(other.Left, Left) && Equals(other.Right, Right);
			}

			return result;
		}

		public override bool Equals(object obj)
		{
			var result = false;

			if (ReferenceEquals(null, obj))
			{
				result = false;
			}
			else if (ReferenceEquals(this, obj))
			{
				result = true;
			}
			else if (obj.GetType() != typeof (BinaryFilterExpression))
			{
				result = false;
			}
			else
			{
				result = Equals((BinaryFilterExpression) obj);
			}

			return result;
		}

		public override int GetHashCode()
		{
			var result = String.Format("Left:{0};Operator:{1};Right:{2};", Left, Operator, Right);

			return result.GetHashCode();
		}

		public override string ToString()
		{
			return String.Format("({0} {1} {2})", Left, Operator.GetODataQueryOperatorString(), Right);
		}
	}
}
