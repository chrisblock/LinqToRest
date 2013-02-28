using System;

namespace LinqToRest.OData
{
	public class TopQueryPart : ODataQueryPart, IEquatable<TopQueryPart>
	{
		public override ODataQueryPartType QueryPartType { get { return ODataQueryPartType.Top; } }

		public int? NumberToTake { get; private set; }

		public TopQueryPart() : this(null)
		{
		}

		public TopQueryPart(int? numberToTake)
		{
			NumberToTake = numberToTake;
		}

		public bool Equals(TopQueryPart other)
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
				result = other.NumberToTake.Equals(NumberToTake);
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
			else if (obj.GetType() != typeof (TopQueryPart))
			{
				result = false;
			}
			else
			{
				result = Equals((TopQueryPart) obj);
			}

			return result;
		}

		public override int GetHashCode()
		{
			var result = 0;

			if (NumberToTake.HasValue)
			{
				result = NumberToTake.Value;
			}

			return result;
		}

		public override string ToString()
		{
			var result = String.Empty;

			if (NumberToTake.HasValue)
			{
				result = BuildParameterString(NumberToTake.Value);
			}

			return result;
		}
	}
}
