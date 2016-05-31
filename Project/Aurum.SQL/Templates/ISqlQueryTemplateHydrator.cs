using Aurum.SQL.Templates;

namespace Aurum.SQL
{
	public interface ISqlQueryTemplateHydrator
	{
		ISqlQueryTemplate Hydrate(SqlQueryTemplateData data);
	}
}