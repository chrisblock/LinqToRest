using System;

namespace LinqToRest.OData
{
	public class ODataOrdering : ODataQuery
	{
		public string Field { get; private set; }

		public ODataOrderingDirection Direction { get; private set; }

		public override ODataQueryPartType QueryType { get { return ODataQueryPartType.Ordering; } }

		public ODataOrdering(string fieldName, ODataOrderingDirection direction)
		{
			Field = fieldName;
			Direction = direction;
		}

		public override string ToString()
		{
			return String.Format("{0} {1}", Field, Direction.ToString().ToLowerInvariant());
		}
	}
}
