using System;
using System.Runtime.Serialization;

namespace Aurum.Core
{
    [DataContract]
    public class QueryDefinition : Storeable<QueryDefinition>
    {
        public QueryDefinition()
        {
            Id = Guid.NewGuid();
        }

        [DataMember] public Guid Id { get; set; }
        [DataMember] public string Name { get; set; }
        [DataMember] public string Description { get; set; }
        [DataMember] public string GroupName { get; set; }
        [DataMember] public bool IsModified { get; set; }
        [DataMember] public SourceType SourceType { get; set; }
        [DataMember] public string SourceName { get; set; }


    }
}
