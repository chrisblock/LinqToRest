// ReSharper disable InconsistentNaming

using System;
using System.Linq;

using LinqToRest.OData;
using LinqToRest.OData.Filters;
using LinqToRest.Server.OData.Parsing;
using LinqToRest.Server.OData.Parsing.Impl;

using NUnit.Framework;

namespace LinqToRest.Server.Tests
{
	[TestFixture]
	public class ODataQueryParserTests
	{
		private ODataQueryParser _parser;

		[SetUp]
		public void TestSetUp()
		{
			_parser = new ODataQueryParser(new ODataQueryPartParserStrategy());
		}

		[Test]
		public void Parse_ValidUriWithNoQueryParameters_ReturnsCompleteODataQueryObjectWithAllNullProperties()
		{
			var uri = new Uri("http://www.site.com/path/Model");

			var result = _parser.Parse(uri);

			Assert.That(result.ExpandPredicate, Is.Null);
			Assert.That(result.FilterPredicate, Is.Null);
			Assert.That(result.FormatPredicate, Is.Null);
			Assert.That(result.InlineCountPredicate, Is.Null);
			Assert.That(result.OrderByPredicate, Is.Null);
			Assert.That(result.SelectPredicate, Is.Null);
			Assert.That(result.SkipPredicate, Is.Null);
			Assert.That(result.SkipTokenPredicate, Is.Null);
			Assert.That(result.TopPredicate, Is.Null);
			Assert.That(result.Uri.ToString(), Is.EqualTo("http://www.site.com/path/Model"));
		}

		[Test]
		public void Parse_ValidUriWithCountQueryParameter_ReturnsCompleteODataQueryObjectWithCorrectProperties()
		{
			var uri = new Uri("http://www.site.com/path/Model?$count");

			var result = _parser.Parse(uri);

			Assert.That(result.CountPredicate, Is.Not.Null);
			Assert.That(result.ExpandPredicate, Is.Null);
			Assert.That(result.FilterPredicate, Is.Null);
			Assert.That(result.FormatPredicate, Is.Null);
			Assert.That(result.InlineCountPredicate, Is.Null);
			Assert.That(result.OrderByPredicate, Is.Null);
			Assert.That(result.SelectPredicate, Is.Null);
			Assert.That(result.SkipPredicate, Is.Null);
			Assert.That(result.SkipTokenPredicate, Is.Null);
			Assert.That(result.TopPredicate, Is.Null);
			Assert.That(result.Uri.ToString(), Is.EqualTo("http://www.site.com/path/Model"));
		}

		[Test]
		public void Parse_ValidUriWithExpandQueryParameter_ReturnsCompleteODataQueryObjectWithCorrectProperties()
		{
			var uri = new Uri("http://www.site.com/path/Model?$expand=TestProperty");

			var result = _parser.Parse(uri);

			Assert.That(result.CountPredicate, Is.Null);
			Assert.That(result.ExpandPredicate, Is.Not.Null);
			Assert.That(result.ExpandPredicate.Members, Is.Not.Null);
			Assert.That(result.ExpandPredicate.Members.First(), Is.EqualTo(FilterExpression.MemberAccess("TestProperty")));
			Assert.That(result.FilterPredicate, Is.Null);
			Assert.That(result.FormatPredicate, Is.Null);
			Assert.That(result.InlineCountPredicate, Is.Null);
			Assert.That(result.OrderByPredicate, Is.Null);
			Assert.That(result.SelectPredicate, Is.Null);
			Assert.That(result.SkipPredicate, Is.Null);
			Assert.That(result.SkipTokenPredicate, Is.Null);
			Assert.That(result.TopPredicate, Is.Null);
			Assert.That(result.Uri.ToString(), Is.EqualTo("http://www.site.com/path/Model"));
		}

		[Test]
		public void Parse_ValidUriWithFormatQueryParameter_ReturnsCompleteODataQueryObjectWithCorrectProperties()
		{
			var uri = new Uri("http://www.site.com/path/Model?$format=atom");

			var result = _parser.Parse(uri);

			Assert.That(result.CountPredicate, Is.Null);
			Assert.That(result.ExpandPredicate, Is.Null);
			Assert.That(result.FilterPredicate, Is.Null);
			Assert.That(result.FormatPredicate, Is.Not.Null);
			Assert.That(result.FormatPredicate.DataFormat, Is.EqualTo(ODataFormat.Atom));
			Assert.That(result.InlineCountPredicate, Is.Null);
			Assert.That(result.OrderByPredicate, Is.Null);
			Assert.That(result.SelectPredicate, Is.Null);
			Assert.That(result.SkipPredicate, Is.Null);
			Assert.That(result.SkipTokenPredicate, Is.Null);
			Assert.That(result.TopPredicate, Is.Null);
			Assert.That(result.Uri.ToString(), Is.EqualTo("http://www.site.com/path/Model"));
		}

		[Test]
		public void Parse_ValidUriWithInlineCountQueryParameter_ReturnsCompleteODataQueryObjectWithCorrectProperties()
		{
			var uri = new Uri("http://www.site.com/path/Model?$inlinecount=allpages");

			var result = _parser.Parse(uri);

			Assert.That(result.CountPredicate, Is.Null);
			Assert.That(result.ExpandPredicate, Is.Null);
			Assert.That(result.FilterPredicate, Is.Null);
			Assert.That(result.FormatPredicate, Is.Null);
			Assert.That(result.InlineCountPredicate, Is.Not.Null);
			Assert.That(result.InlineCountPredicate.InlineCountType, Is.EqualTo(InlineCountType.AllPages));
			Assert.That(result.OrderByPredicate, Is.Null);
			Assert.That(result.SelectPredicate, Is.Null);
			Assert.That(result.SkipPredicate, Is.Null);
			Assert.That(result.SkipTokenPredicate, Is.Null);
			Assert.That(result.TopPredicate, Is.Null);
			Assert.That(result.Uri.ToString(), Is.EqualTo("http://www.site.com/path/Model"));
		}

