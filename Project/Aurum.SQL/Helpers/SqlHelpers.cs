using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Aurum.SQL.Helpers.SqlHelpers;

namespace Aurum.SQL.Helpers
{
	internal static class SqlHelpers
	{
		const int COMPILER_ERROR_CODE = 11501;
		private static bool NotAPointlessCompilerError(SqlError e) => e.Number != COMPILER_ERROR_CODE;

		public static T RunAndGetErrors<T>(Func<T> operation, out IList<SqlError> errors)
		{
			try
			{
				var result = operation();
				errors = null;
				return result;
			}
			catch (SqlException ex)
			{
				errors = ex.Errors.Cast<SqlError>().Where(NotAPointlessCompilerError).ToList();
				return default(T);
			}
		}

		public static SqlCommand AddParam<T>(this SqlCommand command, string name, T value)
		{
			var type = SqlTypeMap.Get(typeof(T));
			var param = new SqlParameter(name, type) { Value = value };
			command.Parameters.Add(param);
			return command;
		}
	}
}
