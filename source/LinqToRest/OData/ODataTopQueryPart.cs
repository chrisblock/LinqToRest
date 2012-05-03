using System;

namespace LinqToRest.OData
{
	public class ODataTopQueryPart : ODataQuery
	{
		public override ODataQueryPartType QueryType { get { return ODataQueryPartType.Top; } }

		public int? NumberToTake { get; private set; }

		public ODataTopQueryPart(int? numberToTake)
		{
			NumberToTake = numberToTake;
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
