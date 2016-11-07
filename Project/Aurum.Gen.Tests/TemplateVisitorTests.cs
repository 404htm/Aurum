using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aurum.Gen.Data;
using System.Linq;

namespace Aurum.Gen.Tests
{
    [TestClass]
    public class TemplateVisitorTests
    {

        //[TestMethod]
        public void TEMPTEST()
        {
            var items = Enumerable.Range('A', 10).Select(c => ((char)c).ToString());
            TemplateNode node = new ForEach() { Set = "TestSet", Var = "Current" };
            var scope = new Scope();
            scope.SetList("TestSet", items);

            var templateVisitor = new TemplateVisitor(null, (s) => new Scope(s));
            templateVisitor.Visit(node, scope);


        }


    }
}
