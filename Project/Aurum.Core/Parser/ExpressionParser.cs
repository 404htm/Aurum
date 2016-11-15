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

    public class Globals
    {
        public dynamic __scope;
    }

    public class ExpressionParser<T> : IExpressionParser<T>
    {
        List<string> _imports = new List<string>();
        List<Type> _types = new List<Type>();
        const string DYN_ALIAS = "__scope";

        public void Import(string import) =>_imports.Add(import);
        public void Register<T2>() =>_types.Add(typeof(T2));
        public void Register(Type type) => _types.Add(type);

        public ExpressionParser()
        {
            Register<DynamicObject>();
            Register<CSharpArgumentInfo>();
        }

        public async Task<T> Parse(string expression)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            return await Parse(expression, (ExpandoObject)null);
        }

        public async Task<T> Parse(string expression, IScope scope)
        {
            if (scope == null) throw new ArgumentNullException(nameof(scope));
            if (expression == null) throw new ArgumentNullException(nameof(expression));

            var globals = scope.ToExpando();
            return await Parse(expression, globals);
        }

        async Task<T> Parse(string expression, ExpandoObject globals)
        {
            string expr;
            var vars = new Globals{ __scope = globals };

            if (globals != null) expr = $"{BuildIndirectAdaptor(globals)} return {expression};";
            else expr = $"return {expression};";

            var loader = new InteractiveAssemblyLoader();
            var task = CSharpScript.RunAsync<T>(expr, GetOptions(), vars, typeof(Globals));

            //var script = CSharpScript.Create<T>(expr, GetOptions(), typeof(Globals), loader);
            //var task = script.RunAsync(vars);
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
            //TODO: Bring this up in confession

            var vars = globals as IDictionary<string, object>;
            var sb = new StringBuilder();
            foreach(var v in vars)
            {
                var type = v.Value.GetType();
                sb.AppendLine($"var {v.Key} = {DYN_ALIAS}.{v.Key};");
            }
            return sb.ToString();
        }

    }
}
