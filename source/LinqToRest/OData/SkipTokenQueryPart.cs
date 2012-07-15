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
