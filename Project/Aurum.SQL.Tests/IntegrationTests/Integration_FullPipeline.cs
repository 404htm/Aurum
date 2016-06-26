using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Aurum.Core;
using Aurum.SQL.Templates;
using Aurum.Core.Parser;
using Ninject;
using Aurum.SQL.Data;
using Aurum.SQL.Readers;
using System.Data.SqlClient;

namespace Aurum.SQL.Tests.IntegrationTests
{
	[TestClass]
	public class Integration_FullPipeline
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
			IOC.Bind<ISqlValidator>().ToMethod(c => new SqlQueryReader2012(TestHelpers.GetTestConnection()));
			IOC.Bind<ISqlSchemaReader>().ToMethod(c => new SqlSchemaReader(TestHelpers.GetTestConnection()));
			IOC.Bind<IList<SqlQueryTemplateData>>().ToMethod(c => StoreableSet<SqlQueryTemplateData>.Load(Resources.GetDefaultTemplates()));
		}

		[TestMethod]
		public void Integration_AllTables_DefaultTemplates_FullPipeline()
		{
			var tableMetadata = ReadTableMetadataFromTestDatabase();
			var templates = ReadAndHydrateDefaultTemplates();
			var queryDefinitions = MaterializeTemplates(templates, tableMetadata);
			AssertGeneratedQueryValidity(queryDefinitions);
		}

		private IList<SqlTableDetail> ReadTableMetadataFromTestDatabase()
		{
			IList<SqlTableDetail> details;
			using (var reader = IOC.Get<ISqlSchemaReader>())
			{
				var tables = reader.GetTables();
				details = tables.Select(reader.GetTableDetail).ToList();

				Context.WriteLine($"Integration Results: {tables.Count()} tables found.");
				Assert.IsTrue(tables.Any(), "Tables not retrieved from connection");
				Assert.IsTrue(details.Any(), "Details not retrieved from connection");
			}
			return details;
		}

		private List<ISqlQueryTemplate> ReadAndHydrateDefaultTemplates()
		{
			var rawTemplates = IOC.Get<IList<SqlQueryTemplateData>>();
			Assert.IsTrue(rawTemplates.Any(), "Templates could not be loaded from assembly resources");

			var hydrator = IOC.Get<ISqlQueryTemplateHydrator>();
			var templates = rawTemplates.Select(hydrator.Hydrate).ToList();
			return templates;
		}

		private List<SqlQueryDefinition> MaterializeTemplates(IList<ISqlQueryTemplate> templates, IList<SqlTableDetail> tables)
		{
			var builder = new TemplateMaterializer(templates);
			var query_sets = tables.SelectMany(builder.Build).ToList();
			return query_sets;
		}

		private void AssertGeneratedQueryValidity(List<SqlQueryDefinition> queryDefinitions)
		{
			int query_count = queryDefinitions.Count();
			int failed_count = 0;

			using (var validator = IOC.Get<ISqlValidator>())
			{
				foreach (var query in queryDefinitions)
				{
					IList<SqlError> errors;

					Context.WriteLine($"Validating {query.SourceName} - {query.Name}: {query.Query}".Escape());
					validator.Validate(query.Query, out errors);

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
