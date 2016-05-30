using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Aurum.SQL.Templates;

namespace Aurum.SQL.Tests
{
	[TestClass]
	public class SqlTemplatingTests
	{
		//[TestMethod]
		//public void BuildSelectQuery_SingleID_StaticQueryName()
		//{
		//	var templates = new List<ISqlQueryTemplate>();
		//	Aurum.SQL.Fakes.


		//	templates.Add(new SqlQueryTemplate { Name = "SelectById", IsDestructive = false, AllowAutoSubquery = true, QueryText = "SELECT ${*|c=>[c]|, } FROM [{schema}].[{table}] WHERE (${identity|c=>[c] = @c| AND })" });

		//	var table1 = new SqlTableDetail { Name = "TestTable", Schema = "dbo" };
		//	table1.ColumnInfo.Add(new SqlColumnInfo() { ColumnId = 1, Name = "Id", Identity = true });
		//	table1.ColumnInfo.Add(new SqlColumnInfo() { ColumnId = 2, Name = "Name", Identity = false });
		//	table1.ColumnInfo.Add(new SqlColumnInfo() { ColumnId = 3, Name = "City", Identity = false });
		//	table1.ColumnInfo.Add(new SqlColumnInfo() { ColumnId = 4, Name = "State", Identity = false });
		//	table1.ColumnInfo.Add(new SqlColumnInfo() { ColumnId = 5, Name = "Zip", Identity = false });

		//	var materializer = new TemplateMaterializer(templates);
		//	var queryset = materializer.Build(table1);

		//	Assert.IsTrue(queryset.Any(), "No Queries were generated");
		//	Assert.AreEqual(1, queryset.Count, "Multiple queries were generated for a single template");

		//	var query = queryset.ElementAt(0);
		//	Assert.AreEqual(table1.Name, query.GroupName, "Groupname should default to tablename");
		//	Assert.AreEqual(templates[0].Name, query.Name);
		//	Assert.IsFalse(query.IsModified, "Query should be marked as unmodified");
		//	Assert.AreEqual("SELECT [Id], [Name], [City], [State], [Zip] FROM [dbo].[TestTable] WHERE ([Id] = @Id)", query.Query);
		//}

	//	[TestMethod]
	//	public void BuildSelectQuery_MultipleID_DynamicQueryName()
	//	{
	//		var templates = new List<ISqlQueryTemplate>();
	//		templates.Add(new SqlQueryTemplateData { Name = "Select{table}ById", IsDestructive = false, AllowAutoSubquery = true, QueryText = "SELECT ${!identity|column=>[column]|, } FROM [{schema}].[{table}] WHERE (${identity|c=>[c] = @c| AND })" });

	//		var table1 = new SqlTableDetail { Name = "TestTable", Schema = "dbo" };
	//		table1.ColumnInfo.Add(new SqlColumnInfo() { ColumnId = 1, Name = "Id1", Identity = true });
	//		table1.ColumnInfo.Add(new SqlColumnInfo() { ColumnId = 6, Name = "Id2", Identity = true });
	//		table1.ColumnInfo.Add(new SqlColumnInfo() { ColumnId = 2, Name = "Name", Identity = false });
	//		table1.ColumnInfo.Add(new SqlColumnInfo() { ColumnId = 3, Name = "City", Identity = false });
	//		table1.ColumnInfo.Add(new SqlColumnInfo() { ColumnId = 4, Name = "State", Identity = false });
	//		table1.ColumnInfo.Add(new SqlColumnInfo() { ColumnId = 5, Name = "Zip", Identity = false });

	//		var materializer = new TemplateMaterializer(templates);
	//		var queryset = materializer.Build(table1);

	//		Assert.IsTrue(queryset.Any(), "No Queries were generated");
	//		Assert.AreEqual(1, queryset.Count, "Multiple queries were generated for a single template");

	//		var query = queryset.ElementAt(0);
	//		Assert.AreEqual(table1.Name, query.GroupName, "Groupname should default to tablename");
	//		Assert.AreEqual("SelectTestTableById", query.Name);
	//		Assert.IsFalse(query.IsModified, "Query should be marked as unmodified");
	//		Assert.AreEqual("SELECT [Name], [City], [State], [Zip] FROM [dbo].[TestTable] WHERE ([Id1] = @Id1 AND [Id2] = @Id2)", query.Query);
	//	}


	}
}
