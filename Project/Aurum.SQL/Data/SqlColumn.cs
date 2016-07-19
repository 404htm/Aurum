using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.SQL.Data
{
	public class SqlColumn : SqlElement
	{
		[DataMember] public bool Identity { get; set; }
		[DataMember] public bool UniqueKey { get; set; }
		[DataMember] public bool Updatable { get; set; }
		[DataMember] public bool Computed { get; set; }
		
		[DataMember] public string SourceSchema { get; set; }
		[DataMember] public string SourceColumn { get; set; }
	}
}
