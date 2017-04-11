using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Aurum.Core.CodeAnalysis;

namespace Aurum.Core.Tests.CodeAnalysis
{
    [TestClass]
    public class SymbolLocatorTests
    {


        [TestMethod]
        public void FindImplementationsReturnsNothingForEmptyCompilation()
        {
           // var underTest = SymbolLocator.FindImplementations<ITest>();
        }

        private Compilation makeEmptyComp()
        {
            return makeScriptCompilation(@"");
        }

        private Compilation makeScriptCompilation(string code)
        {
            var options = ScriptOptions.Default;
            var state =  CSharpScript.RunAsync(code, options).Result;
            return state.Script.GetCompilation();
        }
    }
}
