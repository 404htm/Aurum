using System.Runtime.Serialization;

namespace Aurum.SQL.Data
{
    public class SqlColumn : SqlElement
    {
        [DataMember] public bool Identity { get; set; }
        [DataMember] public bool UniqueKey { get; set; }
        [DataMember] public bool Computed { get; set; }
        [DataMember] public string SourceSchema { get; set; }
        [DataMember] public string SourceColumn { get; set; }

        [DataMember] public bool IsUpdatable { get; set; }
        [DataMember] public bool IsComputed { get; internal set; }
    }
}
