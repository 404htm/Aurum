using Aurum.SQL.Data;

namespace Aurum.SQL.Loaders
{
    public interface ISqlQueryMetadataLoader
    {
        SqlQueryDetail LoadQueryDetails(SqlQueryDefinition queryDefinition);
    }
}