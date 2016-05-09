using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

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
				tables = reader.GetTables(null);
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
				tables = reader.GetTables("dbo");
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
				customer_columns = reader.GetColumns("dbo.customer");
				order_columns = reader.GetColumns("[dbo].[ORDER]");
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
