using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Gen.Tests
{
    [TestClass]
    public class CodeEmitterTests
    {
        [TestMethod]
        public void EmitterAccuratelyMapsOutput()
        {
            var underTest = new CodeEmitter(1000);

            for (int i = 1; i <= 100; i++) underTest.WriteLine($"Line{i}", i * 2);

            var result = underTest.GetCode();
            Assert.AreEqual(100, result.Count());

            Assert.AreEqual("Line1", result[0].Text);
            Assert.AreEqual(2, result[0].InputLineNumber);
            Assert.AreEqual(1, result[0].OutputLineNumber);

            Assert.AreEqual("Line50", result[49].Text);
            Assert.AreEqual(100, result[49].InputLineNumber);
            Assert.AreEqual(50, result[49].OutputLineNumber);

            Assert.AreEqual("Line100", result[99].Text);
            Assert.AreEqual(200, result[99].InputLineNumber);
            Assert.AreEqual(100, result[99].OutputLineNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void EmitterThrowsWhenMaxExceeded()
        {
            var underTest = new CodeEmitter(10);

            for (int i = 1; i <= 10; i++) underTest.WriteLine($"Line{i}", i * 2);

            underTest.WriteLine("This should crash");
            
        }
    }
}
