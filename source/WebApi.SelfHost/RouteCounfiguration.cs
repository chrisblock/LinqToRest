using System;

namespace WebApi.SelfHost
{
	[Serializable]
	public class RouteCounfiguration
	{
		public string Name { get; set; }
		public string Template { get; set; }
		public object Defaults { get; set; }
	}
}
