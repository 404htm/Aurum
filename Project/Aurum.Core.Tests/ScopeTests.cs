using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Aurum.Core.Tests
{
    [TestClass]
    public class ScopeTests
    {
        [TestMethod]
        public void Scope_RetrieveStringSingleScope()
        {
            var key = "TestKey";
            var value = "TestValue";

            var scope = new Scope();
            scope.Set(key, value);

            var result = scope.Get<string>(key);
            Assert.AreEqual(value, result);
        }

        [TestMethod]
        public void Scope_CheckNullValue()
        {
            var key = "TestKey";

            var scope = new Scope();
            var result = scope.Get<string>(key);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Scope_CheckInnerAndOuterVars()
        {
            var pairOuter1 = Tuple.Create("key", "value_o1");
            var pairOuter2 = Tuple.Create("key2", "value_o2");
            var pairInner1 = Tuple.Create("key", "value_i1");

            var outer = new Scope();
            var inner = new Scope(outer);

            outer.Set(pairOuter1.Item1, pairOuter1.Item2);
            outer.Set(pairOuter2.Item1, pairOuter2.Item2);
            inner.Set(pairInner1.Item1, pairInner1.Item2);

            var resultInner = inner.Get<string>(pairInner1.Item1);
            Assert.AreEqual(pairInner1.Item2, resultInner);

            var resultOuter = inner.Get<string>(pairOuter2.Item1);
            Assert.AreEqual(pairOuter2.Item2, resultOuter);
        }

        [TestMethod]
        public void Scope_RetrieveListStringSingleScope()
        {
            var key = "TestKey";
            var value = new List<string> { "TestValue" };

            var scope = new Scope();
            scope.SetList(key, value);

            var result = scope.GetList<string>(key);
            Assert.AreEqual(value, result);
        }

        [TestMethod]
        public void Scope_CheckInnerAndOuterListVars()
        {
            var pairOuter1 = Tuple.Create("key", new List<string> { "value_o1" });
            var pairOuter2 = Tuple.Create("key2", new List<string> { "value_o2" });
            var pairInner1 = Tuple.Create("key", new List<string> { "value_i1" });

            var outer = new Scope();
            var inner = new Scope(outer);

            outer.SetList(pairOuter1.Item1, pairOuter1.Item2);
            outer.SetList(pairOuter2.Item1, pairOuter2.Item2);
            inner.SetList(pairInner1.Item1, pairInner1.Item2);

            var resultInner = inner.GetList<string>(pairInner1.Item1);
            Assert.AreEqual(pairInner1.Item2, resultInner);

            var resultOuter = inner.GetList<string>(pairOuter2.Item1);
            Assert.AreEqual(pairOuter2.Item2, resultOuter);
        }


    }
}
