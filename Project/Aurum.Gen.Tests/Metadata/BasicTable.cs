using System.Collections.Generic;

namespace Aurum.Gen.Tests.Metadata
{
    public class BasicTable
    {
        public string Name { get; set; }
        public List<BasicColumn> Columns { get; set; }

        public BasicTable()
        {
            Columns = new List<BasicColumn>();
        }
    }

    public class BasicColumn
    {
        public string DisplayName { get; set; }
        public string ColumnName { get; set; }
        public string TypeName { get; set; }
    }
}
