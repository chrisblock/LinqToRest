namespace DataModel.Tests
{
	public class TypeWithNoParameterlessConstructor
	{
		public string Parameter { get; set; }

		public TypeWithNoParameterlessConstructor(string parameter)
		{
			Parameter = parameter;
		}
	}
}
