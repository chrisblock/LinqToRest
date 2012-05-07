using System;
using System.Linq;
using System.Linq.Expressions;

using LinqToRest.OData.Parsing;

namespace LinqToRest.OData
{
	public class ODataUriParser
	{
		private readonly ODataQueryParser _parser;
		private readonly IODataQueryTranslator _translator;

		public ODataUriParser() : this(DependencyResolver.Current.GetInstance<ODataQueryParser>(), DependencyResolver.Current.GetInstance<IODataQueryTranslator>())
		{
		}

		public ODataUriParser(ODataQueryParser parser, IODataQueryTranslator translator)
		{
			_parser = parser;
			_translator = translator;
		}

		public LambdaExpression Parse(Type type, Uri uri)
		{
			var query = _parser.Parse(uri);

			var baseUrl = type.GetCustomAttributes<ServiceUrlAttribute>().Single().Url;

			if (baseUrl != query.Uri.ToString())
			{
				throw new ArgumentException(String.Format("URI '{0}' does not correspond to the service URI of type '{1}'.", uri, type));
			}

			var lambda = _translator.Translate(type, query);

			return lambda;
		}
	}
}
