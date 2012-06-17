using LinqToRest.OData;
using LinqToRest.OData.Impl;
using LinqToRest.Serialization;
using LinqToRest.Serialization.Impl;
using LinqToRest.Server.OData;
using LinqToRest.Server.OData.Expressions;
using LinqToRest.Server.OData.Expressions.Impl;
using LinqToRest.Server.OData.Parsing;
using LinqToRest.Server.OData.Parsing.Impl;

namespace LinqToRest.Server
{
	internal class ServerDependencyResolver : AbstractDependencyResolver
	{
		public ServerDependencyResolver()
		{
			Register<ISerializer, JsonSerializer>();
			Register<IODataQueryPartParserStrategy, ODataQueryPartParserStrategy>();
			Register<IODataQueryFactory, DefaultODataQueryFactory>();
			Register<IFilterExpressionBuilder, FilterExpressionBuilder>();
			Register<IODataQueryTranslator, ExpressionODataQueryVisitor>();
		}
	}
}
