using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Core
{
	[DataContract]
	[KnownType(typeof(SQLDataConnection))]
	public class Context:StoreableBase<Context>
	{
		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public Dictionary<Guid, IDataSource> DataSources { get; set; }

		[DataMember]
		public Dictionary<Guid, QueryDefinition> Queries { get; set; }

		[DataMember]
		public Dictionary<Guid, QueryTemplate> Templates { get; set; }


	}
}
