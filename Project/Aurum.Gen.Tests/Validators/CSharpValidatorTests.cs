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
        public void Gen_VerifyBasicClassDeclarationPasses()
        {
            var code = @"
                using custom.namespace;

                namespace Code.Generated;
                public class GeneratedCode
                {
                    public bool GeneratedMethod()
                    {
                        return true;
                    }
                };
            ";

            var underTest = new CSharpValidator();
            var result = underTest.Parse(code);
        }
    }
}
