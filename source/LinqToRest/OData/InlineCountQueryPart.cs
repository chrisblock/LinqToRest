using System;

namespace LinqToRest.OData
{
	public class InlineCountQueryPart : ODataQueryPart, IEquatable<InlineCountQueryPart>
	{
		public override ODataQueryPartType QueryPartType { get { return ODataQueryPartType.InlineCount; } }

		public InlineCountType InlineCountType { get; private set; }

		public InlineCountQueryPart(InlineCountType inlineCountType)
		{
			InlineCountType = inlineCountType;
		}

		public bool Equals(InlineCountQueryPart other)
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
				result = Equals(other.InlineCountType, InlineCountType);
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
			else if (obj.GetType() != typeof (InlineCountQueryPart))
			{
				result = false;
			}
			else
			{
				result = Equals((InlineCountQueryPart) obj);
			}

			return result;
		}

		public override int GetHashCode()
		{
			return InlineCountType.GetHashCode();
		}

		public override string ToString()
		{
			return BuildParameterString(InlineCountType.ToString().ToLowerInvariant());
		}
	}
}
