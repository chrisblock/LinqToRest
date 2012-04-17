using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using NUnit.Framework;

namespace LinqToRest.Tests
{
	[TestFixture]
	public class RestQueryModelVisitorTests
	{
		[Test]
		public void WhatUp()
		{
			var visitor = new OpenData.ODataExpressionVisitor();

			Expression<Func<string, bool>> yellow = s => s == "3";

			var hello = visitor.Translate(yellow.Body);
		}

		[Test]
		public void Test()
		{
			var queryable = RestQueryableFactory.Create<int>("http://localhost/Hello");

			var skipped = queryable.Skip(1).Take(1).ToList();
		}
	}
}
