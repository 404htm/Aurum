using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Aurum.Core;
using Aurum.SQL.Templates;
using Aurum.Core.Parser;
using Ninject;

namespace Aurum.SQL.Tests.IntegrationTests
{
	[TestClass]
	public class DefaultTemplateIntegration
	{
		static StandardKernel IOC;
		static TestContext Context;

		[ClassInitialize]
		public static void SetupTests(TestContext testContext)
		{
			Context = testContext;

			IOC = new StandardKernel();
			IOC.Bind<IParserFactory>().To<ParserFactory>();
			IOC.Bind<ISqlQueryTemplateHydrator>().To<SqlQueryTemplateHydrator>();
			IOC.Bind<ISqlValidator>().ToMethod(c => new SqlValidator(TestHelpers.GetTestConnection()));
			IOC.Bind<ISqlSchemaReader>().ToMethod(c => new SqlSchemaReader(TestHelpers.GetTestConnection()));
			IOC.Bind<IList<SqlQueryTemplateData>>().ToMethod(c => StoreableSet<SqlQueryTemplateData>.Load(Resources.GetDefaultTemplates()));
		}

		[TestMethod]
		public void Integration_DefaultTemplates()
		{
			//Step 1: Read metadata from test database
			IList<SqlTableDetail> details;
			using (var reader = IOC.Get<ISqlSchemaReader>())
			{
				var tables = reader.GetTables();
				details = tables.Select(t => reader.GetTableDetail(t)).ToList();

				Context.WriteLine($"Integration Results: {tables.Count()} tables found.");
				Assert.IsTrue(tables.Any(), "Tables not retrieved from connection");
				Assert.IsTrue(details.Any(), "Details not retrieved from connection");
			}

			//Step 2: Read default queries and parse
			var rawTemplates = IOC.Get<IList<SqlQueryTemplateData>>();
			Assert.IsTrue(rawTemplates.Any(), "Templates could not be loaded from assembly resources");

			var hydrator = IOC.Get<ISqlQueryTemplateHydrator>();
			var templates = rawTemplates.Select(hydrator.Hydrate).ToList();

			//Step 3: Materialize Templates
			var builder = new TemplateMaterializer(templates);
			var query_sets = details.Select(λ => new { Table = λ.Name, Queries = builder.Build(λ) }).ToList();

			int query_count = query_sets.SelectMany(q => q.Queries).Count();
			int failed_count = 0;

			//Step 4: Validate SQL
			using (var validator = IOC.Get<ISqlValidator>())
			{
				foreach (var table in query_sets) foreach (var query in table.Queries)
				{
					Context.WriteLine($"Validating {table.Table} - {query.Name}: {query.Query}".Escape());
					IList<System.Data.SqlClient.SqlError> errors;
					validator.GetParametersAndValidate(query.Query, out errors);

					if (errors != null)
					{
						foreach (var e in errors) Context.WriteLine($"\tError: {e.Message}");
						failed_count++;
					}
				}
				Assert.IsTrue(failed_count == 0, $"{failed_count}/{query_count} Queries Failed Validation - See output for details.");
			}
		}
	}
}
