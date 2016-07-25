using System.Collections.Generic;
using System.Data.SqlClient;
using Aurum.SQL.Data;

namespace Aurum.SQL.Readers
{
	public interface ISqlQueryReader
	{
		IList<Data.SqlParameter> GetParameters(string query, out IList<SqlError> errors);
		IList<SqlColumn> GetResultSet(string query, out IList<SqlError> errors, params string[] parameters);
	}
}