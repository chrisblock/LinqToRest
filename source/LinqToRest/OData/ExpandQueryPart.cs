using System;

namespace LinqToRest.OData
{
	public class ExpandQueryPart : ODataQueryPart
	{
		public override ODataQueryPartType QueryPartType { get { return ODataQueryPartType.Expand; } }

		public string Predicate { get; private set; }

		internal ExpandQueryPart(string predicate)
		{
			Predicate = predicate;
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
