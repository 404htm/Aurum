﻿using System;
using System.Collections.Generic;

namespace Aurum.SQL
{
	public interface ISqlSchemaReader : IDisposable
	{
		void Dispose();
		SqlTableDetail GetTableDetail(SqlTableInfo tableInfo);
		IList<SqlTableInfo> GetTables(string schema = null);
		IList<SqlTableInfo> GetViews(string schema = null);
	}
}