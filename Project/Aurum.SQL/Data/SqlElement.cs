using System.Runtime.Serialization;

namespace Aurum.SQL.Data
{
    [DataContract]
    public class SqlElement
    {
        [DataMember] public string Name { get; set; }
        [DataMember] public SqlType SQLType { get; set; }
        [DataMember] public int Order { get; set; }
        [DataMember] public bool Nullable { get; set; }

        [DataMember] public int Length { get; set; }
        [DataMember] public int Precision { get; set; }
        [DataMember] public int Scale { get; internal set; }
    }
}
