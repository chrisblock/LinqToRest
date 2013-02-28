using System;

using LinqToRest.OData.Filters;

namespace LinqToRest.OData
{
	public class FilterQueryPart : ODataQueryPart, IEquatable<FilterQueryPart>
	{
		public override ODataQueryPartType QueryPartType { get { return ODataQueryPartType.Filter; } }

		public FilterExpression FilterExpression { get; private set; }

		public FilterQueryPart(FilterExpression filterExpression)
		{
			if (filterExpression == null)
			{
				throw new ArgumentNullException("filterExpression");
			}

			FilterExpression = filterExpression;
		}

		public bool Equals(FilterQueryPart other)
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
				result = Equals(other.FilterExpression, FilterExpression);
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
			else if (obj.GetType() != typeof (FilterQueryPart))
			{
				result = false;
			}
			else
			{
				result = Equals((FilterQueryPart) obj);
			}

			return result;
		}

		public override int GetHashCode()
		{
			var result = 0;

			if (FilterExpression != null)
			{
				result = FilterExpression.GetHashCode();
			}

			return result;
		}

		public override string ToString()
		{
			var result = BuildParameterString(FilterExpression.ToString());

			return result;
		}
	}
}
