using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lookup = System.Collections.Generic.Dictionary<string, string>;

namespace Aurum.SQL
{
	public class TemplateMaterializer
	{
		IList<SqlQueryTemplate> _templates;
		public TemplateMaterializer(IList<SqlQueryTemplate> templates)
		{
			_templates = templates;
		}

		public List<SqlQueryDefinition> Build(SqlTableInfo table)
		{
			var lookup = BuildStrings(table);

			return _templates
				.Where(t => t.AppliesTo(table))
				.Select(t => Apply(t, lookup))
				.ToList();
		} 

		SqlQueryDefinition Apply(SqlQueryTemplate template, Lookup lookup)
		{
			var def = new SqlQueryDefinition();
			

			def.Name = template.Name;
			def.Query = Replace(template.QueryText, lookup);


			return def;
		}

		string Replace(string input, Lookup lookup)
		{
			var sb = new StringBuilder(input);
			foreach (var set in lookup) sb.Replace(set.Key, set.Value);
			return sb.ToString();
		}

		Lookup BuildStrings(SqlTableInfo table)
		{
			var lookup = new Dictionary<string, string>();
			lookup["{schema}"] = table.Schema;
			lookup["{table}"] = table.Name;
			lookup["{identity}"] = GetIdentity(table);
			lookup["{columns}"] = GetColumns(table, (c) => true);
			lookup["{nonident_columns}"] = GetColumns(table, (c) => !c.Identity);

			return lookup;
		}

		private string GetIdentity(SqlTableInfo table)
		{
			return table.ColumnInfo
			.Where(c => c.Identity)
			.Select(c => $"[{c.Name}] = @{c.Name}")
			.DefaultIfEmpty()
			.Aggregate((a, b) => $"{a} AND {b}");
		}


		private string GetColumns(SqlTableInfo table, Func<SqlColumnInfo, bool> condition)
		{
			return table.ColumnInfo
			.DefaultIfEmpty()
			.Select(c => $"[{c.Name}]")
			.Aggregate((a, b) => $"{a}, {b}");
		}



	}
}
