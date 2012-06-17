using System;
using System.Collections.Generic;
using System.Linq;

using LinqToRest.OData.Filters;

namespace LinqToRest.OData
{
	public class ExpandQueryPart : ODataQueryPart
	{
		public override ODataQueryPartType QueryPartType { get { return ODataQueryPartType.Expand; } }

		public ICollection<MemberAccessFilterExpression> Members { get; private set; }

		internal ExpandQueryPart(params MemberAccessFilterExpression[] members)
		{
			Members = members;
		}

		public override string ToString()
		{
			var result = String.Empty;

			if (Members.Any())
			{
				result = BuildParameterString(String.Join(", ", Members));
			}

			return result;
		}
	}
}
