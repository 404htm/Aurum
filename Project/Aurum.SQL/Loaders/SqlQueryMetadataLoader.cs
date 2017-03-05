using Aurum.SQL.Data;
using Aurum.SQL.Readers;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Aurum.SQL.Loaders
{
    public class SqlQueryMetadataLoader : ISqlQueryMetadataLoader
    {
        ISqlQueryReader _queryReader;

        public SqlQueryMetadataLoader(ISqlQueryReader queryReader)
        {
            _queryReader = queryReader;
        }

        public SqlQueryDetail LoadQueryDetails(SqlQueryDefinition queryDefinition)
        {
            var detail = SqlQueryDetail.MapFrom(queryDefinition);
            IList<SqlError> errors = new List<SqlError>();
            detail.Inputs = _queryReader.GetParameters(queryDefinition.Query, out errors);
            detail.Outputs = _queryReader.GetResultStructure(queryDefinition.Query, out errors);
            return detail;
        }
    }
}
