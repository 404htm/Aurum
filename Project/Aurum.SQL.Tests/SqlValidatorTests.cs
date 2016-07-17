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
using Aurum.SQL.Readers;

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

		//[TestMethod]
		//public void TestParseSimple()
		//{
		//	using (var validator = new SqlQueryReader2012(_cstr_db))
		//	{ 
		//		Assert.IsTrue(validator.ParseSQLBasic("select * from Customers;"));
		//		Assert.IsFalse(validator.ParseSQLBasic("select  from Customers;"));
		//	}

		//}



	}
}
