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

		public SqlQueryReader2012(string connectionString)
		{
			_cnn = new SqlConnection(connectionString);
			_cnn.Open();
		}

		public bool Validate(string query, out IList<SqlError> errors)
		{
			try
			{
				var result = runParameterQuery(query);
				errors = null;
				return true;
			}
			catch (SqlException ex)
			{
				errors = ex.Errors.Cast<SqlError>().Where(NotCompiler).ToList();
				return false;
			}
		}


		public IList<SqlParameterInfo> GetParametersAndValidate(string sql, out IList<SqlError> errors)
		{
			try
			{
				var result = runParameterQuery(sql);
				errors = null;
				return result.ToList();
			}
			catch (SqlException ex)
			{
				errors = ex.Errors.Cast<SqlError>().Where(NotCompiler).ToList();
				return null;
			}
		}

		const string sp_getParameters = "sys.sp_describe_undeclared_parameters";
		const string sp_getResultSet = "sys.dm_exec_describe_first_result_set";

		private IEnumerable<SqlParameterInfo> runParameterQuery(string sql)
		{
			var query = "sp_describe_undeclared_parameters";

			var command = new SqlCommand(query, _cnn);
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

		private bool NotCompiler(SqlError e) => e.Number != COMPILER_ERROR_CODE;
		

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
