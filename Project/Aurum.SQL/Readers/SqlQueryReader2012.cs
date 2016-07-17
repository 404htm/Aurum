using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Aurum.SQL.Data;

using static Aurum.SQL.Helpers.SqlHelpers;

namespace Aurum.SQL.Readers
{
	/// <summary>Provides Query Metadata for SQL2012 and Newer</summary>
	public class SqlQueryReader2012 : IDisposable, ISqlValidator
	{
		SqlConnection _cnn;

		public SqlQueryReader2012(string connectionString)
		{
			_cnn = new SqlConnection(connectionString);
			_cnn.Open();
		}

		public bool Validate(string query, out IList<SqlError> errors)
		{
			var result = RunAndGetErrors(() => RunParameterQuery(query).ToList(), out errors);
			return errors == null;
		}

		public IList<SqlParameterInfo> GetParameters(string query, out IList<SqlError> errors)
		{
			return RunAndGetErrors(() => RunParameterQuery(query).ToList(), out errors);
		}

		public IList<SqlParameterInfo> GetResultSet(string query, out IList<SqlError> errors)
		{
			return RunAndGetErrors(() => RunResultSetQuery(query).ToList(), out errors);
		}

		private IEnumerable<SqlParameterInfo> RunParameterQuery(string sql)
		{
			var query = "sys.sp_describe_undeclared_parameters";
			var command = new SqlCommand(query, _cnn) { CommandType = CommandType.StoredProcedure }
				.AddParam("tsql", sql);

			using (var reader = command.ExecuteReader())
			{
				while (reader.Read()) yield return new SqlParameterInfo
				{
					Name = Convert.ToString(reader["name"]),
					ColumnId = Convert.ToInt32(reader["parameter_ordinal"]),
					SQLType = Convert.ToString(reader["suggested_system_type_name"])
					//Nullable = Convert.ToBoolean(reader["is_nullable"]),
					//Identity = Convert.ToBoolean(reader["is_identity"])
				};
			}
		}

		private IEnumerable<SqlParameterInfo> RunResultSetQuery(string sql)
		{
			var query = "sys.dm_exec_describe_first_result_set";
			var command = new SqlCommand(query, _cnn) {CommandType = CommandType.TableDirect}
				.AddParam("tsql", sql)
				.AddParam("params", (string)null)
				.AddParam("browse_information_mode", (Int16)1);

			using (var reader = command.ExecuteReader())
			{
				while (reader.Read()) yield return new SqlParameterInfo
				{
					Name = Convert.ToString(reader["name"]),
					ColumnId = Convert.ToInt32(reader["parameter_ordinal"]),
					SQLType = Convert.ToString(reader["suggested_system_type_name"])
					//Nullable = Convert.ToBoolean(reader["is_nullable"]),
					//Identity = Convert.ToBoolean(reader["is_identity"])
				};
			}
		}

		#region IDisposable Support
		private bool disposedValue = false;
		public void Dispose() => Dispose(true);
		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing) _cnn.Close();
				disposedValue = true;
			}
		}
		#endregion
	}
}
