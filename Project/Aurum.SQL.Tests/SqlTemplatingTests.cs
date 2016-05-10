using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Aurum.SQL.Tests
{
	[TestClass]
	public class SqlTemplatingTests
	{
		[TestMethod]
		public void BuildSelectQuery_SingleTemplate()
		{
			var templates = new List<SqlQueryTemplate>();
			templates.Add(new SqlQueryTemplate { Name = "SelectById", IsDestructive = false, AllowAutoSubquery = true, QueryText = "Select {columns} from [{schema}].[{table}] where {identity}" });

			var table1 = new SqlTableInfo { Name = "TestTable", Schema = "dbo" };
			table1.ColumnInfo.Add(new SqlColumnInfo() { ColumnId = 1, Name = "Id", Identity = true });
			table1.ColumnInfo.Add(new SqlColumnInfo() { ColumnId = 2, Name = "Name", Identity = false });
			table1.ColumnInfo.Add(new SqlColumnInfo() { ColumnId = 3, Name = "City", Identity = false });
			table1.ColumnInfo.Add(new SqlColumnInfo() { ColumnId = 4, Name = "State", Identity = false });
			table1.ColumnInfo.Add(new SqlColumnInfo() { ColumnId = 5, Name = "Zip", Identity = false });

			var materializer = new TemplateMaterializer(templates);
			var queryset = materializer.Build(table1);

			Assert.IsTrue(queryset.Any(), "No Queries were generated");
			Assert.AreEqual(1, queryset.Count, "Multiple queries were generated for a single template");

			var query = queryset.ElementAt(0);
			Assert.AreEqual(table1.Name, query.GroupName, "Groupname should default to tablename");
			Assert.AreEqual(templates[0].Name, query.Name);
			Assert.IsFalse(query.IsModified, "Query should be marked as unmodified");
			Assert.AreEqual("Select [Id], [Name], [City], [State], [Zip] from [dbo].[TestTable] where [Id] = @Id", query.Query);
		}
	}
}
