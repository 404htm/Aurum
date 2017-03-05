using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Aurum.SQL.Templates;
using Ninject;
using Aurum.SQL.Data;
using Aurum.SQL.Loaders;
using Aurum.SQL;
using System.Data.SqlClient;
using Aurum.SQL.Tests;

namespace Aurum.Integration.Tests
{
    [TestClass]
	public class Integration_FullPipeline : TestBase
	{
		[ClassInitialize] public static void SetupTests(TestContext testContext) => Context = testContext;

		[TestMethod]
		public void Integration_AllTables_DefaultTemplates_FullPipeline()
		{
			var tableMetadata = ReadTableMetadataFromTestDatabase();
			var templates = ReadAndHydrateDefaultTemplates();

			var queryDefinitions = MaterializeTemplates(templates, tableMetadata);
			AssertGeneratedQueryValidity(queryDefinitions);

			var queryDetails = ReadMetadataForQueries(queryDefinitions);
			AssertLoadedQueryValidity(queryDetails);
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

		private void AssertLoadedQueryValidity(List<SqlQueryDetail> queryDetails)
		{
			Context.WriteLine("Validating Query Details");
			foreach (var query in queryDetails)
			{
				var inputs = query.Inputs?.Select(λ => λ.Name);
				var outputs = query.Outputs?.Select(λ => λ.Name);

				Context.WriteLine($"\t{query.GroupName} : {query.Name}");
				Context.WriteLine($"\t\tInputs: {string.Join(", ", inputs)}");
				Context.WriteLine($"\t\tOutputs: {string.Join(", ", outputs)}");

				Assert.IsNotNull(query.Inputs);
				Assert.IsNotNull(query.Outputs);
			}
		}

		private List<SqlQueryDetail> ReadMetadataForQueries(List<SqlQueryDefinition> queryDefinitions)
		{
			var loader = IOC.Get<ISqlQueryMetadataLoader>();
			return queryDefinitions.Select(loader.LoadQueryDetails).ToList();
		}

	}
}
