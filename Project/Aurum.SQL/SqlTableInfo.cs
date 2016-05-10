using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.SQL
{
	public class SqlTableInfo
	{
		public SqlTableInfo()
		{
			ColumnInfo = new List<SqlColumnInfo>();
		}

		public string Name { get; set; }
		public string Schema { get; set; }
		public IList<SqlColumnInfo> ColumnInfo { get; set; }
		
	}
}
