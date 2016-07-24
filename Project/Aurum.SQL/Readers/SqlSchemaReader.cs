using Aurum.SQL.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.SQL
{
	/// <summary> Reads metadata around database objects</summary>
	public class SqlSchemaReader : IDisposable, ISqlSchemaReader
	{
		SqlConnection _cnn;

		public SqlSchemaReader(string connectionString)
		{
			_cnn = new SqlConnection(connectionString);
			_cnn.Open();
		}

		public IList<SqlTableInfo> GetTables(string schema = null)
		{
			return runInfoSchemaQuery(schema, "BASE TABLE").ToList();
		}

		public IList<SqlTableInfo> GetViews(string schema = null)
		{
			return runInfoSchemaQuery(schema, "VIEW").ToList();
		}

		public SqlTableDetail GetTableDetail(SqlTableInfo tableInfo)
		{
			return new SqlTableDetail(tableInfo)
			{
				Columns = runColumnQuery(tableInfo.Name).ToList()
			};
		}

		//public SqlTableInfo GetTableInfo(string objectname)
		//{
		//    var result = new SqlTableInfo
		//    {
		//        Name = objectname,
		//        ColumnInfo = GetColumns(objectname)
		//    }
		//}

		private static SqlColumn mapColumn(IDataReader reader)
		{
			return new SqlColumn
			{
				Name = Convert.ToString(reader["name"]),
				Order = Convert.ToInt32(reader["column_id"]),
				Nullable = Convert.ToBoolean(reader["is_nullable"]),
				Identity = Convert.ToBoolean(reader["is_identity"])
			};
		}

		private static T mapTable<T>(IDataReader reader, T table) where T : SqlTableInfo
		{
			table.Name = reader["TABLE_NAME"].ToString();
			table.Schema = reader["TABLE_SCHEMA"].ToString();
			return table;
		}

		private IEnumerable<SqlColumn> runColumnQuery(string objectname)
		{
			string query = "SELECT [name], [column_id], [is_nullable], [is_identity] FROM sys.columns WHERE object_id = OBJECT_ID(@object_name)";

			var command = new SqlCommand(query, _cnn);
			command.CommandType = CommandType.Text;
			command.Parameters.AddWithValue("@object_name", objectname);

			var reader = command.ExecuteReader();

			while (reader.Read()) yield return mapColumn(reader);
			reader.Close();
		}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
		private IEnumerable<SqlTableInfo> runInfoSchemaQuery(string schema, string type)
		{
			string query = "SELECT [TABLE_SCHEMA], [TABLE_NAME] FROM information_schema.tables WHERE TABLE_TYPE = @type";
			if (schema != null) query += " AND TABLE_SCHEMA = @schema";

			var command = new SqlCommand(query, _cnn);
			command.CommandType = CommandType.Text;
			command.Parameters.AddWithValue("@type", type);
			if (schema != null) command.Parameters.AddWithValue("@schema", schema);

			var reader = command.ExecuteReader();
			while (reader.Read()) yield return mapTable(reader, new SqlTableInfo());
			reader.Close();
		}

		

		#region IDisposable Support
		private bool disposedValue = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing) _cnn.Close();
				disposedValue = true;
			}
		}


		public void Dispose()
		{
			Dispose(true);
		}
		#endregion
	}
}
