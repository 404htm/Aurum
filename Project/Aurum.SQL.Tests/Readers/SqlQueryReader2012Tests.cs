using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aurum.SQL.Readers;
using System.Collections;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

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

				Assert.IsNotNull(errors);
				Assert.AreEqual(errors.Count(), 1);
				Assert.IsNull(Parameters);

				foreach (var error in errors) Context.WriteLine($"{error.Number} - {error.Message}");
			}
		}

		[TestMethod]
		public void TestValidate_GoodQuery()
		{
			using (var reader = new SqlQueryReader2012(TestHelpers.GetTestConnection()))
			{
				IList<SqlError> errors;
				var result = reader.Validate("select * from Customer where FirstName = @name;", out errors);

				Assert.IsNull(errors);
				Assert.IsTrue(result);

				if (errors?.Any() ?? false) foreach (var e in errors) Context.WriteLine(e.Message);
			}
		}

		[TestMethod]
		public void TestValidate_BadQuery()
		{
			using (var reader = new SqlQueryReader2012(TestHelpers.GetTestConnection()))
			{
				IList<SqlError> errors;
				var result = reader.Validate("select * from NotARealTable where FirstName = @name;", out errors);

				Assert.IsNotNull(errors);
				Assert.AreEqual(errors.Count(), 1);
				Assert.IsFalse(result);

				foreach (var error in errors) Context.WriteLine($"{error.Number} - {error.Message}");
			}
		}

		[TestMethod]
		public void TestGetResult_GoodQuery()
		{
			using (var validator = new SqlQueryReader2012(TestHelpers.GetTestConnection()))
			{
				IList<SqlError> errors;
				var results = validator.GetResultSet("select Id, Active, FirstName from Customer where FirstName = @name;", out errors, "@name varchar");

				if (errors?.Any() ?? false) foreach (var e in errors) Context.WriteLine(e.Message);
				Assert.IsNull(errors);
				Assert.IsTrue(results.Any(), "No Results Returned");
				Assert.IsTrue(results.Any(p => p.Name == "FirstName"), "FirstName column not found");

				
			}
		}

	}
}
