using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Aurum.SQL
{
	public class SqlValidator : IDisposable
	{
		SqlConnection _cnn;

		public SqlValidator(string connectionString)
		{
			_cnn = new SqlConnection(connectionString);
			_cnn.Open();
		}

		/// <summary>
		/// Parse SQL for Errors - Does not handle parameters
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		public bool ParseSQLBasic(string sql)
		{
			var query = new StringBuilder();
			query.AppendLine("SET NOEXEC ON;");
			query.AppendLine(sql);
			query.AppendLine("SET NOEXEC OFF;");

			var command = new SqlCommand(query.ToString(), _cnn);
			command.CommandType = CommandType.Text;
			try
			{
				command.ExecuteNonQuery();
				return true;
			}
			catch(SqlException ex)
			{
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
				//Eliminate an extraneous compilier error for clarity
				errors = ex.Errors.Cast<SqlError>().Where(e => e.Number == 11501).ToList();
				return null;
			}
		}

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
