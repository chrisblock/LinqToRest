namespace LinqToRest.Delta
{
	public static class ChangeSetExtensions
	{
		public static ChangeSet<T> Diff<T>(this T left, T right)
		{
			return ChangeSet.Generate(left, right);
		}
	}
}
