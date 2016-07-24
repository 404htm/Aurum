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
	/// <summary>Provides metadata and validation for user queries - Relies on functions only available in SQLServer 2012+</summary>
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
				var cols = reader.GetColumnLookup();

				while (reader.Read()) yield return new Data.SqlParameter
				{
					Name = reader.GetString(cols["name"]),
					Order = reader.GetInt32(cols["parameter_ordinal"]),
					SQLType = reader.GetDbType(cols["suggested_system_type_id"]),
					Length = reader.GetInt16(cols["suggested_max_length"]),
					Precision = reader.GetByte(cols["suggested_precision"]),
					Scale = reader.GetByte(cols["suggested_scale"]),
					IsOutput = reader.GetBoolean(cols["suggested_is_output"]),
					IsInput = reader.GetBoolean(cols["suggested_is_input"]),
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
				var cols = reader.GetColumnLookup();

				while (reader.Read()) yield return new Data.SqlColumn
				{
					Name = reader.GetString(cols["name"]),
					Order = reader.GetInt32(cols["column_ordinal"]),
					SQLType = reader.GetDbType(cols["system_type_id"]),
					Nullable = reader.GetBoolean(cols["is_nullable"]),
					Length = reader.GetInt16(cols["max_length"]),
					Precision = reader.GetByte(cols["precision"]),
					Scale = reader.GetByte(cols["scale"]),
					SourceColumn = reader.GetString(cols["source_column"]),
					IsUpdatable = reader.GetBoolean(cols["is_updateable"]),
					IsComputed = reader.GetBoolean(cols["is_computed_column"])
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
