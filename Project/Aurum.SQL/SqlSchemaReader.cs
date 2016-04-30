using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.SQL
{
	public class SqlSchemaReader : IDisposable
	{
		SqlConnection _cnn;

		public SqlSchemaReader(string connectionString)
		{
			_cnn = new SqlConnection(connectionString);
			_cnn.Open();
		}

		public IList<string> GetTables(string schema = null)
		{
			return runInfoSchemaQuery(schema, "BASE TABLE").ToList();
		}

		public IList<string> GetViews(string schema = null)
		{
			return runInfoSchemaQuery(schema, "VIEW").ToList();
		}

		public IList<SqlColumnInfo> GetColumns(string objectname)
		{
			return runColumnQuery(objectname).ToList();
		}

		//public SqlTableInfo GetTableInfo(string objectname)
		//{
		//    var result = new SqlTableInfo
		//    {
		//        Name = objectname,
		//        ColumnInfo = GetColumns(objectname)
		//    }
		//}

		private IEnumerable<SqlColumnInfo> runColumnQuery(string objectname)
		{
			string query = "SELECT [name], [column_id], [is_nullable], [is_identity] FROM sys.columns WHERE object_id = OBJECT_ID(@object_name)";

			var command = new SqlCommand(query, _cnn);
			command.CommandType = CommandType.Text;
			command.Parameters.AddWithValue("@object_name", objectname);

			var reader = command.ExecuteReader();

			while (reader.Read())
			{
				yield return new SqlColumnInfo
				{
					Name = Convert.ToString(reader["name"]),
					ColumnId = Convert.ToInt32(reader["column_id"]),
					Nullable = Convert.ToBoolean(reader["is_nullable"]),
					Identity = Convert.ToBoolean(reader["is_identity"])
				};
			}

			reader.Close();
		}

		private IEnumerable<string> runInfoSchemaQuery(string schema, string type)
		{
			string query = "SELECT [TABLE_SCHEMA], [TABLE_NAME] FROM information_schema.tables WHERE TABLE_TYPE = @type";
			if (schema != null) query += " AND TABLE_SCHEMA = @schema";

			var command = new SqlCommand(query, _cnn);
			command.CommandType = CommandType.Text;
			command.Parameters.AddWithValue("@type", type);
			if (schema != null) command.Parameters.AddWithValue("@schema", schema);

			var reader = command.ExecuteReader();

			while (reader.Read())
			{
				yield return $"[{reader["TABLE_SCHEMA"]}].[{reader["TABLE_NAME"]}]";
			}
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
