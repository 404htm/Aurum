using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.SQL.Readers
{
	public class SqlQueryReaderLegacy
	{
		/// <summary>
		/// Parse SQL for Errors - Does not handle parameters
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
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
			catch (SqlException)
			{
				return false;
			}
		}
	}
}
