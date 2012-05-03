namespace LinqToRest.OData
{
	public class ODataFormatQueryPart : ODataQuery
	{
		public override ODataQueryPartType QueryType { get { return ODataQueryPartType.Format; } }

		public ODataFormat DataFormat { get; private set; }

		public ODataFormatQueryPart(ODataFormat format)
		{
			DataFormat = format;
		}

		public override string ToString()
		{
			var result = BuildParameterString(DataFormat.ToString().ToLowerInvariant());

			return result;
		}
	}
}
