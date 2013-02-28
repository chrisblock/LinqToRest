// ReSharper disable InconsistentNaming

using System;

using NUnit.Framework;

namespace LinqToRest.Client.Tests
{
	[TestFixture]
	public class UriFactoryTests
	{
		private const string Url = "http://www.example.com:9001/api/";
		private IUriFactory _uriFactory;

		[SetUp]
		public void TestSetUp()
		{
			_uriFactory = new UriFactory(new Uri(Url));
		}

		[TearDown]
		public void TestTearDown()
		{
			_uriFactory = null;
		}

		[Test]
		public void GetItemUri_GenericTypeArgumentAndGuidId_UsesTypeNameAsResourceNameAnd()
		{
			var id = Guid.NewGuid();
			var typeName = typeof (int).Name;
			var uri = _uriFactory.CreateItemUri<int>(id);

			Assert.That(uri.ToString(), Is.EqualTo(String.Format("{0}{1}/{2}", Url, typeName, id)));
		}

		[Test]
		public void GetItemUri_TypeArgumentAndGuidId_UsesTypeNameAsResourceName()
		{
			var id = Guid.NewGuid();
			var type = typeof (int);
			var typeName = type.Name;
			var uri = _uriFactory.CreateItemUri(type, id);

			Assert.That(uri.ToString(), Is.EqualTo(String.Format("{0}{1}/{2}", Url, typeName, id)));
		}

		[Test]
		public void GetItemUri_TypeNameArgumentAndGuidId_UsesTypeNameAsResourceName()
		{
			var id = Guid.NewGuid();
			var typeName = typeof (int).Name;
			var uri = _uriFactory.CreateItemUri(typeName, id);

			Assert.That(uri.ToString(), Is.EqualTo(String.Format("{0}{1}/{2}", Url, typeName, id)));
		}

		[Test]
		public void GetCollectionUri_GenericTypeArgument_UsesTypeNameAsResourceName()
		{
			var typeName = typeof(int).Name;
			var uri = _uriFactory.GetCollectionUri<int>();

			Assert.That(uri.ToString(), Is.EqualTo(String.Format("{0}{1}/", Url, typeName)));
		}

		[Test]
		public void GetCollectionUri_TypeArgument_UsesTypeNameAsResourceName()
		{
			var type = typeof (int);
			var typeName = type.Name;
			var uri = _uriFactory.GetCollectionUri(type);

			Assert.That(uri.ToString(), Is.EqualTo(String.Format("{0}{1}/", Url, typeName)));
		}

		[Test]
		public void GetCollectionUri_TypeNameArgument_UsesTypeNameAsResourceName()
		{
			var typeName = typeof(int).Name;
			var uri = _uriFactory.GetCollectionUri(typeName);

			Assert.That(uri.ToString(), Is.EqualTo(String.Format("{0}{1}/", Url, typeName)));
		}
	}
}
