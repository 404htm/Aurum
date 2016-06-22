using Aurum.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.SQL.Data
{
	[DataContract]
	public class SqlQueryDefinition : QueryDefinition
	{
		public SqlQueryDefinition() : base()
		{

		}

		[DataMember] public string Query { get; set; }


	}
}
