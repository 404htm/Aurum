using Aurum.Core.Parser;
using Aurum.SQL.Data;

using filter = System.Func<Aurum.SQL.Data.SqlTableDetail, bool>;
using query = System.Func<Aurum.SQL.Data.SqlTableDetail, string>;


namespace Aurum.SQL.Templates
{
    /// <summary> Template that can be applied to a Sql Table or View to produce a default SQL Query </summary>
    public class SqlQueryTemplate : SqlQueryTemplateData,  ISqlQueryTemplate
    {
        internal SqlQueryTemplate(SqlQueryTemplateData data, IParser<filter> filterParser, IParser<query> queryParser)
        {
            Name = data.Name;
            QueryName = data.Name;
            Description = data.Description;
            IsDestructive = data.IsDestructive;
            AllowAutoSubquery = data.AllowAutoSubquery;
            QueryText = data.QueryText;
            FilterText = data.FilterText;

            //TODO: ASYNC
            AppliesTo = string.IsNullOrEmpty(FilterText) ? (t) => true : filterParser.Parse(FilterText).Result;
            //GetQuery = queryParser.Parse(QueryText);
        }

        public filter AppliesTo { get; private set; }
        public query GetQuery { get; private set; }

    }
}
