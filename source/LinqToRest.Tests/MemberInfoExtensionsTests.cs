// ReSharper disable InconsistentNaming

using System;
using System.Linq;
using System.Reflection;

using NUnit.Framework;

namespace LinqToRest.Tests
{
	[TestFixture]
	public class MemberInfoExtensionsTests
	{
		[Test]
		public void GetCustomAttributes_TypeWithNoCustomAttributesOfType_ReturnsEmptyList()
		{
			var attributes = GetType().GetCustomAttributes<TestAttribute>();

			Assert.That(attributes, Is.Not.Null);
			Assert.That(attributes, Is.Empty);
		}

		[Test]
		public void GetCustomAttributes_TypeWithCustomAttributesOfType_ReturnsList()
		{
			var attributes = GetType().GetCustomAttributes<TestFixtureAttribute>();

			Assert.That(attributes, Is.Not.Null);
			Assert.That(attributes, Is.Not.Empty);
			Assert.That(attributes, Has.All.TypeOf<TestFixtureAttribute>());
			Assert.That(attributes.Count(), Is.EqualTo(1));
		}

		[Test]
		public void GetMemberInfo_MemberAccessExpresion_ReturnsMemberInfo()
		{
			var memberInfo = typeof (DateTime).GetMember("Year", BindingFlags.Instance | BindingFlags.Public).Single();

			Assert.That(DateTime.Now.GetMemberInfo(x => x.Year), Is.EqualTo(memberInfo));
		}

		[Test]
		public void GetMemberInfo_NonMemberAccessExpresion_ThrowsArgumentException()
		{
			Assert.That(() => DateTime.Now.GetMemberInfo(x => x.ToUniversalTime()), Throws.ArgumentException);
		}
	}
}
