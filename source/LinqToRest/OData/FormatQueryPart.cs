using System;

namespace LinqToRest.OData
{
	public class FormatQueryPart : ODataQueryPart, IEquatable<FormatQueryPart>
	{
		public override ODataQueryPartType QueryPartType { get { return ODataQueryPartType.Format; } }

		public ODataFormat DataFormat { get; private set; }

		public FormatQueryPart(ODataFormat format)
		{
			DataFormat = format;
		}

		public bool Equals(FormatQueryPart other)
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
				result = Equals(other.DataFormat, DataFormat);
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
			else if (obj.GetType() != typeof (FormatQueryPart))
			{
				result = false;
			}
			else
			{
				result = Equals((FormatQueryPart) obj);
			}
			return result;
		}

		public override int GetHashCode()
		{
			return DataFormat.GetHashCode();
		}

		public override string ToString()
		{
			var result = BuildParameterString(DataFormat.ToString().ToLowerInvariant());

			return result;
		}
	}
}
