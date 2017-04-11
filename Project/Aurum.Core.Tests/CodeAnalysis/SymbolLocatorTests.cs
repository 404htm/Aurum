using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Aurum.Core.CodeAnalysis;
using Aurum.Core.Tests.Resources;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace Aurum.Core.Tests.CodeAnalysis
{
    public class SymbolLocatorTests
    {
        [Fact]
        public void FindImplementationsThrowsIfInterfaceNotLoaded()
        {
            var comp = makeScriptCompilation("");
            
            List<INamedTypeSymbol> _;

            var ex = Assert.Throws<ArgumentException>(
                () => _ = SymbolLocator.FindImplementations<ITestInterface>(comp)
            );

            Assert.Contains("The specified interface cannot be located in compilation", ex.Message);
        }

        [Fact]
        public void FindImplementationsReturnsNothingForEmptyCompilation()
        {
            var comp = makeScriptCompilation("", getReference<ITestInterface>());

            var result = SymbolLocator.FindImplementations<ITestInterface>(comp);

            Assert.Empty(result);
        }

        [Fact]
        public void FindImplementationLocatesSingleClassWithNoExplicitNamespace()
        {
            var code =
            @"
                using Aurum.Core.Tests.Resources;

                public class TestImplementation: ITestInterface
                {
                    public string DoSomething() => ""Hello World"";
                }
            ";
            var comp = makeScriptCompilation(code, getReference<ITestInterface>());

            var result = SymbolLocator.FindImplementations<ITestInterface>(comp);

            Assert.Single(result);
            Assert.Equal("TestImplementation", result.Single().Name);
        }

        private Compilation makeEmptyComp()
        {
            return makeScriptCompilation(@"");
        }

        private MetadataReference getReference<T>()
        {
            var assembly = typeof(T).Assembly.Location;
            var @ref = MetadataReference.CreateFromFile(assembly);
            return @ref;
        }

        private Compilation makeScriptCompilation(string code, params MetadataReference[] references)
        {
            var options = ScriptOptions.Default;
            options = options.AddReferences(references);

            var state =  CSharpScript.RunAsync(code, options).Result;
            return state.Script.GetCompilation();
        }
    }
}
