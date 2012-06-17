using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqToRest.OData.Filters
{
	public class MethodCallFilterExpression : FilterExpression
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

		public override string ToString()
		{
			var result = String.Format("{0}({1})", Method.GetODataQueryMethodName(), String.Join(", ", _arguments));

			return result;
		}
	}
}
