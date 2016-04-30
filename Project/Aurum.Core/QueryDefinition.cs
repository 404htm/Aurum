using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Core
{
	[DataContract]
	public class QueryDefinition : StoreableBase<QueryDefinition>
	{
		[DataMember]
		public Guid Id { get; set; }
		[DataMember]
		public Guid DataSourceID { get; set; }
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public bool IsModified { get; set; }
		[DataMember]
		public SourceType SourceType { get; set; }
		[DataMember]
		public string SourceName { get; set; }
		[DataMember]
		public TypeDefinition InputType { get; private set; }
		[DataMember]
		public TypeDefinition OutputType { get; private set; }

	}
}
