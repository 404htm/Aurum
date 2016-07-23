using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aurum.SQL.Readers;
using System.Collections;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Aurum.SQL.Data;

namespace Aurum.SQL.Tests.Readers
{
	[TestClass]
	public class SqlQueryReader2012Tests : SQLTestBase
	{
		[ClassInitialize] public static void SetupTests(TestContext testContext) => Context = testContext;

		[TestMethod]
		public void TestGetParameters_GoodQuery()
		{
			using (var validator = new SqlQueryReader2012(TestHelpers.GetTestConnection()))
			{
				IList<SqlError> errors;
				var Parameters = validator.GetParameters("select * from Customer where FirstName = @name;", out errors);
				WriteErrors(errors);

				Assert.IsNull(errors);
				Assert.IsTrue(Parameters.Any());
				Assert.IsTrue(Parameters.Any(p => p.Name == "@name"));
			}
		}

		[TestMethod]
		public void TestGetParameters_BadQuery()
		{
			using (var reader = new SqlQueryReader2012(TestHelpers.GetTestConnection()))
			{
				IList<SqlError> errors;
				var Parameters = reader.GetParameters("select * from NotARealTable where FirstName = @name;", out errors);
				WriteErrors(errors);

				Assert.IsNotNull(errors);
				Assert.AreEqual(errors.Count(), 1);
				Assert.IsNull(Parameters);
			}
		}

		[TestMethod]
		public void TestValidate_GoodQuery()
		{
			using (var reader = new SqlQueryReader2012(TestHelpers.GetTestConnection()))
			{
				IList<SqlError> errors;
				var result = reader.Validate("select * from Customer where FirstName = @name;", out errors);
				WriteErrors(errors);

				Assert.IsNull(errors);
				Assert.IsTrue(result);
			}
		}

		[TestMethod]
		public void TestValidate_BadQuery()
		{
			using (var reader = new SqlQueryReader2012(TestHelpers.GetTestConnection()))
			{
				IList<SqlError> errors;
				var result = reader.Validate("select * from NotARealTable where FirstName = @name;", out errors);
				WriteErrors(errors);

				Assert.IsNotNull(errors);
				Assert.AreEqual(errors.Count(), 1);
				Assert.IsFalse(result);
			}
		}

		[TestMethod]
		public void TestGetResult_BasicQuery()
		{
			using (var validator = new SqlQueryReader2012(TestHelpers.GetTestConnection()))
			{
				IList<SqlError> errors;
				var results = validator.GetResultSet("select Id, Active, FirstName from Customer where FirstName = @name;", out errors, "@name varchar");
				WriteErrors(errors);
				
				Assert.IsNull(errors);
				Assert.IsTrue(results.Any(), "No Results Returned");

				var first = results.SingleOrDefault(λ => λ.Name == "FirstName");
				Assert.IsNotNull(first);
				Assert.AreEqual(false, first.Identity);
				Assert.AreEqual(100, first.Length);
				Assert.AreEqual(true, first.Nullable);
				Assert.AreEqual(3, first.Order);
				Assert.AreEqual("FirstName", first.SourceColumn);
				Assert.AreEqual(SqlType.VarChar, first.SQLType);
				Assert.AreEqual(false, first.UniqueKey);
				Assert.AreEqual(true, first.IsUpdatable);
				Assert.AreEqual(false, first.IsComputed);
			}
		}


	}
}
