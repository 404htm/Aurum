using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Integration.Tests.Temp
{
    public class GroupMetadata
    {
        public object Name { get; internal set; }
        public List<OperationMetadata> Operations { get; set; }
    }
}
