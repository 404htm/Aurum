using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

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
            scope[key] = value;

            var result = (string)scope[key];
            Assert.AreEqual(value, result);
        }

        [TestMethod]
        public void Scope_CheckNullValue()
        {
            var key = "TestKey";

            var scope = new Scope();
            var result = (string)scope[key];
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

            outer[pairOuter1.Item1] = pairOuter1.Item2;
            outer[pairOuter2.Item1] = pairOuter2.Item2;
            inner[pairInner1.Item1] = pairInner1.Item2;

            var resultInner = (string)inner[pairInner1.Item1];
            Assert.AreEqual(pairInner1.Item2, resultInner);

            var resultOuter = (string)inner[pairOuter2.Item1];
            Assert.AreEqual(pairOuter2.Item2, resultOuter);
        }

        [TestMethod]
        public void Scope_RetrieveListStringSingleScope()
        {
            var key = "TestKey";
            var value = new List<string> { "TestValue" };

            var scope = new Scope();
            scope[key] = value;

            var result = (IEnumerable<string>)scope[key];
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

            outer[pairOuter1.Item1] = pairOuter1.Item2;
            outer[pairOuter2.Item1] = pairOuter2.Item2;
            inner[pairInner1.Item1] = pairInner1.Item2;

            var resultInner = (IEnumerable<string>)inner[pairInner1.Item1];
            Assert.AreEqual(pairInner1.Item2, resultInner);

            var resultOuter = (IEnumerable<string>)inner[pairOuter2.Item1];
            Assert.AreEqual(pairOuter2.Item2, resultOuter);
        }

        [TestMethod]
        public void Scope_CheckKeysFlat()
        {
            var scope = new Scope();
            scope["A"] = "A";
            scope["B"] = 5;
            scope["C"] = null;

            var result = scope.Keys.OrderBy(k => k);
            var expected = new string[] { "A", "B", "C" };
            Assert.IsTrue(result.SequenceEqual(expected), "The keys returned by scope do not match the expected value");
        }

        [TestMethod]
        public void Scope_CheckKeysNested()
        {
            var scopeO = new Scope();
            scopeO["A"] = "A";
            scopeO["B"] = 5;
            scopeO["C"] = null;

            var scopeI = new Scope(scopeO);
            scopeI["B"] = "A";
            scopeI["C"] = 5;
            scopeI["D"] = null;

            var resultO = scopeO.Keys.OrderBy(k => k);
            var expectedO = new string[] { "A", "B", "C",};
            Assert.IsTrue(resultO.SequenceEqual(expectedO));

            var resultI = scopeI.Keys.OrderBy(k => k);
            var expectedI = new string[] { "A", "B", "C", "D"};
            Assert.IsTrue(resultI.SequenceEqual(expectedI));
        }

    }
}
