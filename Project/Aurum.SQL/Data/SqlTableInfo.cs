namespace Aurum.SQL.Data
{
    public class SqlTableInfo
    {
        public string Name { get; set; }
        public string Schema { get; set; }
        public override string ToString() => $"[{Schema}].[{Name}]";
    }
}
