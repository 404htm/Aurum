using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using Ninject;

namespace Aurum.SQL.Tests
{
	[TestClass]
	public class SqlSchemaReaderTests
	{
		
		string _cstr_db;

		[TestInitialize]
		public void Init()
		{
			_cstr_db = TestHelpers.GetTestConnection();
		}

		[TestMethod]
		public void GetAllTablesWithSchema()
		{
			IList<string> tables;
			using (var reader = new SqlSchemaReader(_cstr_db))
			{
				tables = reader.GetTables(null).Select(t => t.ToString()).ToList();
			}

			Assert.IsTrue(tables.Contains("[dbo].[Customer]"), "dbo.Customer not found");
			Assert.IsTrue(tables.Contains("[dbo].[Order]"), "dbo.Order not found");
			Assert.IsTrue(tables.Contains("[inventory].[Item]"), "inventory.Item not found");
		}

		[TestMethod]
		public void GetdboTablesWithSchema()
		{
			IList<string> tables;
			using (var reader = new SqlSchemaReader(_cstr_db))
			{
				tables = reader.GetTables("dbo").Select(t => t.ToString()).ToList();
			}

			Assert.IsTrue(tables.Contains("[dbo].[Customer]"), "dbo.Customer not found");
			Assert.IsTrue(tables.Contains("[dbo].[Order]"), "dbo.Order not found");
			Assert.IsFalse(tables.Contains("[inventory].[Item]"), "inventory.Item not found");

		}

		[TestMethod]
		public void GetColumns()
		{
			IList<SqlColumnInfo> customer_columns;
			IList<SqlColumnInfo> order_columns;

			using (var reader = new SqlSchemaReader(_cstr_db))
			{
				customer_columns = reader.GetTableDetail(new SqlTableInfo { Schema = "dbo", Name = "customer" }).Columns;
				order_columns = reader.GetTableDetail(new SqlTableInfo { Schema = "dbo", Name = "order" }).Columns;
			}

			Assert.IsTrue(customer_columns.Any());
			Assert.IsTrue(order_columns.Any());

			var id_column = customer_columns.SingleOrDefault(c => c.Name == "Id");
			
			Assert.IsNotNull(id_column);
			Assert.IsTrue(id_column.Identity);
			Assert.IsFalse(id_column.Nullable);
			Assert.IsTrue(id_column.ColumnId == 1);

			var name_column = customer_columns.SingleOrDefault(c => c.Name == "FirstName");

			Assert.IsNotNull(name_column);
			Assert.IsFalse(name_column.Identity);
			Assert.IsTrue(name_column.Nullable);
			Assert.IsTrue(name_column.ColumnId == 3);
		}

	}
}
