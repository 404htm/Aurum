using Aurum.Core;
using System.Runtime.Serialization;

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
