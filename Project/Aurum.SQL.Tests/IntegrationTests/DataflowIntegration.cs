using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Aurum.Core;
using Aurum.SQL.Templates;
using Aurum.Core.Parser;

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
		public void _INTEGRATION_BuildTemplatesFromTablesAndValidate()
		{
			var timer_str = "Timer";
			var cnn = TestHelpers.GetTestConnection();
			var parserFactory = new ParserFactory();

			IList<SqlTableDetail> details;
			using (var reader = new SqlSchemaReader(cnn))
			{
				var tables = reader.GetTables();

				this.TestContext.WriteLine($"Integration Results: {tables.Count()} tables found.");
				Assert.IsTrue(tables.Count > 0, "Tables not retrieved from connection");

				details = tables.Select(t => reader.GetTableDetail(t)).ToList();
				Assert.IsTrue(details.Count() > 0, "Details not retrieved from connection");
			}

			IList<SqlQueryTemplateData> templateData;
			using (var str = Resources.GetDefaultTemplates())
			{
				templateData = StoreableSet<SqlQueryTemplateData>.Load(str);
			}
			Assert.IsTrue(templateData.Any(), "Templates could not be loaded from assembly resources");

			var hydrator  = new SqlQueryTemplateHydrator(parserFactory);
			var templates = templateData.Select(hydrator.Hydrate).ToList();

			var builder = new TemplateMaterializer(templates);
			var query_sets = details.Select(λ => new { Table = λ.Name, Queries = builder.Build(λ) }).ToList();

			int query_count = query_sets.SelectMany(q => q.Queries).Count();
			int failed_count = 0;

			using (var validator = new SqlValidator(cnn))
			{
				foreach (var table in query_sets) foreach (var query in table.Queries)
				{
					this.TestContext.WriteLine($"Validating {table.Table} - {query.Name}: {query.Query}".Escape());
					
					IList<System.Data.SqlClient.SqlError> errors;
					validator.GetParametersAndValidate(query.Query, out errors);
					if (errors != null)
					{
						foreach (var e in errors) TestContext.WriteLine($"\tError: {e.Message}");
						failed_count++;
					}
				}
				Assert.IsTrue(failed_count == 0, $"{failed_count}/{query_count} Queries Failed Validation - See output for details.");

			}

		}
	}
}
