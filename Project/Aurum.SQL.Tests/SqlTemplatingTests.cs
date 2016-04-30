using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aurum.SQL.Tests
{
	[TestClass]
	public class SqlTemplatingTests
	{
		string _cstr_db;

		[TestInitialize]
		public void Init()
		{
			_cstr_db = Properties.Settings.Default.cnn_db;
		}

		//[TestMethod]
		//public void TestMethod1()
		//{
		//    using (var validator = new SqlValidator(_cstr_db))
		//    {
		//        validator.GetParametersAndValidate()
		//    }
		//}
	}
}
