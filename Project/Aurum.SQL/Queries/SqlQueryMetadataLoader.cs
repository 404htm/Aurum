using Aurum.SQL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.SQL.Queries
{
	public class SqlQueryMetadataLoader
	{
		ISqlValidator _validator;

		public SqlQueryMetadataLoader(ISqlValidator validator)
		{
			_validator = validator;
		}

		public SqlQueryDetail LoadQueryDetails(SqlQueryDefinition queryDefinition)
		{
			var detail = SqlQueryDetail.MapFrom(queryDefinition);
			
			return detail;
		}
	}
}
