using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Core
{
    public abstract class QuerySet
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        public void RescanAll()
        {

        }
    }
}
