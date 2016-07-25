using Aurum.Core;
using Aurum.SQL.Data;
using Aurum.SQL.Readers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.SQL.Loaders
{
	public class SqlQueryMetadataLoader
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
			detail.Outputs = _queryReader.GetResultSet(queryDefinition.Query, out errors);
			return detail;
		}
	}
}
