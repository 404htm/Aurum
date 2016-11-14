using Aurum.Core.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.Scripting.Hosting;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Core.Parser
{
    public class ExpressionParser<T> : IExpressionParser<T>
    {
        List<string> _imports = new List<string>();
        List<Type> _types = new List<Type>();
        const string DYNAMIC_OBJ = "__scope";

        public void Import(string import) =>_imports.Add(import);
        public void Register<T2>() =>_types.Add(typeof(T2));
        public void Register(Type type) => _types.Add(type);

        public ExpressionParser()
        {
            Register<DynamicObject>();
            Register<Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo>();
            Register<CSharpArgumentInfo>();
        }

        public async Task<T> Parse(string expression)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            return await Parse(expression, null, null);
        }

        public async Task<T> Parse(string expression, IScope scope)
        {
            if (scope == null) throw new ArgumentNullException(nameof(scope));
            if (expression == null) throw new ArgumentNullException(nameof(expression));

            var globals = scope.ToExpando();
            return await Parse(expression, globals, typeof(ExpandoObject));
        }

        async Task<T> Parse(string expression, ExpandoObject globals, Type typeGlobals)
        {
            var loader = new InteractiveAssemblyLoader();
            var script = CSharpScript.Create<T>($"return {expression};", GetOptions(), typeGlobals, loader);
            var task = script.RunAsync(globals);
            var result = await task;
            return result.ReturnValue;
        }

        private ScriptOptions GetOptions()
        {
            var metadata = _types
                .Distinct()
                .Select(λ => λ.Assembly.Location)
                .Select(λ => MetadataReference.CreateFromFile(λ));

            return ScriptOptions.Default
                .AddImports(_imports)
                .WithReferences(metadata);
                

        }

        private string BuildIndirectAdaptor(ExpandoObject globals)
        {
            //Workaround for https://github.com/dotnet/roslyn/issues/3194
            var sb = new StringBuilder();

            return sb.ToString();
        }
    }
}
