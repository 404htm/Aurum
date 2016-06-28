using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Aurum.SQL.Data;

namespace Aurum.SQL.Readers
{
	/// <summary>Provides Query Metadata for SQL2012 and Newer</summary>
	public class SqlQueryReader2012 : IDisposable, ISqlValidator
	{
		SqlConnection _cnn;
		const int COMPILER_ERROR_CODE = 11501;

		private bool NotCompiler(SqlError e) => e.Number != COMPILER_ERROR_CODE;
		const string sp_getParameters = "sys.sp_describe_undeclared_parameters";
		const string sp_getResultSet = "sys.dm_exec_describe_first_result_set";

		public SqlQueryReader2012(string connectionString)
		{
			_cnn = new SqlConnection(connectionString);
			_cnn.Open();
		}

		public bool Validate(string query, out IList<SqlError> errors)
		{
			return RunAndGetErrors(() => RunParameterQuery(query), out errors) != null;
		}

		public IList<SqlParameterInfo> GetParameters(string query, out IList<SqlError> errors)
		{
			return RunAndGetErrors(() => RunParameterQuery(query).ToList(), out errors);
		}

		public IList<SqlParameterInfo> GetResultSet(string query, out IList<SqlError> errors)
		{
			return RunAndGetErrors(() => RunResultSetQuery(query).ToList(), out errors);
		}



		private T RunAndGetErrors<T>(Func<T> operation, out IList<SqlError> errors)
		{
			try
			{
				var result = operation();
				errors = null;
				return result;
			}
			catch (SqlException ex)
			{
				errors = ex.Errors.Cast<SqlError>().Where(NotCompiler).ToList();
				return default(T);
			}
		}

		private IEnumerable<SqlParameterInfo> RunResultSetQuery(string sql)
		{
			var command = new SqlCommand(sp_getResultSet, _cnn);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.AddWithValue("tsql", sql);

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

		private IEnumerable<SqlParameterInfo> RunParameterQuery(string sql)
		{
			var command = new SqlCommand(sp_getParameters, _cnn);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.AddWithValue("tsql", sql);

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
