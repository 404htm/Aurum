using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Core
{
    public class QueryTemplate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDestructive { get; set; }
        public List<Guid> AppliesToTags { get; set; }
        public List<Guid> AppliesToSources { get; set; }
    }
}
