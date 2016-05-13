﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using filter = System.Func<Aurum.SQL.SqlColumnInfo, bool>;
using Lookup = System.Collections.Generic.Dictionary<string, string>;

namespace Aurum.SQL
{
	public class TemplateMaterializer
	{
		IList<SqlQueryTemplate> _templates;
		static readonly IDictionary<string, filter> _filters = new Dictionary<string, filter>
		{
			{"all", λ => true },
			{"*", λ => true },
			{"identity", λ => λ.Identity},
			{"!identity", λ => !λ.Identity}
			//TODO: Defaulted/Autogenerated Columns
			//TODO: Contains
			//TODO: Type
			
		};
		
		public TemplateMaterializer(IList<SqlQueryTemplate> templates)
		{
			_templates = templates;
		}

		public List<SqlQueryDefinition> Build(SqlTableInfo table)
		{
			var lookup = buildLookup(table);

			return _templates
				.Where(t => t.AppliesTo(table))
				.Select(t => Apply(t, table, lookup))
				.ToList();
		} 

		SqlQueryDefinition Apply(SqlQueryTemplate template, SqlTableInfo table, Lookup lookup)
		{
			var def = new SqlQueryDefinition();
			def.GroupName = table.Name;
			def.SourceName = table.Name;
			def.IsModified = false;

			def.Name = replace(template.QueryName??template.Name, lookup);
			var query = replace(template.QueryText, lookup);
			def.Query = parse(query, table.ColumnInfo);

			return def;
		}

		string replace(string input, Lookup lookup)
		{
			var sb = new StringBuilder(input);
			foreach (var set in lookup) sb.Replace(set.Key, set.Value);
			return sb.ToString();
		}

		string parse(string query, IEnumerable<SqlColumnInfo> columns)
		{
			var pattern = @"\$\{(?<s>[^|}]*)\|(?<e>[^|}]*)\|(?<d>[^|}]*)\}";
			Func<Match, string, string> get = (match, key) => match.Groups[key].Value;

			var substitutions = Regex.Matches(query, pattern)
				.Cast<Match>()
				.Select(λ => new { Expr = buildExpression(columns, get(λ, "s"), get(λ, "e"), get(λ, "d")), λ.Index, λ.Length})
				.ToList();

			var sb = new StringBuilder();
			int seg_start = 0;

			foreach(var sub in substitutions)
			{
				sb.Append(query.Substring(seg_start, sub.Index - seg_start));
				sb.Append(sub.Expr);
				seg_start = sub.Index + sub.Length;
			}
			sb.Append(query.Substring(seg_start));

			return sb.ToString();
		}

		string buildExpression(IEnumerable<SqlColumnInfo> columns, string set, string expression, string delimiter)
		{
			var split = expression.Split(new string[]{"=>"}, StringSplitOptions.None);

			if (split.Count() != 2) throw new InvalidOperationException("Invalid expression syntax");
			if (!_filters.ContainsKey(set)) throw new InvalidOperationException($@"""{set}"" is not a valid set name");

			var filter = _filters[set];
			var @var = split[0];
			var expr = split[1];

			var result = columns
			.Where(filter)
			.Select(λ => expr.Replace(@var, λ.Name))
			.DefaultIfEmpty()
			.Aggregate((λ1, λ2) => $"{λ1}{delimiter}{λ2}");

			return result;
		}

		Lookup buildLookup(SqlTableInfo table)
		{
			var lookup = new Dictionary<string, string>();
			lookup["{schema}"] = table.Schema;
			lookup["{table}"] = table.Name;
			return lookup;
		}

	}
}
