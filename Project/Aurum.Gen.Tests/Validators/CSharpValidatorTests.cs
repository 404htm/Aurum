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

            Assert.IsTrue(results.Any());
            Assert.AreEqual(3, results.Count());

            var msg = results.First();
            Assert.AreEqual("Invalid token '1' in class, struct, or interface member declaration", msg.Message);
            Assert.AreEqual("CS1519", msg.Code);
        }
    }
}
