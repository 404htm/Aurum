using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Aurum.Core.CodeAnalysis;

namespace Aurum.Core.CSharp
{
    /// <summary>
    /// Create object instances from text. 
    /// Requirements:
    /// Code is parsed as a CSharpScript - No entry point is required
    /// Each class must contain a default constructor
    /// All types in T must be registered with the scripts
    /// </summary>
    public class InstanceCompiler
    {
        ScriptOptions _options;
        Func<Compilation, INamedTypeSymbol> _typeLocator;

        /// <param name="typeLocator">
        /// Function to locate desired class/classes within a compilation
        /// <see cref="ISymbolLocator"/>
        /// </param>
        /// 
        public InstanceCompiler(Func<CSharpCompilation, List<INamedTypeSymbol>> typeLocator, ICodeBuilder codeBuilder)
        {
            _options = ScriptOptions.Default;
        }

        public void Import(string v)
        {
            _options.AddImports(v);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="classCode"></param>
        /// <returns></returns>
        public async Task<List<T>> Create<T>(string classCode)
        {
            var declaration = await CSharpScript.RunAsync(classCode, _options);
            var comp = declaration.Script.GetCompilation();
            var types = _typeLocator(comp);

            var gArgs = typeof(T).GetGenericArguments();

            ////TODO: PLINQ
            //foreach(var line in lines)
            //{
            //    var instance = await declaration.ContinueWithAsync<I>(line);
            //    instances.Add(instance.ReturnValue);
            //}

            //return instances;
            return null;
        }

     
        //static List<string> ExtractConstructors(ScriptState state)
        //{
        //    var comp = state.Script.GetCompilation();

        //    //Cross reference the interface we want with the symbols in the compilation
        //    var targetInterface = comp.GetTypeByMetadataName(typeof(I).FullName);
        //    var targetType = comp.GetTypeByMetadataName(typeof(T).FullName);

        //    var global = comp.Assembly.GlobalNamespace;
        //    var namespaces = global.ConstituentNamespaces;
        //    var members = namespaces.SelectMany(m => m.GetTypeMembers());
        //    var implementors = members.Where(t => t.Interfaces.Contains(targetInterface)).ToList();

        //    if (!implementors.Any()) throw new InvalidOperationException("No Types Found");
        //    //TODO: Throw if default constructor is missing

        //    var genericized = implementors.Select(i => i.Construct(targetType)).ToList();
        //    return genericized.Select(t => $"new {t.ToDisplayString()}()").ToList(); //TODO: Convert to something less shaky
        //}

        
    }
}
