using Aurum.Core;
using Aurum.Core.Parser;
using Aurum.Gen;
using Aurum.Gen.Validators;
using Aurum.SQL;
using Aurum.SQL.Data;
using Aurum.SQL.Loaders;
using Aurum.SQL.Readers;
using Aurum.SQL.Tests;
using Aurum.TemplateUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Aurum.Integration.Tests
{
    public abstract class TestBase
    {
        internal static StandardKernel IOC;
        internal static TestContext Context;

        static TestBase()
        {
            IOC = new StandardKernel();
            IOC.Bind<IParserFactory>().To<ParserFactory>();
            IOC.Bind<ISqlQueryTemplateHydrator>().To<SqlQueryTemplateHydrator>();

            IOC.Bind<ISqlValidator>().ToMethod(c => new SqlQueryReader2012(SQLTestHelpers.GetTestConnection()));
            IOC.Bind<ISqlQueryReader>().ToMethod(c => new SqlQueryReader2012(SQLTestHelpers.GetTestConnection()));
            IOC.Bind<ISqlSchemaReader>().ToMethod(c => new SqlSchemaReader(SQLTestHelpers.GetTestConnection()));

            IOC.Bind<IList<SqlQueryTemplateData>>().ToMethod(c => StoreableSet<SqlQueryTemplateData>.Load(Resources.GetDefaultTemplates()));
            IOC.Bind<ISqlQueryMetadataLoader>().To<SqlQueryMetadataLoader>();

            IOC.Bind<ICodeEmitter>().To<CodeEmitter>();
            IOC.Bind<ICodeValidator>().To<CSharpValidator>();
        }

        public void WriteErrors(IEnumerable<SqlError> errors)
        {
            if (errors?.Any() ?? false) foreach (var e in errors) Context.WriteLine(e.Message);
        }
    }
}
