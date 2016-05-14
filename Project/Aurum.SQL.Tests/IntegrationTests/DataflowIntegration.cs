using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Aurum.SQL.Tests.IntegrationTests
{
	/// <summary>
	/// Summary description for DataflowTest
	/// </summary>
	[TestClass]
	public class DataflowIntegration
	{
		public DataflowIntegration()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region Additional test attributes
		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		[TestMethod]
		public void BuildFromTables()
		{
			var timer_str = "Timer";
			var cnn = TestHelpers.GetTestConnection();

			using (var reader = new SqlSchemaReader(cnn))
			{
				var tables = reader.GetTables();

				this.TestContext.WriteLine($"Integration Results: {tables.Count()} tables found.");
				Assert.IsTrue(tables.Count > 0, "Tables not retrieved from connection");

				var details = tables.Select(t => reader.GetTableDetail(t));
				Assert.IsTrue(details.Count() > 0, "Details not retrieved from connection");

				//tables.Select(λ => λ.);

			}





		}
	}
}
