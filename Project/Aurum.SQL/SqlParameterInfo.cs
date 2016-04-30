using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.SQL
{
	public class SqlParameterInfo
	{
		public string Name { get; set; }
		public string SQLType { get; set; }
		public int ColumnId { get; set; }
		public bool Nullable { get; set; }
		public bool Identity { get; set; }
	}
}
