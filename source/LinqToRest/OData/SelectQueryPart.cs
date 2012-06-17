using System;
using System.Collections.Generic;
using System.Linq;

using LinqToRest.OData.Filters;

namespace LinqToRest.OData
{
	public class SelectQueryPart : ODataQueryPart
	{
		public override ODataQueryPartType QueryPartType { get { return ODataQueryPartType.Select; } }

		public ICollection<MemberAccessFilterExpression> Selectors { get; private set; }

		public SelectQueryPart(params MemberAccessFilterExpression[] selectors)
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
