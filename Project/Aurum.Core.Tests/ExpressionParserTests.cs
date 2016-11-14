using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aurum.Core.Parser;
using System.Text;
using Moq;
using System.Collections.Generic;

namespace Aurum.Core.Tests
{
    [TestClass]
    public class ExpressionParserTests
    {
        [TestMethod]
        public void Parser_SimpleStatement()
        {
            var runner = new ExpressionParser<Func<int, int>>();
            var result = runner.Parse("(n) => 6 + n").Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(11, result(5));
        }

        [TestMethod]
        public void Parser_SimpleStatementWithImport()
        {
            var runner = new ExpressionParser<Func<Context>>();
            runner.Register<Context>();
            runner.Import("Aurum.Core");
            var result = runner.Parse("() => new Context()").Result;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result());
        }

        [TestMethod]
        public void Parser_EvaluateWithScope()
        {
            var scope = new Mock<IScope>();
            scope.SetupGet(s => s["A"]).Returns(3);
            scope.Setup(s => s.Keys).Returns(new List<string>{ "A" });

            var runner = new ExpressionParser<Func<int, int>>();
            var result = runner.Parse("(n) => n + A", scope.Object).Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(8, result(5));
        }

    }
}
