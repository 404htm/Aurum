using System.Collections.Generic;

namespace Aurum.Integration.Tests.Temp
{
    /// <summary>
    /// This roughly correlates to a sql table - includes a schema, set of operations, and a set of sources
    /// </summary>
    public class GroupMetadata
    {
        public object Name { get; internal set; }
        public List<OperationMetadata> Operations { get; set; }
        public List<ObjectMetadata> Objects {get; set;}
        public List<SourceMetadata> Sources {get; set;}
    }
}
