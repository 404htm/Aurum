using Aurum.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.SQL
{
	[DataContract]
	public class SqlQueryTemplate
	{
		[DataMember] public string Name { get; set; }
		[DataMember] public string QueryName { get; set; }
		[DataMember] public string Description { get; set; }
		[DataMember] public List<string> RequiredColumns { get; set; }
		[DataMember] public bool IsDestructive { get; set; }
		[DataMember] public bool AllowAutoSubquery { get; set; }
		[DataMember] public string QueryText { get; set; }

		internal bool AppliesTo(SqlTableDetail table)
		{
			return true;
			//TODO: Make this actually work
		}
		//TODO: Output Types

	}
}
