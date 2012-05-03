using System;
using System.Collections.Generic;
using System.Linq;

using LinqToRest.OData.Filters;

namespace LinqToRest.OData
{
	public class ODataSelectQueryPart : ODataQuery
	{
		public override ODataQueryPartType QueryType { get { return ODataQueryPartType.Select; } }

		public IEnumerable<ODataQueryMemberAccessFilterExpression> Selectors { get; private set; }

		public ODataSelectQueryPart(params ODataQueryMemberAccessFilterExpression[] selectors)
		{
			Selectors = selectors;
		}

		public override string ToString()
		{
			var result = String.Empty;

			if (Selectors.Any())
			{
				result = BuildParameterString(String.Join(", ", Selectors));
			}

			return result;
		}
	}
}
