using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Core
{
	[DataContract]
	public class SQLDataConnection : IDataConnection
	{
		[DataMember]
		public Guid Id { get; set; }
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public string ConnectionString { get; set; }
		[DataMember]
		public string ConnectionStringName { get; set; }
	}
}
