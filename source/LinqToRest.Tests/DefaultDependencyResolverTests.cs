using LinqToRest.Http;
using LinqToRest.Http.Impl;
using LinqToRest.Serialization;
using LinqToRest.Serialization.Impl;

using NUnit.Framework;

namespace LinqToRest.Tests
{
	[TestFixture]
	public class DefaultDependencyResolverTests
	{
		private static readonly IDependencyResolver _dependencyResolver = new DefaultDependencyResolver();

		[Test]
		public void GetInstance_IHttpService_ReturnsHttpService()
		{
			var httpService = _dependencyResolver.GetInstance(typeof (IHttpService));

			Assert.That(httpService, Is.Not.Null);
			Assert.That(httpService, Is.TypeOf<HttpService>());
		}

		[Test]
		public void GetInstance_ISerializer_ReturnsJsonSerializer()
		{
			var jsonSerializer = _dependencyResolver.GetInstance(typeof (ISerializer));

			Assert.That(jsonSerializer, Is.Not.Null);
			Assert.That(jsonSerializer, Is.TypeOf<JsonSerializer>());
		}

		[Test]
		public void GetInstance_ConcreteTypeWithParameterlessConstructor_ReturnsNewInstance()
		{
			var fixture = _dependencyResolver.GetInstance(GetType());

			Assert.That(fixture, Is.Not.Null);
			Assert.That(fixture, Is.TypeOf<DefaultDependencyResolverTests>());
		}

		[Test]
		public void GetInstance_ConcreteTypeWithNoParameterlessConstructor_ReturnsNull()
		{
			var result = _dependencyResolver.GetInstance(typeof (TypeWithNoParameterlessConstructor));

			Assert.That(result, Is.Null);
		}
	}
}
