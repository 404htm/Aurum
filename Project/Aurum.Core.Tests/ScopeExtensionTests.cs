using Aurum.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Aurum.Core.Tests
{
    [TestClass]
    public class ScopeExtensionTests
    {
        [TestMethod]
        public void ScopeExtension_ToExpando()
        {
            var scope = new Mock<IScope>();
            scope.Setup(s => s.Get<object>("ItemA")).Returns("A");
            scope.Setup(s => s.Get<object>("ItemB")).Returns(5);
            scope.Setup(s => s.Get<object>("ItemC")).Returns('C');
            scope.Setup(s => s.Get<object>("ItemD")).Returns(null);
            scope.Setup(s => s.Keys).Returns(new List<string> { "ItemA", "ItemB", "ItemC", "ItemD" });


            var result = scope.Object.ToExpando() as dynamic;
            Assert.AreEqual(result.ItemA, "A");
            Assert.AreEqual(result.ItemB, 5);
            Assert.AreEqual(result.ItemC, 'C');
            Assert.IsNull(result.ItemD);
        }
    }
}
