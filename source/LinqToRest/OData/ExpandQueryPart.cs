using System;
using System.Collections.Generic;
using System.Linq;

using LinqToRest.OData.Filters;

namespace LinqToRest.OData
{
	public class ExpandQueryPart : ODataQueryPart, IEquatable<ExpandQueryPart>
	{
		public override ODataQueryPartType QueryPartType { get { return ODataQueryPartType.Expand; } }

		public ICollection<MemberAccessFilterExpression> Members { get; private set; }

		internal ExpandQueryPart(params MemberAccessFilterExpression[] members)
		{
			Members = members;
		}

		public bool Equals(ExpandQueryPart other)
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
				if (Members.Count == other.Members.Count)
				{
					result = Members
						.Zip(other.Members, Equals)
						.Aggregate(true, (previous, current) => previous && current);
				}
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
			else if (obj.GetType() != typeof (ExpandQueryPart))
			{
				result = false;
			}
			else
			{
				result = Equals((ExpandQueryPart) obj);
			}

			return result;
		}

		public override int GetHashCode()
		{
			var result = 0;

			if (Members.Any())
			{
				var str = String.Format("Members:{0};", String.Join(",", Members));

				result = str.GetHashCode();
			}

			return result.GetHashCode();
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
