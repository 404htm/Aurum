using Aurum.SQL.Data;
using System;
using System.Collections.Generic;

namespace Aurum.SQL
{
	public interface ISqlSchemaReader : IDisposable
	{
		SqlTableDetail GetTableDetail(SqlTableInfo tableInfo);
		IList<SqlTableInfo> GetTables(string schema = null);
		IList<SqlTableInfo> GetViews(string schema = null);
	}
}