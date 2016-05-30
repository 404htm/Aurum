using Aurum.Core.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using filter = System.Func<Aurum.SQL.SqlTableDetail, bool>;
using query = System.Func<Aurum.SQL.SqlTableDetail, string>;


namespace Aurum.SQL.Templates
{
	public class SqlQueryTemplate : SqlQueryTemplateData,  ISqlQueryTemplate
	{
		internal SqlQueryTemplate(SqlQueryTemplateData data, IExpressionParser<filter> filterParser, IExpressionParser<query> queryParser)
		{
			Name = data.Name;
			QueryName = data.Name;
			Description = data.Description;
			IsDestructive = data.IsDestructive;
			AllowAutoSubquery = data.AllowAutoSubquery;
			QueryText = data.QueryText;
			FilterText = data.FilterText;

			AppliesTo = string.IsNullOrEmpty(FilterText) ? (t) => true : filterParser.Parse(FilterText);
			//GetQuery = queryParser.Parse(QueryText);
		}

		public filter AppliesTo { get; private set; }
		public query GetQuery { get; private set; }

	}
}
