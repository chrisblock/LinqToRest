using System;

namespace LinqToRest.OData
{
	public class CountQueryPart : ODataQueryPart, IEquatable<CountQueryPart>
	{
		public override ODataQueryPartType QueryPartType { get { return ODataQueryPartType.Count; } }

		public bool Equals(CountQueryPart other)
		{
			var result = ReferenceEquals(null, other) == false;

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
			else if (obj.GetType() != typeof (CountQueryPart))
			{
				result = false;
			}
			else
			{
				result = Equals((CountQueryPart) obj);
			}

			return result;
		}

		public override int GetHashCode()
		{
			return 0;
		}

		public override string ToString()
		{
			return QueryPartType.GetUrlParameterName();
		}
	}
}
