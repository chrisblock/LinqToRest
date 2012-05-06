namespace LinqToRest.OData
{
	public class FormatQueryPart : ODataQueryPart
	{
		public override ODataQueryPartType QueryPartType { get { return ODataQueryPartType.Format; } }

		public ODataFormat DataFormat { get; private set; }

		public FormatQueryPart(ODataFormat format)
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
