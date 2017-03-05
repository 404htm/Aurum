using Aurum.SQL.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Aurum.SQL.Helpers
{
    internal static class SqlHelpers
    {
        const int COMPILER_ERROR_CODE = 11501;
        private static bool NotAPointlessCompilerError(System.Data.SqlClient.SqlError e) => e.Number != COMPILER_ERROR_CODE;

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
            var param = new System.Data.SqlClient.SqlParameter(name, type) { Value = value };
            command.Parameters.Add(param);
            return command;
        }

        public static Dictionary<string, int> GetColumnLookup(this SqlDataReader reader)
        {
            return Enumerable.Range(0, reader.FieldCount)
                .ToDictionary(λ => reader.GetName(λ), λ => λ);
        }

        public static SqlType GetDbType(this SqlDataReader reader, int index) => (SqlType)reader.GetInt32(index);
    }
}
