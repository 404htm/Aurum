using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Gen.Tests
{
    [TestClass]
    public class TemplateRewriterFactoryTests
    {
        [TestMethod]
        public void VerifyRewriterCreated()
        {
            var underTest = new TemplateRewriterFactory(":", "`", "`");
            var result = underTest.Create(λ => $"TEST({λ});", (λ) => $"{{{λ}}}");

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(TemplateRewriter));

            var str = result.Rewrite(new string[] { ":MYCODE(`var`)" });
            //Minimal test of rewriter to make sure settings were applied
            Assert.AreEqual("TEST(MYCODE({var}));", str.First()); 
        }
    }
}
