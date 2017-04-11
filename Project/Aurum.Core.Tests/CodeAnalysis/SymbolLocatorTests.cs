using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Aurum.Core.CodeAnalysis;
using Aurum.Core.Tests.Resources;
using Xunit;
using System.Collections.Generic;

namespace Aurum.Core.Tests.CodeAnalysis
{
    public class SymbolLocatorTests
    {
        [Fact]
        public void FindImplementationsThrowsIfInterfaceNotLoaded()
        {
            var comp = makeScriptCompilation("");
            List<INamedTypeSymbol> _;

            var ex = Assert.Throws<Exception>(
                () => _ = SymbolLocator.FindImplementations<ITestInterface>(comp)
            );

            Assert.Contains("The specified interface cannot be located in compilation", ex.Message);
        }

        [Fact]
        public void FindImplementationsReturnsNothingForEmptyCompilation()
        {
            Assert.True(false);
            var comp = makeScriptCompilation("");
            var underTest = SymbolLocator.FindImplementations<ITestInterface>(comp);

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
