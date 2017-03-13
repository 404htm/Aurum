using Aurum.Gen.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Gen.Tests.Validators
{
    [TestClass]
    public class CSharpValidatorTests
    {
        public static TestContext Context { get; private set; }

        [ClassInitialize]
        public static void SetupTests(TestContext testContext) => Context = testContext;

        [TestMethod]
        public void Gen_BasicClassDeclarationPassesValidation()
        {
            var code = @"
                using test.include;

                namespace generated
                {
                    public class GeneratedCode
                    {
                        public bool GeneratedMethod()
                        {
                            return true;
                        }
                    }
                }
            ";

            var underTest = new CSharpValidator();
            var results = underTest.Parse(code);
            WriteResults(results, "Basic Class Declaration");
          
            Assert.IsFalse(results.Any());
        }

        [TestMethod]
        public void Gen_InvalidMethodNameGeneratesErrors()
        {
            var code = @"
                using test.include;

                namespace generated
                {
                    public class GeneratedCode
                    {
                        public bool 1GeneratedMethod()
                        {
                            return true;
                        }
                    }
                }
            ";

            var underTest = new CSharpValidator();
            var results = underTest.Parse(code);
            WriteResults(results, "Invalid Method Name");

            Assert.IsTrue(results.Any());
            Assert.AreEqual(3, results.Count());

            var msg = results.First();
            Assert.AreEqual("Invalid token '1' in class, struct, or interface member declaration", msg.Message);
            Assert.AreEqual("CS1519", msg.Code);
            Assert.AreEqual(7, msg.Line);
        }

        [TestMethod]
        public void Gen_MismatchedBracesGeneratesErrors()
        {
            var code = @"
                using test.include;

                namespace generated
                {
                    public class GeneratedCode
                    {
                        public bool GeneratedMethod()
                        {
                            return true;
                        }
                    }
            ";

            var underTest = new CSharpValidator();
            var results = underTest.Parse(code);
            WriteResults(results, "Invalid Method Name");

            Assert.IsTrue(results.Any());
            Assert.AreEqual(1, results.Count());

            var msg = results.First();
            Assert.AreEqual("} expected", msg.Message);
            Assert.AreEqual("CS1513", msg.Code);
            Assert.AreEqual(11, msg.Line);
        }

        private void WriteResults(IEnumerable<ValidationResult> results, string section)
        {
            Context.WriteLine($"{section} Validation results:");
            foreach (var r in results) Context.WriteLine($"\t{r.Code} : {r.Message.Replace("{","{{").Replace("}","}}")} (Line {r.Line})");
        }
    }
}
