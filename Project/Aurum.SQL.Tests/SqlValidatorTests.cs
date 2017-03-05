using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aurum.SQL.Tests
{
    [TestClass]
    public class SqlValidatorTests
    {
        string _cstr_db;

        [TestInitialize]
        public void Init()
        {
            _cstr_db = SQLTestHelpers.GetTestConnection();
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
