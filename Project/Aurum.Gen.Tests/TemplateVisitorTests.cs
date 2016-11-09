using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Moq;
using Aurum.Gen.Nodes;

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
            scope.Setup(s => s.GetList<object>("list")).Returns(items);
            scope.Setup(s => s.Set("cur", It.IsAny<object>())).Callback<string, object>((k, v) => current = v);
            scope.Setup(s => s.Get<object>("cur")).Returns(() => current);

            //Minimal tree to walk - Visitor terminates on Code so needed to include both objects for testing
            TemplateNode node = new ForEach() { Set = "list", Var = "cur" };
            node.Content.Add(new Code() { Value = "1" });
            node.Content.Add(new Code() { Value = "2" });

            //Build materializer that just concatenates the objects and code segment value
            string output = "HEADER|";
            var mat = new Mock<ICodeMaterializer>();
            mat.Setup(c => c.Process(It.IsAny<IScope>(), It.IsAny<Code>()))
                .Callback<IScope, Code>((s, c) => output += $"{s.Get<object>("cur")}{c.Value}|");

            var templateVisitor = new TemplateVisitor(mat.Object, (s) => scope.Object, null);
            templateVisitor.Visit(node, scope.Object);

            Assert.AreEqual("HEADER|A1|A2|B1|B2|C1|C2|D1|D2|E1|E2|", output);
        }

        //[TestMethod]
        //public void TemplateVisitor_IfTrue()
        //{
        //    var scope = new Mock<IScope>();
        //    scope.Setup(s => s.Get<bool?>("condition")).Returns(true);

        //}


    }
}
