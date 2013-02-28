using System;

namespace LinqToRest.OData
{
	public class SkipQueryPart : ODataQueryPart, IEquatable<SkipQueryPart>
	{
		public override ODataQueryPartType QueryPartType { get { return ODataQueryPartType.Skip; } }

		public int? NumberToSkip { get; private set; }

		public SkipQueryPart() : this(null)
		{
		}

		public SkipQueryPart(int? numberToSkip)
		{
			NumberToSkip = numberToSkip;
		}

		public bool Equals(SkipQueryPart other)
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
				result = other.NumberToSkip.Equals(NumberToSkip);
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
			else if (obj.GetType() != typeof (SkipQueryPart))
			{
				result = false;
			}
			else
			{
				result = Equals((SkipQueryPart) obj);
			}

			return result;
		}

		public override int GetHashCode()
		{
			return (NumberToSkip.HasValue ? NumberToSkip.Value : 0);
		}

		public override string ToString()
		{
			var result = String.Empty;

			if (NumberToSkip.HasValue)
			{
				result = BuildParameterString(NumberToSkip.Value);
			}

			return result;
		}
	}
}
