using System;

namespace LinqToRest.OData
{
	public class ODataExpandQueryPart : ODataQuery
	{
		private readonly string _predicate;

		public override ODataQueryPartType QueryType { get { return ODataQueryPartType.Expand; } }

		internal ODataExpandQueryPart(string predicate)
		{
			_predicate = predicate;
		}

		public override string ToString()
		{
			var result = String.Empty;

			if (String.IsNullOrWhiteSpace(_predicate) == false)
			{
				result = BuildParameterString(_predicate);
			}

			return result;
		}
	}
}
