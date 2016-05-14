using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.SQL
{
	public class SqlTableDetail : SqlTableInfo
	{
		internal SqlTableDetail(SqlTableInfo tableInfo) : this()
		{
			this.Name = tableInfo.Name;
			this.Schema = tableInfo.Schema;
		}

		public SqlTableDetail()
		{
			ColumnInfo = new List<SqlColumnInfo>();
		}

		public IList<SqlColumnInfo> ColumnInfo { get; set; }
		
	}
}
