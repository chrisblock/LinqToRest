using Remotion.Linq;

namespace LinqToRest.Client.Linq
{
	public interface IQueryModelTranslator
	{
		string Translate(QueryModel queryModel);
	}
}
