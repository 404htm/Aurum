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
        public void FindImplementationLocatesSingleClass()
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

        [Fact]
        public void FindImplementationIgnoresOtherInterfaces()
        {
            var code =
            @"
                using Aurum.Core.Tests.Resources;

                public class TestImplementation: IDontCare
                {
                    public string DoSomething() => ""Hello World"";
                }
            ";

            var comp = makeScriptCompilation(code, getReference<ITestInterface>());
            var result = SymbolLocator.FindImplementations<ITestInterface>(comp);

            Assert.Empty(result);
        }

        [Fact]
        public void FindImplementationLocatesNestedClass()
        {
            var code =
            @"
                using Aurum.Core.Tests.Resources;
                public class OuterClass
                {
                    public class TestImplementation: ITestInterface
                    {
                        public string DoSomething() => ""Hello World"";
                    }
                }
            ";

            var comp = makeScriptCompilation(code, getReference<ITestInterface>());
            var result = SymbolLocator.FindImplementations<ITestInterface>(comp);

            Assert.Single(result);
            Assert.Equal("TestImplementation", result.Single().Name);
        }

        [Fact]
        public void FindImplementationLocatesMultipleClasses()
        {
            var code =
            @"
                using Aurum.Core.Tests.Resources;

                public class TestImplementation1: ITestInterface
                {
                    public string DoSomething() => ""Hello World 1"";
                }

                public class TestImplementation2: ITestInterface
                {
                    public string DoSomething() => ""Hello World 2"";
                }
            ";

            var comp = makeScriptCompilation(code, getReference<ITestInterface>());
            var result = SymbolLocator.FindImplementations<ITestInterface>(comp);

            Assert.Equal(2, result.Count());
            Assert.Equal("TestImplementation1", result[0].Name);
            Assert.Equal("TestImplementation2", result[1].Name);
        }

        private Compilation makeEmptyComp()
        {
            return makeScriptCompilation(@"");
        }

        #region Compilation Setup Code
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

            var state = CSharpScript.RunAsync(code, options).Result;
            return state.Script.GetCompilation();
        } 
        #endregion
    }
}
