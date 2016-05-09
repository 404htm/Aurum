using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace Aurum.SQL.Tests
{
	[TestClass]
	public class SqlValidatorTests
	{
		string _cstr_db;

		[TestInitialize]
		public void Init()
		{
			_cstr_db = TestHelpers.GetTestConnection();
		}

		[TestMethod]
		public void TestParseSimple()
		{
			using (var validator = new SqlValidator(_cstr_db))
			{ 
				Assert.IsTrue(validator.ParseSQLBasic("select * from Customers;"));
				Assert.IsFalse(validator.ParseSQLBasic("select  from Customers;"));
			}

		}

		[TestMethod]
		public void TestGetParametersAndValidate_GoodQuery()
		{
			using (var validator = new SqlValidator(_cstr_db))
			{
				IList<SqlError> errors;
				var Parameters = validator.GetParametersAndValidate("select * from Customer where FirstName = @name;", out errors);

				Assert.IsNull(errors);
				Assert.IsTrue(Parameters.Any());
				Assert.IsTrue(Parameters.Any(p => p.Name == "@name"));
			}
		}

		[TestMethod]
		public void TestGetParametersAndValidate_BadQuery()
		{
			using (var validator = new SqlValidator(_cstr_db))
			{
				IList<SqlError> errors;
				var Parameters = validator.GetParametersAndValidate("select * from NotARealTable where FirstName = @name;", out errors);

				Assert.IsNotNull(errors);
				Assert.AreEqual(errors.Count(), 1);
				Assert.IsNull(Parameters);

				foreach (var error in errors) Debug.WriteLine(error);
			}
		}

	}
}
