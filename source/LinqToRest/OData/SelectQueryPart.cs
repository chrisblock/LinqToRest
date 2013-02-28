using System;
using System.Collections.Generic;
using System.Linq;

using LinqToRest.OData.Filters;

namespace LinqToRest.OData
{
	public class SelectQueryPart : ODataQueryPart, IEquatable<SelectQueryPart>
	{
		public override ODataQueryPartType QueryPartType { get { return ODataQueryPartType.Select; } }

		public ICollection<MemberAccessFilterExpression> Selectors { get; private set; }

		public SelectQueryPart(params MemberAccessFilterExpression[] selectors)
		{
			Selectors = selectors;
		}

		public bool Equals(SelectQueryPart other)
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
				result = Equals(other.Selectors, Selectors);
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
			else if (obj.GetType() != typeof (SelectQueryPart))
			{
				result = false;
			}
			else
			{
				result = Equals((SelectQueryPart) obj);
			}

			return result;
		}

		public override int GetHashCode()
		{
			return (Selectors != null ? Selectors.GetHashCode() : 0);
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