		[Test]
		public void Parse_ValidUriWithOrderByQueryParameter_ReturnsCompleteODataQueryObjectWithCorrectProperties()
		{
			var uri = new Uri("http://www.site.com/path/Model?$orderby=TestProperty desc");

			var result = _parser.Parse(uri);

			Assert.That(result.CountPredicate, Is.Null);
			Assert.That(result.ExpandPredicate, Is.Null);
			Assert.That(result.FilterPredicate, Is.Null);
			Assert.That(result.FormatPredicate, Is.Null);
			Assert.That(result.InlineCountPredicate, Is.Null);
			Assert.That(result.OrderByPredicate, Is.Not.Null);
			Assert.That(result.OrderByPredicate.Orderings.Count, Is.EqualTo(1));
			Assert.That(result.OrderByPredicate.Orderings.First().Field, Is.EqualTo("TestProperty"));
			Assert.That(result.OrderByPredicate.Orderings.First().Direction, Is.EqualTo(ODataOrderingDirection.Desc));
			Assert.That(result.SelectPredicate, Is.Null);
			Assert.That(result.SkipPredicate, Is.Null);
			Assert.That(result.SkipTokenPredicate, Is.Null);
			Assert.That(result.TopPredicate, Is.Null);
			Assert.That(result.Uri.ToString(), Is.EqualTo("http://www.site.com/path/Model"));
		}

		[Test]
		public void Parse_ValidUriWithSelectQueryParameter_ReturnsCompleteODataQueryObjectWithCorrectProperties()
		{
			var uri = new Uri("http://www.site.com/path/Model?$select=TestProperty");

			var result = _parser.Parse(uri);

			Assert.That(result.CountPredicate, Is.Null);
			Assert.That(result.ExpandPredicate, Is.Null);
			Assert.That(result.FilterPredicate, Is.Null);
			Assert.That(result.FormatPredicate, Is.Null);
			Assert.That(result.InlineCountPredicate, Is.Null);
			Assert.That(result.OrderByPredicate, Is.Null);
			Assert.That(result.SelectPredicate, Is.Not.Null);
			Assert.That(result.SelectPredicate.Selectors.Count, Is.EqualTo(1));
			Assert.That(result.SelectPredicate.Selectors.First().Instance, Is.Null);
			Assert.That(result.SelectPredicate.Selectors.First().Member, Is.EqualTo("TestProperty"));
			Assert.That(result.SkipPredicate, Is.Null);
			Assert.That(result.SkipTokenPredicate, Is.Null);
			Assert.That(result.TopPredicate, Is.Null);
			Assert.That(result.Uri.ToString(), Is.EqualTo("http://www.site.com/path/Model"));
		}

		[Test]
		public void Parse_ValidUriWithSkipQueryParameter_ReturnsCompleteODataQueryObjectWithCorrectProperties()
		{
			var uri = new Uri("http://www.site.com/path/Model?$skip=10");

			var result = _parser.Parse(uri);

			Assert.That(result.CountPredicate, Is.Null);
			Assert.That(result.ExpandPredicate, Is.Null);
			Assert.That(result.FilterPredicate, Is.Null);
			Assert.That(result.FormatPredicate, Is.Null);
			Assert.That(result.InlineCountPredicate, Is.Null);
			Assert.That(result.OrderByPredicate, Is.Null);
			Assert.That(result.SelectPredicate, Is.Null);
			Assert.That(result.SkipPredicate, Is.Not.Null);
			Assert.That(result.SkipPredicate.NumberToSkip, Is.Not.Null);
			Assert.That(result.SkipPredicate.NumberToSkip, Is.EqualTo(10));
			Assert.That(result.SkipTokenPredicate, Is.Null);
			Assert.That(result.TopPredicate, Is.Null);
			Assert.That(result.Uri.ToString(), Is.EqualTo("http://www.site.com/path/Model"));
		}

		[Test]
		public void Parse_ValidUriWithSkipTokenQueryParameter_ReturnsCompleteODataQueryObjectWithCorrectProperties()
		{
			var uri = new Uri("http://www.site.com/path/Model?$skiptoken=TestProperty");

			var result = _parser.Parse(uri);

			Assert.That(result.CountPredicate, Is.Null);
			Assert.That(result.ExpandPredicate, Is.Null);
			Assert.That(result.FilterPredicate, Is.Null);
			Assert.That(result.FormatPredicate, Is.Null);
			Assert.That(result.InlineCountPredicate, Is.Null);
			Assert.That(result.OrderByPredicate, Is.Null);
			Assert.That(result.SelectPredicate, Is.Null);
			Assert.That(result.SkipPredicate, Is.Null);
			Assert.That(result.SkipTokenPredicate, Is.Not.Null);
			Assert.That(result.SkipTokenPredicate.Predicate, Is.Not.Null);
			Assert.That(result.SkipTokenPredicate.Predicate, Is.EqualTo("TestProperty"));
			Assert.That(result.TopPredicate, Is.Null);
			Assert.That(result.Uri.ToString(), Is.EqualTo("http://www.site.com/path/Model"));
		}
	}
}
