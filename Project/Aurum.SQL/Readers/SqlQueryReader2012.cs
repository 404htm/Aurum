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

		public IList<Data.SqlParameter> GetParameters(string query, out IList<SqlError> errors)
		{
			return RunAndGetErrors(() => RunParameterQuery(query).ToList(), out errors);
		}

		public IList<Data.SqlColumn> GetResultSet(string query, out IList<SqlError> errors, params string[] parameters)
		{
			return RunAndGetErrors(() => RunResultSetQuery(query, parameters).ToList(), out errors);
		}

		private IEnumerable<Data.SqlParameter> RunParameterQuery(string sql)
		{
			var query = "sys.sp_describe_undeclared_parameters";
			var command = new SqlCommand(query, _cnn) { CommandType = CommandType.StoredProcedure }
				.AddParam("tsql", sql);

			using (var reader = command.ExecuteReader())
			{
				while (reader.Read()) yield return new Data.SqlParameter
				{
					Name = Convert.ToString(reader["name"]),
					Order = Convert.ToInt32(reader["parameter_ordinal"]),
					SQLType = (SqlDbType)Convert.ToInt32(reader["system_type_id"]),
					//Nullable = Convert.ToBoolean(reader["is_nullable"]),
					//Identity = Convert.ToBoolean(reader["is_identity"])
				};
			}
		}

		private IEnumerable<Data.SqlColumn> RunResultSetQuery(string sql, params string[] parameters)
		{
			var query = "select * from sys.dm_exec_describe_first_result_set(@tsql,  @params,  @include_browse_information)";
			var paramStr = string.Join(", ", parameters);

			var command = new SqlCommand(query, _cnn) {CommandType = CommandType.Text}
				.AddParam("tsql", sql)
				.AddParam("params", paramStr)
				.AddParam("include_browse_information", (Int16)1);

			//TODO: Actual proper null checks
			using (var reader = command.ExecuteReader())
			{
				while (reader.Read()) yield return new Data.SqlColumn
				{
					Name = Convert.ToString(reader["name"]),
					Order = Convert.ToInt32(reader["column_ordinal"]),
					SQLType = (SqlDbType)Convert.ToInt32(reader["system_type_id"]),
					Nullable = Convert.ToBoolean(reader["is_nullable"]),
					Length = Convert.ToInt32(reader["max_length"]),
					Precision = Convert.ToInt32(reader["precision"]),
					Scale = Convert.ToInt32(reader["scale"])
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
