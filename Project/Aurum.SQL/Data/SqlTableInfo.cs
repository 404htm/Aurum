using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.SQL.Data
{
	public class SqlTableInfo
	{
		public string Name { get; set; }
		public string Schema { get; set; }
		public override string ToString() => $"[{Schema}].[{Name}]";
	}
}
