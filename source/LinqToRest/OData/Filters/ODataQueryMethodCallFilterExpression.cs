using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqToRest.OData.Filters
{
	public class ODataQueryMethodCallFilterExpression : ODataQueryFilterExpression
	{
		private readonly ICollection<ODataQueryFilterExpression> _arguments = new List<ODataQueryFilterExpression>();

		public override ODataQueryFilterExpressionType ExpressionType { get { return ODataQueryFilterExpressionType.MethodCall; } }

		public Function Method { get; private set; }

		public IEnumerable<ODataQueryFilterExpression> Arguments { get { return _arguments.ToList(); } }

		public ODataQueryMethodCallFilterExpression(Function method, params ODataQueryFilterExpression[] arguments)
		{
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
