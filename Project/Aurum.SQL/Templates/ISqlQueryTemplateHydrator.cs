using Aurum.SQL.Data;
using Aurum.SQL.Templates;

namespace Aurum.SQL
{
	public interface ISqlQueryTemplateHydrator
	{
		ISqlQueryTemplate Hydrate(SqlQueryTemplateData data);
	}
}