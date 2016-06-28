using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aurum.SQL.Tests.Readers
{
	[TestClass]
	public class SqlQueryReader : SQLTestBase
	{
		[ClassInitialize] public static void SetupTests(TestContext testContext) => Context = testContext;
		
		[TestMethod]
		public void TestMethod1()
		{
		}
	}
}
