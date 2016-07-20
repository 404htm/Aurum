using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.SQL.Data
{
	public class SqlParameter : SqlElement
	{
		public bool IsInput { get; internal set; }
		public bool IsOutput { get; internal set; }
	}
}
