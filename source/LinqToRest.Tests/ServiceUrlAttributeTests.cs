// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;

namespace LinqToRest.Tests
{
	[TestFixture]
	public class ServiceUrlAttributeTests
	{
		[Test]
		public void Constructor_NullUrl_ThrowsException()
		{
			Assert.That(() => new ServiceUrlAttribute(null), Throws.ArgumentException);
		}

		[Test]
		public void Constructor_EmptyUrl_ThrowsException()
		{
			Assert.That(() => new ServiceUrlAttribute(String.Empty), Throws.ArgumentException);
		}

		[Test]
		public void Constructor_WhitepsaceUrl_ThrowsException()
		{
			Assert.That(() => new ServiceUrlAttribute(" \t\r\n"), Throws.ArgumentException);
		}

		[Test]
		public void Constructor_MalformedUrl_ThrowsException()
		{
			Assert.That(() => new ServiceUrlAttribute("http://localhost:9000/path/to/collection"), Throws.ArgumentException);
		}

		[Test]
		public void GetItemUri_NullItemId_ThrowsException()
		{
			var attribute = new ServiceUrlAttribute("http://localhost:9000/path/to/collection/");

			Assert.That(() => attribute.GetItemUri(null), Throws.ArgumentException);
		}

		[Test]
		public void GetItemUri_EmptyStringItemId_ThrowsException()
		{
			var attribute = new ServiceUrlAttribute("http://localhost:9000/path/to/collection/");

			Assert.That(() => attribute.GetItemUri(String.Empty), Throws.ArgumentException);
		}

		[Test]
		public void GetItemUri_ValidItemId_ReturnsCorrectUrl()
		{
			var url = "http://localhost:9000/path/to/collection/";

			var attribute = new ServiceUrlAttribute(url);

			var id = Guid.NewGuid();

			var result = attribute.GetItemUri(id);

			Assert.That(result.ToString(), Is.EqualTo(String.Format("{0}{1}", url, id)));
		}
	}
}
