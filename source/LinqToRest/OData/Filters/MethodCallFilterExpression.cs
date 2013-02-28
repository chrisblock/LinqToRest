using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqToRest.OData.Filters
{
	public class MethodCallFilterExpression : FilterExpression, IEquatable<MethodCallFilterExpression>
	{
		private readonly ICollection<FilterExpression> _arguments;

		public override FilterExpressionType ExpressionType { get { return FilterExpressionType.MethodCall; } }

		public Function Method { get; private set; }

		public IEnumerable<FilterExpression> Arguments { get { return _arguments.ToList(); } }

		public MethodCallFilterExpression(Function method, params FilterExpression[] arguments)
		{
			if (method == Function.Unknown)
			{
				throw new ArgumentException("Cannot create MethodCallFilterExpression for Unknown method.");
			}

			if ((arguments == null) || (arguments.Any() == false))
			{
				throw new ArgumentException("Cannot create MethodCallFilterExpression with no arguments.");
			}

			Method = method;
			_arguments = arguments;
		}

		public bool Equals(MethodCallFilterExpression other)
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
				result = Equals(other.Method, Method) && (other._arguments.Count == _arguments.Count) && other._arguments.Zip(_arguments, Equals).Aggregate(true, (previous, current) => previous && current);
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
			else if (obj.GetType() != typeof (MethodCallFilterExpression))
			{
				result = false;
			}
			else
			{
				result = Equals((MethodCallFilterExpression) obj);;
			}

			return result;
		}

		public override int GetHashCode()
		{
			var result = String.Format("Method:{0};Arguments:{1}", Method, String.Join(",", Arguments));

			return result.GetHashCode();
		}

		public override string ToString()
		{
			var result = String.Format("{0}({1})", Method.GetODataQueryMethodName(), String.Join(", ", _arguments));

			return result;
		}
	}
}
