using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum.Core.Parser
{
	public class ExpressionParser<T> : IExpressionParser<T>
	{
		List<string> _imports = new List<string>();
		List<Type> _types = new List<Type>();

		public void Import(string import)
		{
			_imports.Add(import);
		}

		public void Register<T2>()
		{
			_types.Add(typeof(T2));
		}

		public void Register(Type type)
		{
			_types.Add(type);
		}

		public T Parse(string expression)
		{
			var loader = new InteractiveAssemblyLoader();
			var script = CSharpScript.Create<T>($"return {expression};", GetOptions(), null, loader);
			var task = script.RunAsync().Result;
			return task.ReturnValue;
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
	}
}
