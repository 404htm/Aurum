using Aurum.Core.Parser;
using Aurum.SQL.Data;
using Aurum.SQL.Templates;

using filter = System.Func<Aurum.SQL.Data.SqlTableDetail, bool>;
using query = System.Func<Aurum.SQL.Data.SqlTableDetail, string>;

namespace Aurum.SQL
{
    /// <summary> Helper class that compiles SqlQueryTemplateData (serializable) into a usable SqlQueryTemplate </summary>
    internal class SqlQueryTemplateHydrator : ISqlQueryTemplateHydrator
    {
        IExpressionParser<filter> _filterParser;
        IExpressionParser<query> _queryParser;

        public SqlQueryTemplateHydrator(IParserFactory parserfactory)
        {
            _filterParser = parserfactory.Create<filter>();
            _filterParser.Register<SqlTableDetail>();
            _filterParser.Import("Aurum.SQL");
            _filterParser.Import("System.Linq");

            //_queryParser = parserfactory.Create<query>();
            //_queryParser.Register<SqlTableDetail>();
            //_queryParser.Import("Aurum.SQL");
            
        }

        public ISqlQueryTemplate Hydrate(SqlQueryTemplateData data)
        {
            return new SqlQueryTemplate(data, _filterParser, _queryParser);
        }


    }
}
