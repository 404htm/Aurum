using Aurum.Core.Parser;
using Aurum.Gen;
using Aurum.Gen.Validators;
using Aurum.Integration.Tests.Temp;
using Aurum.TemplateUtils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aurum.Integration.Tests
{
    [TestClass]
    public class BasicTemplateMaterializationTest : TestBase
    {
        [ClassInitialize]
        public static void SetupTests(TestContext testContext) => Context = testContext;

        [TestMethod]
        [Ignore]
        public void Integration_BasicTemplateMaterialization()
        {
            var model = SetupCustomerMetadata();
            var emitter = IOC.Get<ICodeEmitter>();
            var compiler = IOC.Get<ICodeValidator>();
           
            //var parser = IOC.Get<IParserFactory>().Create<ITemplate<GroupMetadata>>();

            var templateSource = File.ReadAllLines("./Templates/datalayer-basic/Repository.au.cs");
            var materializerFactory = IOC.Get<ITemplateMaterializerFactory>();
            var materializer = materializerFactory.Create<GroupMetadata>(templateSource);

            var template = materializer.Build().Result;
            template.GenerateCode(model, emitter);

            var code = ((ICodeSource)emitter).GetCode();
            Context.WriteLine("Emitted Code:");
            Context.WriteLine(new string('-', 30));
            Context.WriteLine(code);
            Context.WriteLine(new string('-', 30));
            Assert.IsTrue(code.Length > 0);

            var result = compiler.Parse(code);
            foreach(var r in result)
            {
                Context.WriteLine(r.Message);
            }

            Assert.IsFalse(result.Any());
        }

        private GroupMetadata SetupCustomerMetadata()
        {
            var group = new GroupMetadata
            {
                Name = "Customer",
                Objects = new List<ObjectMetadata>(),
                Operations = new List<OperationMetadata>()
            };

            group.Objects.Add(new ObjectMetadata
            {
                Name = "CustomerInformation"
            });

            return group;
        }
    }
}
