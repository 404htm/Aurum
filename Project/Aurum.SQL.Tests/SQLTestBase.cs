using Aurum.Core;
using Aurum.Core.Parser;
using Aurum.SQL.Data;
using Aurum.SQL.Loaders;
using Aurum.SQL.Readers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.SQL.Tests
{
	public abstract class SQLTestBase
	{
		internal static StandardKernel IOC;
		internal static TestContext Context;

		static SQLTestBase()
		{
			IOC = new StandardKernel();
			IOC.Bind<IParserFactory>().To<ParserFactory>();
			IOC.Bind<ISqlQueryTemplateHydrator>().To<SqlQueryTemplateHydrator>();

			IOC.Bind<ISqlValidator>().ToMethod(c => new SqlQueryReader2012(TestHelpers.GetTestConnection()));
			IOC.Bind<ISqlQueryReader>().ToMethod(c => new SqlQueryReader2012(TestHelpers.GetTestConnection()));
			IOC.Bind<ISqlSchemaReader>().ToMethod(c => new SqlSchemaReader(TestHelpers.GetTestConnection()));

			IOC.Bind<IList<SqlQueryTemplateData>>().ToMethod(c => StoreableSet<SqlQueryTemplateData>.Load(Resources.GetDefaultTemplates()));
			IOC.Bind<ISqlQueryMetadataLoader>().To<SqlQueryMetadataLoader>();
		}

		public void WriteErrors(IEnumerable<SqlError> errors)
		{
			if (errors?.Any() ?? false) foreach (var e in errors) Context.WriteLine(e.Message);
		}
	}
}
