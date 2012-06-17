using System;

using LinqToRest.Client.Http;
using LinqToRest.Server.OData;
using LinqToRest.Server.OData.Expressions;
using LinqToRest.Server.OData.Expressions.Impl;
using LinqToRest.Server.OData.Parsing;
using LinqToRest.Server.OData.Parsing.Impl;

namespace LinqToRest.Client.Tests.Mocks
{
	public class MockDependencyResolver : IDependencyResolver
	{
		private readonly IDependencyResolver _old;

		public MockDependencyResolver()
		{
			_old = DependencyResolver.Current;
		}

		public object GetInstance(Type type)
		{
			object result;

			if (type == typeof (IHttpService))
			{
				result = new MockHttpService();
			}
			else if (type == typeof (IODataQueryPartParserStrategy))
			{
				result = new ODataQueryPartParserStrategy();
			}
			else if (type == typeof (IODataQueryTranslator))
			{
				result = new ExpressionODataQueryVisitor();
			}
			else if (type == typeof (IFilterExpressionBuilder))
			{
				result = new FilterExpressionBuilder();
			}
			else
			{
				result = _old.GetInstance(type);
			}

			return result;
		}
	}
}
