using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.SQL
{
	public class SqlLoader
	{
		public SqlLoader(ISqlSchemaReader reader)
		{
			

		}

		public SqlQueryDefinition Load(string query)
		{
			var result = new SqlQueryDefinition
			{
				Query = query
				
			};

			return result;
		}
	}
}
