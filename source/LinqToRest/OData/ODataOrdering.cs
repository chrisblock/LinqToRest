using System;

namespace LinqToRest.OData
{
	public class ODataOrdering : ODataQueryPart, IEquatable<ODataOrdering>
	{
		public string Field { get; private set; }

		public ODataOrderingDirection Direction { get; private set; }

		public override ODataQueryPartType QueryPartType { get { return ODataQueryPartType.Ordering; } }

		public ODataOrdering(string fieldName, ODataOrderingDirection direction)
		{
			if (String.IsNullOrWhiteSpace(fieldName))
			{
				throw new ArgumentException(String.Format("Invalid field name '{0}'.", fieldName), "fieldName");
			}

			Field = fieldName;
			Direction = direction;
		}

		public bool Equals(ODataOrdering other)
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
				result = Equals(other.Field, Field) && Equals(other.Direction, Direction);
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
			else if (obj.GetType() != typeof (ODataOrdering))
			{
				result = false;
			}
			else
			{
				result = Equals((ODataOrdering) obj);
			}

			return result;
		}

		public override int GetHashCode()
		{
			var result = String.Format("Field:{0};Direction:{1};", Field, Direction);

			return result.GetHashCode();
		}

		public override string ToString()
		{
			var result = String.Format("{0} {1}", Field, Direction.ToString().ToLowerInvariant());

			return result;
		}
	}
}
