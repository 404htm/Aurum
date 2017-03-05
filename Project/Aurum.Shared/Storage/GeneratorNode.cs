using System;
using System.Collections.Generic;

namespace Aurum.Shared.Storage
{
    public class GeneratorNode
    {
        public Guid Id { get; set; }
        public Guid ContextId { get; set; }
        public List<Partition> Partitions { get; set; }


    }
}
