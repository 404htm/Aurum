using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Aurum.Gen.Nodes;
using Aurum.Core.Parser;
using System.Collections.Generic;
using Aurum.Core;

namespace Aurum.Gen.Tests
{
    [TestClass]
    public class TemplateVisitorTests
    {
        [TestMethod]
        public void TemplateVisitor_ForEach()
        {
            var items = new object[]{ "A", "B", "C", "D", "E"};
            object current = "X";

            //Scope Contains the set to enumerate and persists the current value - This should match "Var" from the ForEach block
            var scope = new Mock<IScope>();
            scope.SetupGet(s => s["list"]).Returns(items);
            scope.SetupSet(s => s["cur"] = It.IsAny<object>()).Callback<string, object>((k,v) => current = v);
            scope.SetupGet(s => s["cur"]).Returns(() => current);

            //Minimal tree to walk - Visitor terminates on Code so needed to include both objects for testing
            TemplateNode node = new ForEach() { Set = "list", Var = "cur" };
            node.Content.Add(new Code() { Value = "1" });
            node.Content.Add(new Code() { Value = "2" });

            //Build materializer that just concatenates the objects and code segment value
            string output = "HEADER|";
            var materializer = new Mock<ICodeMaterializer>();
            materializer.Setup(c => c.Process(It.IsAny<IScope>(), It.IsAny<Code>()))
                .Callback<IScope, Code>((s, c) => output += $"{s["cur"]}{c.Value}|");

            var templateVisitor = new TemplateVisitor(materializer.Object, (s) => scope.Object, null);
            templateVisitor.Visit(node, scope.Object);

            Assert.AreEqual("HEADER|A1|A2|B1|B2|C1|C2|D1|D2|E1|E2|", output);
        }

        [TestMethod]
        public void TemplateVisitor_IfTrue()
        {
            var scope = new Mock<IScope>();
            //Actual scope ignored because call would be from the parser (which is also mocked out)

            var parser = new Mock<IExpressionParser<Func<bool>>>();
            parser.Setup(p => p.Parse("condition", It.IsAny<IScope>())).ReturnsAsync(() => true).Verifiable();

            var factory = new Mock<IParserFactory>();
            factory.Setup(f => f.Create<Func<bool>>()).Returns(parser.Object);

            //Minimal tree to walk - Visitor terminates on Code so needed to include both objects for testing
            TemplateNode node = new If() { Condition = "condition",
                Content = new List<TemplateNode> { new Code { Value = "TRUE1" }, new Code { Value = "TRUE2" } },
                Else = new List<TemplateNode> { new Code { Value = "FALSE1" }, new Code { Value = "FALSE2" } },
            };

            //Build materializer that just concatenates the objects and code segment value
            string output = "HEADER|";
            var materializer = new Mock<ICodeMaterializer>();
            materializer.Setup(c => c.Process(It.IsAny<IScope>(), It.IsAny<Code>()))
                .Callback<IScope, Code>((s, c) => output += $"{c.Value}|");

            var templateVisitor = new TemplateVisitor(materializer.Object, (s) => scope.Object, factory.Object);
            templateVisitor.Visit(node, scope.Object);

            scope.Verify();
            parser.Verify();

            Assert.AreEqual("HEADER|TRUE1|TRUE2|", output);
        }

        [TestMethod]
        public void TemplateVisitor_IfFalse()
        {
            var scope = new Mock<IScope>();
            //Actual scope ignored because call would be from the parser (which is also mocked out)

            var parser = new Mock<IExpressionParser<Func<bool>>>();
            parser.Setup(p => p.Parse("condition", It.IsAny<IScope>())).ReturnsAsync(() => false).Verifiable();

            var factory = new Mock<IParserFactory>();
            factory.Setup(f => f.Create<Func<bool>>()).Returns(parser.Object);

            //Minimal tree to walk - Visitor terminates on Code so needed to include both objects for testing
            TemplateNode node = new If()
            {
                Condition = "condition",
                Content = new List<TemplateNode> { new Code { Value = "TRUE1" }, new Code { Value = "TRUE2" } },
                Else = new List<TemplateNode> { new Code { Value = "FALSE1" }, new Code { Value = "FALSE2" } },
            };

            //Build materializer that just concatenates the objects and code segment value
            string output = "HEADER|";
            var materializer = new Mock<ICodeMaterializer>();
            materializer.Setup(c => c.Process(It.IsAny<IScope>(), It.IsAny<Code>()))
                .Callback<IScope, Code>((s, c) => output += $"{c.Value}|");

            var templateVisitor = new TemplateVisitor(materializer.Object, (s) => scope.Object, factory.Object);
            templateVisitor.Visit(node, scope.Object);

            scope.Verify();
            parser.Verify();


            Assert.AreEqual("HEADER|FALSE1|FALSE2|", output);
        }

        [TestMethod]
        public void TemplateVisitor_Var()
        {
            int result = 0;

            var scope = new Mock<IScope>();
            scope.SetupSet(s => s["testVar"] = It.IsAny<int>()).Callback<string, object>((k, v) => result = (dynamic)v);

            var parser = new Mock<IExpressionParser<Func<object>>>();
            parser.Setup(p => p.Parse("testStatement", It.IsAny<IScope>())).ReturnsAsync(() => 15).Verifiable();

            var factory = new Mock<IParserFactory>();
            factory.Setup(f => f.Create<Func<object>>()).Returns(parser.Object);

            TemplateNode node = new Var()
            {
                Name = "testVar",
                Value = "testStatement"
            };

            var templateVisitor = new TemplateVisitor(null, (s) => scope.Object, factory.Object);
            templateVisitor.Visit(node, scope.Object);


            parser.Verify();
            Assert.AreEqual(15, result);
        }
    }
}
