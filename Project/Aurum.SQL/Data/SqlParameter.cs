namespace Aurum.SQL.Data
{
    public class SqlParameter : SqlElement
    {
        public bool IsInput { get; internal set; }
        public bool IsOutput { get; internal set; }
    }
}
