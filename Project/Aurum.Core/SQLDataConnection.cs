using System;
using System.Runtime.Serialization;

namespace Aurum.Core
{
    [DataContract]
    public class SqlDataConnection : IDataConnection
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
