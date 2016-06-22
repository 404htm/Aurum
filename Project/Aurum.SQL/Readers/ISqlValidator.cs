using Aurum.SQL.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Aurum.SQL
{
	public interface ISqlValidator : IDisposable
	{
		IList<SqlParameterInfo> GetParametersAndValidate(string sql, out IList<SqlError> errors);
		bool ParseSQLBasic(string sql);
	}
}