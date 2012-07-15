using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApi.SelfHost
{
	[Serializable]
	public class RouteConfigurationTable
	{
		private readonly IList<RouteCounfiguration> _configurations;
		public IEnumerable<RouteCounfiguration> Configurations { get { return _configurations; } }

		public RouteConfigurationTable()
		{
			_configurations = new List<RouteCounfiguration>();
		}

		public RouteConfigurationTable(params RouteCounfiguration[] routes) : this(routes.AsEnumerable())
		{
		}

		public RouteConfigurationTable(IEnumerable<RouteCounfiguration> routes) : this()
		{
			foreach (var route in routes)
			{
				Add(route);
			}
		}

		public void Add(RouteCounfiguration entry)
		{
			_configurations.Add(entry);
		}

		public void Add(string name, string template, object defaults = null)
		{
			var entry = new RouteCounfiguration
			{
				Name = name,
				Template = template,
				Defaults = defaults
			};

			Add(entry);
		}

		public void Configure(HttpRouteCollection httpRouteCollection)
		{
			foreach (var routeTableEntry in Configurations)
			{
				httpRouteCollection.MapHttpRoute(routeTableEntry.Name, routeTableEntry.Template, routeTableEntry.Defaults);
			}
		}
	}
}
