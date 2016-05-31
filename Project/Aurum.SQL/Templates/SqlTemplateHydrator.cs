using Aurum.Core.Parser;
using Aurum.SQL.Templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using filter = System.Func<Aurum.SQL.SqlTableDetail, bool>;
using query = System.Func<Aurum.SQL.SqlTableDetail, string>;

namespace Aurum.SQL
{
	public class SqlQueryTemplateHydrator : ISqlQueryTemplateHydrator
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
