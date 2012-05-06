using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LinqToRest.OData;
using LinqToRest.OData.Parsing;

using NUnit.Framework;

namespace LinqToRest.Tests
{
	[TestFixture]
	public class ODataQueryParserTests
	{
		[Test]
		public void Parse_ValidUriWithNoQueryParameters_ReturnsCompleteODataQueryObjectWithAllNullProperties()
		{
			var uri = new Uri("http://www.site.com/path/Model");

			var parser = new ODataQueryParser();

			var result = (CompleteODataQuery)parser.Parse(uri);

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

			var parser = new ODataQueryParser();

			var result = (CompleteODataQuery)parser.Parse(uri);

			Assert.That(result.ExpandPredicate, Is.Not.Null);
			Assert.That(result.ExpandPredicate.Predicate, Is.Not.Null);
			Assert.That(result.ExpandPredicate.Predicate, Is.EqualTo("TestProperty"));
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

			var parser = new ODataQueryParser();

			var result = (CompleteODataQuery)parser.Parse(uri);

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

			var parser = new ODataQueryParser();

			var result = (CompleteODataQuery)parser.Parse(uri);

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

			var parser = new ODataQueryParser();

			var result = (CompleteODataQuery)parser.Parse(uri);

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

			var parser = new ODataQueryParser();

			var result = (CompleteODataQuery)parser.Parse(uri);

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

			var parser = new ODataQueryParser();

			var result = (CompleteODataQuery)parser.Parse(uri);

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

			var parser = new ODataQueryParser();

			var result = (CompleteODataQuery)parser.Parse(uri);

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
