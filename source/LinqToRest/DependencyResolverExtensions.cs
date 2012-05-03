namespace LinqToRest
{
	public static class DependencyResolverExtensions
	{
		public static T GetInstance<T>(this IDependencyResolver dependencyResolver)
		{
			return (T) dependencyResolver.GetInstance(typeof (T));
		}
	}
}
