using Aurum.Integration.Tests.Temp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Aurum.Integration.Tests
{
    [TestClass]
    public class BasicTemplateMaterializationTest : TestBase
    {
        [ClassInitialize]
        public static void SetupTests(TestContext testContext) => Context = testContext;

        [Ignore]
        [TestMethod]
        public void Integration_BasicTemplateMaterialization()
        {
            var customerMetadata = SetupCustomerMetadata();
            //TODO: Materialize Code
            //TODO: Compile Code

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
