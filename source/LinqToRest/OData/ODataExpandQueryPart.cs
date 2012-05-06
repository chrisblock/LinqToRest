using System;

namespace LinqToRest.OData
{
	public class ODataExpandQueryPart : ODataQuery
	{
		public override ODataQueryPartType QueryType { get { return ODataQueryPartType.Expand; } }

		public string Predicate { get; private set; }

		internal ODataExpandQueryPart(string predicate)
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
