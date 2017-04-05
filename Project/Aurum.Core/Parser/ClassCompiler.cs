using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace Aurum.Core.Parser
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ClassCompiler<I, T> : ICompiler<I, T>
    {
        ScriptOptions _options;

        public ClassCompiler()
        {
            _options = ScriptOptions.Default;
        }
        public void Import(string v)
        {
            _options.AddImports(v);
        }

        public async Task<List<I>> Create(string classCode)
        {
            var declaration = await CSharpScript.RunAsync(classCode, _options);
            var lines = ExtractConstructors(declaration);
            var instances = new List<I>();

            //TODO: PLINQ
            foreach(var line in lines)
            {
                var instance = await declaration.ContinueWithAsync<I>(line);
                instances.Add(instance.ReturnValue);
            }
            
            return instances;
        }

        static List<string> ExtractConstructors(ScriptState state)
        {
            var comp = state.Script.GetCompilation();

            //Cross reference the interface we want with the symbols in the compilation
            var targetInterface = comp.GetTypeByMetadataName(typeof(I).FullName);
            var targetType = comp.GetTypeByMetadataName(typeof(T).FullName);

            var global = comp.Assembly.GlobalNamespace;
            var namespaces = global.ConstituentNamespaces;
            var members = namespaces.SelectMany(m => m.GetTypeMembers());
            var implementors = members.Where(t => t.Interfaces.Contains(targetInterface)).ToList();

            if (!implementors.Any()) throw new InvalidOperationException("No Types Found");
            //TODO: MAKE PEOPLE FEEL BAD IF WE DON"T HAVE A DEFAULT CONSTRUCTOR 

            var genericized = implementors.Select(i => i.Construct(targetType)).ToList();
            return genericized.Select(t => $"new {t.ToDisplayString()}()").ToList(); //TODO: Convert to something less shaky
        }

        
    }
}
