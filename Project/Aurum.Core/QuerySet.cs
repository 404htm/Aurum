using System;

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
