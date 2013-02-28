using System;

namespace LinqToRest.OData
{
	public class SkipTokenQueryPart : ODataQueryPart
	{
		public string Predicate { get; private set; }

		public override ODataQueryPartType QueryPartType { get { return ODataQueryPartType.SkipToken; } }

		public SkipTokenQueryPart(string predicate)
		{
			Predicate = predicate;
		}

		public bool Equals(SkipTokenQueryPart other)
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
				result = Equals(other.Predicate, Predicate);
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
			else if (obj.GetType() != typeof (SkipTokenQueryPart))
			{
				result = false;
			}
			else
			{
				result = Equals((SkipTokenQueryPart) obj);
			}
			return result;
		}

		public override int GetHashCode()
		{
			return (Predicate != null ? Predicate.GetHashCode() : 0);
		}

		public override string ToString()
		{
			var result = String.Empty;

			if (String.IsNullOrWhiteSpace(Predicate) == false)
			{
				result = BuildParameterString(Predicate);
			}

			return result;
		}
	}
}
