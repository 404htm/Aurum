using Aurum.Core;
using Aurum.Core.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

/// <summary>Backing data for SqlTemplates - Describes a compilable query and filter that can be used to generate SQL queries against multiple tables</summary>
namespace Aurum.SQL.Data
{
	[DataContract(Name = "SqlQueryTemplate")]
	public class SqlQueryTemplateData
	{
		[DataMember] public string Name { get; set; }
		[DataMember] public string QueryName { get; set; }
		[DataMember] public string Description { get; set; }
		[DataMember] public bool IsDestructive { get; set; }
		[DataMember] public bool AllowAutoSubquery { get; set; }
		[DataMember] public string QueryText { get; set; }
		[DataMember] public string FilterText { get; set; }
	}
}
