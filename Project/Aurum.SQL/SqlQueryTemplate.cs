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
		[DataMember] public string Description { get; set; }
		[DataMember] public List<string> RequiredColumns { get; set; }
		[DataMember] public bool IsDestructive { get; set; }
		[DataMember] public bool AllowRecursion { get; set; }
		[DataMember] public string QueryText { get; set; }

		//TODO: SubQuery Settings
		//TODO: Output Types

	}
}
