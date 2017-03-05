using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Aurum.Core
{
    [DataContract]
    [KnownType(typeof(SqlDataConnection))]
    public class Context:Storeable<Context>
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
