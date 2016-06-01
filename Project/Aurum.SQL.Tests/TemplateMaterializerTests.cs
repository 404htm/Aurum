using Aurum.SQL.Templates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aurum.SQL.Tests
{
	[TestClass]
	public class TemplateMaterializerTests
	{
		[TestInitialize]
		public void Init()
		{

		}

		#region Stubs
		private ISqlQueryTemplate StubTemplate_SelectById()
		{
			var mock =  new Mock<ISqlQueryTemplate>();
			mock.Setup(λ => λ.Name).Returns("SelectById");
			mock.Setup(λ => λ.IsDestructive).Returns(false);
			mock.Setup(λ => λ.QueryText).Returns("SELECT ${*|c=>[c]|, } FROM [{schema}].[{table}] WHERE (${identity|c=>[c] = @c| AND })");
			mock.Setup(λ => λ.AppliesTo).Returns(() => t => t.Columns.Any(c => c.Identity));
			return mock.Object;
		}

		private SqlTableDetail StubTable_SingleId()
		{
			var table = new SqlTableDetail { Name = "TestTable", Schema = "dbo" };
			table.Columns.Add(new SqlColumnInfo() { ColumnId = 1, Name = "Id", Identity = true });
			table.Columns.Add(new SqlColumnInfo() { ColumnId = 2, Name = "Name", Identity = false });
			table.Columns.Add(new SqlColumnInfo() { ColumnId = 3, Name = "City", Identity = false });
			table.Columns.Add(new SqlColumnInfo() { ColumnId = 4, Name = "State", Identity = false });
			table.Columns.Add(new SqlColumnInfo() { ColumnId = 5, Name = "Zip", Identity = false });
			return table;
		}

		private SqlTableDetail StubTable_CompoundId()
		{
			var table = new SqlTableDetail { Name = "CrossRefTable", Schema = "dbo" };
			table.Columns.Add(new SqlColumnInfo() { ColumnId = 1, Name = "Id1", Identity = true });
			table.Columns.Add(new SqlColumnInfo() { ColumnId = 6, Name = "Id2", Identity = true });
			table.Columns.Add(new SqlColumnInfo() { ColumnId = 2, Name = "Name", Identity = false });
			table.Columns.Add(new SqlColumnInfo() { ColumnId = 3, Name = "City", Identity = false });
			table.Columns.Add(new SqlColumnInfo() { ColumnId = 4, Name = "State", Identity = false });
			table.Columns.Add(new SqlColumnInfo() { ColumnId = 5, Name = "Zip", Identity = false });
			return table;
		}

		private SqlTableDetail StubTable_NoId()
		{
			var table = new SqlTableDetail { Name = "NoIdentTable", Schema = "dbo" };
			table.Columns.Add(new SqlColumnInfo() { ColumnId = 1, Name = "ColumnA", Identity = false });
			table.Columns.Add(new SqlColumnInfo() { ColumnId = 2, Name = "ColumnB", Identity = false });
			table.Columns.Add(new SqlColumnInfo() { ColumnId = 3, Name = "ColumnC", Identity = false });
			return table;
		}

		#endregion

		[TestMethod]
		public void Build_SelectQuery_CheckQueryInfo()
		{
			var templates = new List<ISqlQueryTemplate>() { StubTemplate_SelectById() };
			var table = StubTable_SingleId();
			var materializer = new TemplateMaterializer(templates);

			var queryset = materializer.Build(table);
			Assert.IsTrue(queryset.Any(), "No Queries were generated");
			Assert.AreEqual(1, queryset.Count, "Multiple Queries should not be generated for a single template");

			var query = queryset.ElementAt(0);
			Assert.AreEqual(table.Name, query.GroupName, "GroupName should default to TableName");
			Assert.AreEqual(templates[0].Name, query.Name);
			Assert.IsFalse(query.IsModified, "Query should be marked as unmodified");
		}

		[TestMethod]
		public void Build_SelectQuery_TableWithSingleId()
		{
			var templates = new List<ISqlQueryTemplate>() { StubTemplate_SelectById() };
			var table = StubTable_SingleId();
			var materializer = new TemplateMaterializer(templates);

			var queryset = materializer.Build(table);
			var query = queryset[0];
			Assert.AreEqual("SELECT [Id], [Name], [City], [State], [Zip] FROM [dbo].[TestTable] WHERE ([Id] = @Id)", query.Query);
		}

		[TestMethod]
		public void Build_SelectQuery_TableWithMultipleIds()
		{
			var templates = new List<ISqlQueryTemplate>() { StubTemplate_SelectById() };
			var table = StubTable_CompoundId();
			var materializer = new TemplateMaterializer(templates);

			var queryset = materializer.Build(table);
			var query = queryset.ElementAt(0);
			Assert.AreEqual("SELECT [Id1], [Id2], [Name], [City], [State], [Zip] FROM [dbo].[CrossRefTable] WHERE ([Id1] = @Id1 AND [Id2] = @Id2)", query.Query);
		}

		[TestMethod]
		public void Build_SelectQuery_QueryIsNotApplicable()
		{
			var templates = new List<ISqlQueryTemplate>() { StubTemplate_SelectById() };
			var table = StubTable_NoId();
			var materializer = new TemplateMaterializer(templates);

			var queryset = materializer.Build(table);
			Assert.AreEqual(0, queryset.Count(), "Template does not apply - No query should have been generated");
		}
	}
}
