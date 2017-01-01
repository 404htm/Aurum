using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.SQL.Data
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
            Columns = new List<SqlColumn>();
        }

        public IList<SqlColumn> Columns { get; set; }
        
    }
}
