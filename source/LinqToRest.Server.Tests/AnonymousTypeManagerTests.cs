// ReSharper disable InconsistentNaming

using System;
using System.Linq;
using System.Reflection;

using NUnit.Framework;

namespace LinqToRest.Server.Tests
{
	[TestFixture]
	public class AnonymousTypeManagerTests
	{
		[Test]
		public void BuildType_NullPropertyCollection_ThrowsException()
		{
			Assert.That(() => AnonymousTypeManager.BuildType(null), Throws.InstanceOf<ArgumentNullException>());
		}

		[Test]
		public void BuildType_EmptyPropertyCollection_ThrowsException()
		{
			Assert.That(() => AnonymousTypeManager.BuildType(Enumerable.Empty<Property>()), Throws.ArgumentException);
		}

		[Test]
		public void BuildType_PropertyCollectionContainingSingleProperty_GeneratesTypeCorrectly()
		{
			var properties = new[]
			{
				new Property
				{
					Type = typeof (string),
					Name = "TestProperty"
				}
			};

			var type = AnonymousTypeManager.BuildType(properties);

			Assert.That(type, Is.Not.Null);
			Assert.That(type.Name, Is.EqualTo("TestProperty"));
			Assert.That(type.GetFields(BindingFlags.Instance | BindingFlags.Public).Length, Is.EqualTo(1));
		}
	}
}
